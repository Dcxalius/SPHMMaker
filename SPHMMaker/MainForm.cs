using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SPHMMaker.Items;

namespace SPHMMaker
{
    public partial class MainForm : Form
    {
        public static MainForm Instance;
        const int ItemImageSize = 48;
        const int ItemHorizontalPadding = 8;
        const int ItemVerticalPadding = 4;
        static readonly string[] SupportedImageExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp" };

        readonly Dictionary<string, Image> itemImageCache = new();
        readonly Image defaultItemImage;
        bool imagesDisposed;

        static readonly IReadOnlyDictionary<ItemData.ItemQuality, Color> ItemQualityColors = new Dictionary<ItemData.ItemQuality, Color>
        {
            { ItemData.ItemQuality.Poor, Color.DimGray },
            { ItemData.ItemQuality.Common, Color.WhiteSmoke },
            { ItemData.ItemQuality.Uncommon, Color.MediumSeaGreen },
            { ItemData.ItemQuality.Rare, Color.RoyalBlue },
            { ItemData.ItemQuality.Epic, Color.MediumPurple },
            { ItemData.ItemQuality.Legendary, Color.Orange }
        };
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        int editingItem = -1;


        public MainForm()
        {
            Instance = this;
            defaultItemImage = CreateDefaultItemImage();
            InitializeComponent();
            AllocConsole();

            InitializeItems();
        }
        private void loadDatapackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? path = GetDirectory();

            if (path is null)
            {
                return;
            }

            //TODO: Check access?
            //DirectoryInfo di = new DirectoryInfo(path);
            //di.GetAccessControl().GetAccessRules();

            string[] foldersThatShouldBeHere = ["Items"];

            string[] folders = Directory.GetDirectories(path);


            foreach (string folder in foldersThatShouldBeHere)
            {
                if (!folders.Contains(path + "\\" + folder))
                {
                    MessageBox.Show($"Error, {folder} is not found");
                    return;
                }
            }

            ItemManager.Load(path + "\\Items");
        }

        private void items_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= items.Items.Count)
            {
                return;
            }

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color backgroundColor = isSelected ? SystemColors.Highlight : items.BackColor;
            Color textColor = isSelected ? SystemColors.HighlightText : items.ForeColor;

            using (SolidBrush backgroundBrush = new(backgroundColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }

            if (items.Items[e.Index] is not ItemData item)
            {
                TextRenderer.DrawText(e.Graphics, items.Items[e.Index].ToString() ?? string.Empty, e.Font, e.Bounds, textColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                e.DrawFocusRectangle();
                return;
            }

            Rectangle imageRect = CalculateImageRectangle(e.Bounds);
            Image image = GetItemImage(item);
            e.Graphics.DrawImage(image, imageRect);

            Rectangle borderRect = Rectangle.Inflate(imageRect, 1, 1);
            using (Pen borderPen = new(GetQualityColor(item.Quality), 2f))
            {
                e.Graphics.DrawRectangle(borderPen, borderRect);
            }

            Rectangle textRect = new(imageRect.Right + ItemHorizontalPadding, e.Bounds.Y, e.Bounds.Width - imageRect.Width - (ItemHorizontalPadding * 2), e.Bounds.Height);
            TextRenderer.DrawText(e.Graphics, item.Name, e.Font, textRect, textColor, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            e.DrawFocusRectangle();
        }

        static Rectangle CalculateImageRectangle(Rectangle bounds)
        {
            int availableHeight = Math.Max(bounds.Height - (ItemVerticalPadding * 2), 1);
            int size = Math.Min(ItemImageSize, availableHeight);
            int imageY = bounds.Y + ((bounds.Height - size) / 2);
            return new Rectangle(bounds.X + ItemHorizontalPadding, imageY, size, size);
        }

        Image GetItemImage(ItemData item)
        {
            if (item.GfxPath == null || string.IsNullOrWhiteSpace(item.GfxPath.Name))
            {
                return defaultItemImage;
            }

            string cacheKey = item.GfxPath.ToString();

            if (itemImageCache.TryGetValue(cacheKey, out Image? cachedImage))
            {
                return cachedImage;
            }

            foreach (string candidate in GetCandidatePaths(item))
            {
                if (!TryLoadImage(candidate, out Image? loadedImage))
                {
                    continue;
                }

                itemImageCache[cacheKey] = loadedImage;
                return loadedImage;
            }

            itemImageCache[cacheKey] = defaultItemImage;
            return defaultItemImage;
        }

        static IEnumerable<string> GetCandidatePaths(ItemData item)
        {
            if (item.GfxPath == null)
            {
                yield break;
            }

            string relativePath = item.GfxPath.ToString();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            yield return Path.Combine(baseDirectory, relativePath);
            yield return Path.Combine(baseDirectory, "Gfx", relativePath);
            yield return Path.Combine(baseDirectory, item.GfxPath.Type.ToString(), item.GfxPath.Name);
            yield return Path.Combine(baseDirectory, "Gfx", item.GfxPath.Type.ToString(), item.GfxPath.Name);
        }

        static bool TryLoadImage(string basePath, out Image? image)
        {
            if (TryLoadImageCore(basePath, out image))
            {
                return true;
            }

            foreach (string extension in SupportedImageExtensions)
            {
                string candidate = basePath + extension;
                if (TryLoadImageCore(candidate, out image))
                {
                    return true;
                }
            }

            image = null;
            return false;
        }

        static bool TryLoadImageCore(string path, out Image? image)
        {
            if (!File.Exists(path))
            {
                image = null;
                return false;
            }

            try
            {
                image = Image.FromFile(path);
                return true;
            }
            catch (Exception)
            {
                image = null;
                return false;
            }
        }

        static Image CreateDefaultItemImage()
        {
            Bitmap bitmap = new(ItemImageSize, ItemImageSize);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.FromArgb(48, 48, 48));

                using Font font = new(FontFamily.GenericSansSerif, 18, FontStyle.Bold, GraphicsUnit.Pixel);
                using SolidBrush brush = new(Color.LightGray);
                using StringFormat format = new()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                graphics.DrawString("?", font, brush, new RectangleF(0, 0, ItemImageSize, ItemImageSize), format);
            }

            return bitmap;
        }

        static Color GetQualityColor(ItemData.ItemQuality quality)
        {
            if (ItemQualityColors.TryGetValue(quality, out Color color))
            {
                return color;
            }

            return Color.White;
        }

        void DisposeItemImages()
        {
            if (imagesDisposed)
            {
                return;
            }

            foreach (Image image in itemImageCache.Values)
            {
                if (!ReferenceEquals(image, defaultItemImage))
                {
                    image.Dispose();
                }
            }

            defaultItemImage.Dispose();
            itemImageCache.Clear();
            imagesDisposed = true;
        }

        private void saveDatapackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? s = GetDirectory();

            if (s is null)
            {
                MessageBox.Show("Save aborted.");
                return;
            }

            //ItemManager.Save(s);
        }

        string? GetDirectory()
        {
            var fbg = new FolderBrowserDialog()
            {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            };

            if (fbg.ShowDialog() != DialogResult.OK)
                return null;

            return fbg.SelectedPath;
        }

        private void itemCheckGeneratedTooltip_Click(object sender, EventArgs e)
        {
            ItemData item = FoldDataIntoItem;
            string tooltip = itemNameInput.Text;
            tooltip += "\n";
            tooltip += itemDescriptionInput.Text;
            tooltip += "\n";

            switch (itemTypeSelector.GetSingleCheckedIndexName)
            {
                case "Bag":
                    tooltip += "Bag slots: " + itemBagSizeSetter.Value;
                    break;
                case "Consumable":
                    //TODO: Check what type and do stuff
                    tooltip += "Not finished yet xdd";
                    break;
                case "Equipment":
                    if(itemEquipmentMaterialSetter.Text != EquipmentData.MaterialType.None.ToString())
                    {
                        tooltip += itemEquipmentMaterialSetter.Text;
                        tooltip += "\n";
                    }
                    tooltip += ((EquipmentData)item).StatReport;
                    break;
                case "Weapon":
                    tooltip += ((WeaponData)item).StatReport;
                    tooltip += "\n";
                    tooltip += ((WeaponData)item).GetAttack;
                    break;
                case "None":
                    break;
                default:
                    throw new NotImplementedException();
            }


            MessageBox.Show(tooltip);
        }

        private void fileDownloadInstructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string instructions = "To download files that are shared in the chat:" + Environment.NewLine +
                Environment.NewLine +
                "1. Hover the message that contains the attachment and select the download icon." + Environment.NewLine +
                "2. Pick a destination on your computer when the save dialog appears." + Environment.NewLine +
                "3. After the download finishes, open the saved file from the chosen folder." + Environment.NewLine +
                "4. If the download is a compressed archive (.zip), extract it before importing it into the game.";

            MessageBox.Show(instructions, "File Download Instructions", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
