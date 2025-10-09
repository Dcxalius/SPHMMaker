using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SPHMMaker.Items;

namespace SPHMMaker
{
    public partial class MainForm
    {
        Image CreateDefaultItemImage()
        {
            var bitmap = new Bitmap(ItemImageSize, ItemImageSize);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.FromArgb(32, 32, 32));
                using var borderPen = new Pen(Color.FromArgb(96, 96, 96));
                graphics.DrawRectangle(borderPen, 0, 0, ItemImageSize - 1, ItemImageSize - 1);

                FontFamily fontFamily = SystemFonts.MessageBoxFont?.FontFamily ?? FontFamily.GenericSansSerif;
                using Font font = new Font(fontFamily, 18f, FontStyle.Bold, GraphicsUnit.Pixel);
                string text = "?";
                SizeF measured = graphics.MeasureString(text, font);
                float x = (ItemImageSize - measured.Width) / 2f;
                float y = (ItemImageSize - measured.Height) / 2f;
                using var textBrush = new SolidBrush(Color.FromArgb(224, 224, 224));
                graphics.DrawString(text, font, textBrush, x, y);
            }

            return bitmap;
        }

        void items_DrawItem(object? sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index < 0)
            {
                return;
            }

            ListBox listBox = sender as ListBox ?? items;
            if (listBox == null || e.Index >= listBox.Items.Count)
            {
                return;
            }

            object? rawItem = listBox.Items[e.Index];
            if (rawItem is not ItemData item)
            {
                using var fallbackBrush = new SolidBrush(e.ForeColor);
                Font fallbackFont = e.Font ?? SystemFonts.DefaultFont;
                e.Graphics.DrawString(rawItem?.ToString() ?? string.Empty, fallbackFont, fallbackBrush, e.Bounds);
                e.DrawFocusRectangle();
                return;
            }

            Rectangle imageRect = CalculateImageRectangle(e.Bounds);
            Image image = GetItemImage(item);

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.DrawImage(image, imageRect);

            Rectangle textRect = new Rectangle(
                imageRect.Right + ItemHorizontalPadding,
                e.Bounds.Y + ItemVerticalPadding,
                Math.Max(0, e.Bounds.Width - imageRect.Width - (ItemHorizontalPadding * 2)),
                e.Bounds.Height - (ItemVerticalPadding * 2));

            Color textColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected
                ? SystemColors.HighlightText
                : GetQualityColor(item.Quality);

            Font textFont = e.Font ?? SystemFonts.DefaultFont;
            TextRenderer.DrawText(
                e.Graphics,
                item.Name,
                textFont,
                textRect,
                textColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            e.DrawFocusRectangle();
        }

        Rectangle CalculateImageRectangle(Rectangle bounds)
        {
            return new Rectangle(
                bounds.X + ItemHorizontalPadding,
                bounds.Y + ItemVerticalPadding,
                ItemImageSize,
                ItemImageSize);
        }

        Image GetItemImage(ItemData item)
        {
            GfxPath? gfxPath = item.GfxPath;
            if (gfxPath is null)
            {
                return defaultItemImage;
            }

            if (string.IsNullOrWhiteSpace(gfxPath.Name))
            {
                return defaultItemImage;
            }

            string key = gfxPath.ToString();
            if (key.Length == 0)
            {
                return defaultItemImage;
            }

            if (itemImageCache.TryGetValue(key, out Image? cached) && cached != null)
            {
                return cached;
            }

            Image image = TryLoadImage(gfxPath) ?? defaultItemImage;
            itemImageCache[key] = image;
            return image;
        }

        Image? TryLoadImage(GfxPath gfxPath)
        {
            foreach (string candidate in GetCandidatePaths(gfxPath))
            {
                if (TryLoadImageCore(candidate, out Image? image))
                {
                    return image;
                }
            }

            return null;
        }

        IEnumerable<string> GetCandidatePaths(GfxPath gfxPath)
        {
            string name = gfxPath.Name;
            if (string.IsNullOrWhiteSpace(name))
            {
                yield break;
            }

            string typeSegment = gfxPath.Type.ToString();
            var baseDirectories = new List<string?>
            {
                datapackRootPath,
                datapackRootPath is null ? null : Path.Combine(datapackRootPath, "Gfx"),
                AppDomain.CurrentDomain.BaseDirectory,
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gfx"),
            };

            var unique = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            bool hasExtension = Path.HasExtension(name);

            foreach (string? baseDirectory in baseDirectories)
            {
                if (string.IsNullOrWhiteSpace(baseDirectory))
                {
                    continue;
                }

                string withType = Path.Combine(baseDirectory, typeSegment);
                foreach (string candidate in EnumerateCandidates(withType))
                {
                    if (unique.Add(candidate))
                    {
                        yield return candidate;
                    }
                }

                // Allow searching without the type segment as well.
                foreach (string candidate in EnumerateCandidates(baseDirectory))
                {
                    if (unique.Add(candidate))
                    {
                        yield return candidate;
                    }
                }
            }

            IEnumerable<string> EnumerateCandidates(string directory)
            {
                if (hasExtension)
                {
                    yield return Path.Combine(directory, name);
                }
                else
                {
                    foreach (string extension in SupportedImageExtensions)
                    {
                        yield return Path.Combine(directory, name + extension);
                    }
                }
            }
        }

        bool TryLoadImageCore(string path, [NotNullWhen(true)] out Image? image)
        {
            image = null;

            try
            {
                if (!File.Exists(path))
                {
                    return false;
                }

                using FileStream stream = File.OpenRead(path);
                using Image loaded = Image.FromStream(stream);
                image = (Image)loaded.Clone();
                return true;
            }
            catch
            {
                image?.Dispose();
                image = null;
                return false;
            }
        }

        Color GetQualityColor(ItemData.ItemQuality quality)
        {
            if (ItemQualityColors.TryGetValue(quality, out Color color))
            {
                return color;
            }

            return SystemColors.ControlText;
        }

        void DisposeItemImages()
        {
            if (imagesDisposed)
            {
                return;
            }

            imagesDisposed = true;

            foreach (Image image in itemImageCache.Values.Where(img => img != defaultItemImage))
            {
                image?.Dispose();
            }

            itemImageCache.Clear();
            defaultItemImage.Dispose();
        }
    }
}
