using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SPHMMaker
{
    /// <summary>
    /// Provides a simple sprite painting experience with common tools and file management helpers.
    /// </summary>
    public partial class SpriteEditorForm : Form
    {
        internal enum SpriteTool
        {
            Brush,
            Eraser,
            Fill,
            Line,
            Rectangle,
            Ellipse,
            ColorPicker
        }

        private readonly MenuStrip menuStrip;
        private readonly ToolStrip toolStrip;
        private readonly StatusStrip statusStrip;
        private readonly ToolStripStatusLabel positionLabel;
        private readonly ToolStripStatusLabel zoomLabel;
        private readonly ToolStripStatusLabel toolLabel;
        private readonly Panel contentPanel;
        private readonly Panel sidebar;
        private readonly Panel primaryPreview;
        private readonly Panel secondaryPreview;
        private readonly Button swapButton;
        private readonly FlowLayoutPanel palettePanel;
        private readonly NumericUpDown brushSizeSelector;
        private readonly Panel canvasHost;
        private readonly PictureBox canvasBox;

        private readonly Stack<Bitmap> undoStack = new Stack<Bitmap>();
        private readonly Stack<Bitmap> redoStack = new Stack<Bitmap>();

        private Bitmap workingBitmap;
        private string currentFilePath = string.Empty;
        private SpriteTool activeTool = SpriteTool.Brush;
        private Color primaryColor = Color.Black;
        private Color secondaryColor = Color.White;
        private float zoomLevel = 16f;
        private bool showGrid = true;

        private bool isDrawing;
        private bool drawingWithRightMouse;
        private Point dragStart;
        private Point lastPoint;
        private Point currentPoint;

        public SpriteEditorForm()
        {
            menuStrip = new MenuStrip();
            toolStrip = new ToolStrip();
            statusStrip = new StatusStrip();
            positionLabel = new ToolStripStatusLabel("Pos: -,-");
            zoomLabel = new ToolStripStatusLabel("Zoom: 1600%");
            toolLabel = new ToolStripStatusLabel("Tool: Brush");
            contentPanel = new Panel();
            sidebar = new Panel();
            primaryPreview = CreateColorPreviewPanel("P", (_, _) => SelectCustomColor(false));
            secondaryPreview = CreateColorPreviewPanel("S", (_, _) => SelectCustomColor(true));
            swapButton = new Button();
            palettePanel = new FlowLayoutPanel();
            brushSizeSelector = new NumericUpDown();
            canvasHost = new Panel();
            canvasBox = new PictureBox();

            workingBitmap = new Bitmap(64, 64, PixelFormat.Format32bppArgb);

            InitializeComponent();
            UpdateCanvasSize();
            RedrawCanvas();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposeHistory(undoStack);
                DisposeHistory(redoStack);
                if (workingBitmap != null)
                {
                    workingBitmap.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            Text = "Sprite Editor";
            ClientSize = new Size(1024, 720);
            MinimumSize = new Size(800, 600);

            InitializeMenuStrip();
            InitializeToolStrip();
            InitializeStatusStrip();
            InitializeSidebar();
            InitializeCanvas();
            InitializeContentLayout();

            Controls.Add(contentPanel);
            Controls.Add(statusStrip);
            Controls.Add(toolStrip);
            Controls.Add(menuStrip);

            MainMenuStrip = menuStrip;

            ResumeLayout(false);
            PerformLayout();
        }

        private void InitializeContentLayout()
        {
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Padding = new Padding(0, 0, 0, 0);

            contentPanel.Controls.Add(canvasHost);
            contentPanel.Controls.Add(sidebar);
        }

        private void InitializeMenuStrip()
        {
            var fileMenu = new ToolStripMenuItem("File");
            var newItem = new ToolStripMenuItem("New", null, NewSpriteRequested) { ShortcutKeys = Keys.Control | Keys.N };
            var openItem = new ToolStripMenuItem("Open...", null, OpenSpriteRequested) { ShortcutKeys = Keys.Control | Keys.O };
            var saveItem = new ToolStripMenuItem("Save", null, SaveSpriteRequested) { ShortcutKeys = Keys.Control | Keys.S };
            var saveAsItem = new ToolStripMenuItem("Save As...", null, SaveSpriteAsRequested)
            {
                ShortcutKeys = Keys.Control | Keys.Shift | Keys.S
            };
            var exportItem = new ToolStripMenuItem("Export PNG", null, ExportSpriteRequested)
            {
                ShortcutKeys = Keys.Control | Keys.E
            };
            var closeItem = new ToolStripMenuItem("Close", null, (_, _) => Close())
            {
                ShortcutKeys = Keys.Alt | Keys.F4
            };
            fileMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                newItem,
                openItem,
                new ToolStripSeparator(),
                saveItem,
                saveAsItem,
                exportItem,
                new ToolStripSeparator(),
                closeItem
            });

            var editMenu = new ToolStripMenuItem("Edit");
            var undoItem = new ToolStripMenuItem("Undo", null, (_, _) => Undo()) { ShortcutKeys = Keys.Control | Keys.Z };
            var redoItem = new ToolStripMenuItem("Redo", null, (_, _) => Redo()) { ShortcutKeys = Keys.Control | Keys.Y };
            var clearItem = new ToolStripMenuItem("Clear", null, (_, _) => ClearCanvas())
            {
                ShortcutKeys = Keys.Control | Keys.Delete
            };
            editMenu.DropDownOpened += (_, _) =>
            {
                undoItem.Enabled = undoStack.Count > 0;
                redoItem.Enabled = redoStack.Count > 0;
            };
            editMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                undoItem,
                redoItem,
                new ToolStripSeparator(),
                clearItem
            });

            var viewMenu = new ToolStripMenuItem("View");
            viewMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem("Zoom In", null, (_, _) => ChangeZoom(1.25f)) { ShortcutKeys = Keys.Control | Keys.Add },
                new ToolStripMenuItem("Zoom Out", null, (_, _) => ChangeZoom(0.8f)) { ShortcutKeys = Keys.Control | Keys.Subtract },
                new ToolStripMenuItem("Reset Zoom", null, (_, _) => ResetZoom()) { ShortcutKeys = Keys.Control | Keys.D0 },
                new ToolStripSeparator(),
                new ToolStripMenuItem("Toggle Grid", null, (_, _) => ToggleGrid()) { ShortcutKeys = Keys.Control | Keys.G },
                new ToolStripSeparator(),
                new ToolStripMenuItem("Select Tool...", null, OpenToolSelectorDialog)
            });

            var imageMenu = new ToolStripMenuItem("Image");
            imageMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem("Flip Horizontal", null, (_, _) => FlipHorizontal()),
                new ToolStripMenuItem("Flip Vertical", null, (_, _) => FlipVertical()),
                new ToolStripMenuItem("Rotate 90° Clockwise", null, (_, _) => RotateSprite(RotateFlipType.Rotate90FlipNone)),
                new ToolStripMenuItem("Rotate 90° Counter-Clockwise", null, (_, _) => RotateSprite(RotateFlipType.Rotate270FlipNone))
            });

            menuStrip.Items.AddRange(new ToolStripItem[] { fileMenu, editMenu, viewMenu, imageMenu });
        }

        private void InitializeToolStrip()
        {
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip.Dock = DockStyle.Top;

            toolStrip.Items.Add(CreateToolButton("Brush", SpriteTool.Brush));
            toolStrip.Items.Add(CreateToolButton("Eraser", SpriteTool.Eraser));
            toolStrip.Items.Add(CreateToolButton("Fill", SpriteTool.Fill));
            toolStrip.Items.Add(CreateToolButton("Line", SpriteTool.Line));
            toolStrip.Items.Add(CreateToolButton("Rectangle", SpriteTool.Rectangle));
            toolStrip.Items.Add(CreateToolButton("Ellipse", SpriteTool.Ellipse));
            toolStrip.Items.Add(CreateToolButton("Color Picker", SpriteTool.ColorPicker));
            toolStrip.Items.Add(new ToolStripSeparator());

            toolStrip.Items.Add(new ToolStripLabel("Brush Size:"));
            brushSizeSelector.Minimum = 1;
            brushSizeSelector.Maximum = 64;
            brushSizeSelector.Value = 1;
            brushSizeSelector.Width = 60;
            brushSizeSelector.ValueChanged += (_, _) => UpdateToolLabel();
            toolStrip.Items.Add(new ToolStripControlHost(brushSizeSelector));
        }

        private ToolStripItem CreateToolButton(string text, SpriteTool tool)
        {
            var button = new ToolStripButton(text)
            {
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                Tag = tool,
                CheckOnClick = true,
                Checked = tool == activeTool
            };
            button.Click += (_, _) => SelectTool(tool);
            return button;
        }

        private void InitializeStatusStrip()
        {
            statusStrip.Dock = DockStyle.Bottom;
            statusStrip.Items.Add(positionLabel);
            statusStrip.Items.Add(new ToolStripStatusLabel { Spring = true });
            statusStrip.Items.Add(zoomLabel);
            statusStrip.Items.Add(toolLabel);
        }

        private void InitializeSidebar()
        {
            sidebar.Dock = DockStyle.Left;
            sidebar.Width = 220;
            sidebar.Padding = new Padding(12);
            sidebar.BackColor = SystemColors.ControlLight;

            primaryPreview.BackColor = primaryColor;

            secondaryPreview.BackColor = secondaryColor;

            swapButton.Text = "Swap";
            swapButton.Width = 64;
            swapButton.Height = 32;
            swapButton.Margin = new Padding(0, 8, 0, 8);
            swapButton.AutoSize = true;
            swapButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            swapButton.Click += (_, _) => SwapColors();

            palettePanel.Dock = DockStyle.Fill;
            palettePanel.FlowDirection = FlowDirection.LeftToRight;
            palettePanel.WrapContents = true;
            palettePanel.AutoScroll = true;
            palettePanel.Margin = new Padding(0);
            palettePanel.Padding = new Padding(0);

            foreach (Color color in GetDefaultPalette())
            {
                palettePanel.Controls.Add(CreatePaletteButton(color));
            }

            var instructions = new Label
            {
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0),
                Padding = new Padding(0, 0, 0, 8),
                MaximumSize = new Size(180, 0),
                Dock = DockStyle.Top,
                Text = "Left click sets Primary\nRight click sets Secondary"
            };

            var colorSelectionPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = false,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            colorSelectionPanel.Controls.Add(primaryPreview);
            colorSelectionPanel.Controls.Add(secondaryPreview);
            colorSelectionPanel.Controls.Add(swapButton);
            colorSelectionPanel.Padding = new Padding(0, 0, 0, 8);

            var paletteContainer = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 260,
                Margin = new Padding(0),
                Padding = new Padding(0, 12, 0, 0)
            };

            paletteContainer.Controls.Add(palettePanel);
            paletteContainer.Controls.Add(instructions);
            instructions.BringToFront();

            sidebar.Controls.Add(paletteContainer);
            sidebar.Controls.Add(colorSelectionPanel);
        }

        private void InitializeCanvas()
        {
            canvasHost.Dock = DockStyle.Fill;
            canvasHost.AutoScroll = true;
            canvasHost.BackColor = Color.DimGray;

            canvasBox.BackColor = Color.Transparent;
            canvasBox.TabStop = true;
            canvasBox.Paint += CanvasBox_Paint;
            canvasBox.MouseDown += CanvasBox_MouseDown;
            canvasBox.MouseMove += CanvasBox_MouseMove;
            canvasBox.MouseUp += CanvasBox_MouseUp;
            canvasBox.MouseLeave += (_, _) =>
            {
                positionLabel.Text = "Pos: -,-";
                isDrawing = false;
            };
            canvasBox.MouseEnter += (_, _) => canvasBox.Focus();
            canvasBox.MouseWheel += CanvasMouseWheel;

            canvasHost.Controls.Add(canvasBox);
            canvasHost.MouseWheel += CanvasMouseWheel;
        }

        private static IEnumerable<Color> GetDefaultPalette()
        {
            return new[]
            {
                Color.FromArgb(255, 26, 28, 44),
                Color.FromArgb(255, 45, 55, 72),
                Color.FromArgb(255, 77, 93, 133),
                Color.FromArgb(255, 148, 176, 194),
                Color.FromArgb(255, 236, 244, 243),
                Color.FromArgb(255, 72, 28, 36),
                Color.FromArgb(255, 123, 31, 44),
                Color.FromArgb(255, 190, 58, 52),
                Color.FromArgb(255, 235, 125, 87),
                Color.FromArgb(255, 255, 203, 117),
                Color.FromArgb(255, 78, 52, 32),
                Color.FromArgb(255, 139, 94, 60),
                Color.FromArgb(255, 201, 143, 92),
                Color.FromArgb(255, 243, 201, 150),
                Color.FromArgb(255, 255, 236, 209),
                Color.FromArgb(255, 20, 75, 46),
                Color.FromArgb(255, 38, 134, 70),
                Color.FromArgb(255, 98, 184, 82),
                Color.FromArgb(255, 167, 224, 100),
                Color.FromArgb(255, 211, 241, 152),
                Color.FromArgb(255, 14, 84, 99),
                Color.FromArgb(255, 30, 129, 149),
                Color.FromArgb(255, 46, 175, 186),
                Color.FromArgb(255, 112, 218, 222),
                Color.FromArgb(255, 182, 240, 240),
                Color.FromArgb(255, 24, 66, 137),
                Color.FromArgb(255, 41, 118, 184),
                Color.FromArgb(255, 68, 162, 220),
                Color.FromArgb(255, 126, 195, 255),
                Color.FromArgb(255, 193, 223, 255)
            };
        }

        private Panel CreateColorPreviewPanel(string labelText, EventHandler clickHandler)
        {
            var panel = new Panel
            {
                Size = new Size(64, 64),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 0, 8)
            };

            var label = new Label
            {
                Dock = DockStyle.Fill,
                Text = labelText,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(FontFamily.GenericSansSerif, 24f, FontStyle.Bold),
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            panel.Controls.Add(label);
            panel.Click += clickHandler;
            label.Click += clickHandler;

            return panel;
        }

        private Button CreatePaletteButton(Color color)
        {
            var button = new Button
            {
                BackColor = color,
                Width = 32,
                Height = 32,
                Margin = new Padding(2)
            };
            button.MouseDown += (_, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    SetPrimaryColor(color);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    SetSecondaryColor(color);
                }
            };
            return button;
        }

        private void CanvasBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Transparent);
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.SmoothingMode = SmoothingMode.None;

            DrawCheckerboard(e.Graphics);
            e.Graphics.DrawImage(workingBitmap, new RectangleF(PointF.Empty, canvasBox.Size),
                new RectangleF(0, 0, workingBitmap.Width, workingBitmap.Height), GraphicsUnit.Pixel);

            if (isDrawing && (activeTool == SpriteTool.Line || activeTool == SpriteTool.Rectangle || activeTool == SpriteTool.Ellipse))
            {
                Rectangle previewRect = GetNormalizedRectangle(dragStart, currentPoint);
                Color previewColor = drawingWithRightMouse ? secondaryColor : primaryColor;
                using (var previewBrush = new SolidBrush(previewColor))
                using (var previewPen = new Pen(previewColor, ClampFloat(brushSizeSelector.Value, 1f, 64f)))
                {
                    switch (activeTool)
                    {
                        case SpriteTool.Line:
                            e.Graphics.DrawLine(previewPen, ScalePoint(dragStart), ScalePoint(currentPoint));
                            break;
                        case SpriteTool.Rectangle:
                            e.Graphics.FillRectangle(previewBrush, ScaleRectangle(previewRect));
                            break;
                        case SpriteTool.Ellipse:
                            e.Graphics.FillEllipse(previewBrush, ScaleRectangle(previewRect));
                            break;
                    }
                }
            }

            if (showGrid && zoomLevel >= 4)
            {
                using (var gridPen = new Pen(Color.FromArgb(40, Color.Black)))
                {
                    for (int x = 0; x <= workingBitmap.Width; x++)
                    {
                        float pos = x * zoomLevel;
                        e.Graphics.DrawLine(gridPen, pos, 0, pos, workingBitmap.Height * zoomLevel);
                    }

                    for (int y = 0; y <= workingBitmap.Height; y++)
                    {
                        float pos = y * zoomLevel;
                        e.Graphics.DrawLine(gridPen, 0, pos, workingBitmap.Width * zoomLevel, pos);
                    }
                }
            }
        }

        private void DrawCheckerboard(Graphics graphics)
        {
            using (var light = new SolidBrush(Color.FromArgb(220, 220, 220)))
            using (var dark = new SolidBrush(Color.FromArgb(180, 180, 180)))
            {
                for (int y = 0; y < workingBitmap.Height; y++)
                {
                    for (int x = 0; x < workingBitmap.Width; x++)
                    {
                        RectangleF rect = new RectangleF(x * zoomLevel, y * zoomLevel, zoomLevel, zoomLevel);
                        graphics.FillRectangle(((x + y) % 2 == 0) ? light : dark, rect);
                    }
                }
            }
        }

        private void CanvasBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right)
            {
                return;
            }

            Point pixel = ScreenToPixel(e.Location);
            if (!IsInside(pixel))
            {
                return;
            }

            if (activeTool != SpriteTool.ColorPicker)
            {
                PushUndo();
            }

            isDrawing = activeTool != SpriteTool.ColorPicker && activeTool != SpriteTool.Fill;
            drawingWithRightMouse = e.Button == MouseButtons.Right;
            dragStart = pixel;
            lastPoint = pixel;
            currentPoint = pixel;

            switch (activeTool)
            {
                case SpriteTool.Brush:
                    PaintPixel(pixel, drawingWithRightMouse);
                    break;
                case SpriteTool.Eraser:
                    ErasePixel(pixel);
                    break;
                case SpriteTool.Fill:
                    FloodFill(pixel, drawingWithRightMouse);
                    break;
                case SpriteTool.Line:
                case SpriteTool.Rectangle:
                case SpriteTool.Ellipse:
                    break;
                case SpriteTool.ColorPicker:
                    SampleColor(pixel, drawingWithRightMouse);
                    break;
            }

            RedrawCanvas();
        }

        private void CanvasBox_MouseMove(object sender, MouseEventArgs e)
        {
            Point pixel = ScreenToPixel(e.Location);
            if (IsInside(pixel))
            {
                positionLabel.Text = string.Format("Pos: {0}, {1}", pixel.X, pixel.Y);
            }
            else
            {
                positionLabel.Text = "Pos: -,-";
            }

            if (!isDrawing)
            {
                return;
            }

            pixel = ClampPoint(pixel);
            currentPoint = pixel;

            switch (activeTool)
            {
                case SpriteTool.Brush:
                    DrawBrushStroke(pixel, drawingWithRightMouse);
                    break;
                case SpriteTool.Eraser:
                    DrawEraseStroke(pixel);
                    break;
                case SpriteTool.Line:
                case SpriteTool.Rectangle:
                case SpriteTool.Ellipse:
                    RedrawCanvas();
                    break;
            }
        }

        private void CanvasBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDrawing)
            {
                return;
            }

            Point pixel = ScreenToPixel(e.Location);
            pixel = ClampPoint(pixel);
            currentPoint = pixel;

            switch (activeTool)
            {
                case SpriteTool.Line:
                    DrawLineShape(dragStart, pixel, drawingWithRightMouse);
                    break;
                case SpriteTool.Rectangle:
                    DrawRectangleShape(dragStart, pixel, drawingWithRightMouse);
                    break;
                case SpriteTool.Ellipse:
                    DrawEllipseShape(dragStart, pixel, drawingWithRightMouse);
                    break;
            }

            isDrawing = false;
            RedrawCanvas();
        }

        private void DrawBrushStroke(Point point, bool useSecondary)
        {
            using (Graphics graphics = Graphics.FromImage(workingBitmap))
            using (Pen pen = new Pen(useSecondary ? secondaryColor : primaryColor, ClampFloat(brushSizeSelector.Value, 1f, 64f)))
            {
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                graphics.CompositingMode = CompositingMode.SourceOver;
                pen.StartCap = LineCap.Square;
                pen.EndCap = LineCap.Square;
                graphics.DrawLine(pen, lastPoint, point);
            }

            lastPoint = point;
            RedrawCanvas();
        }

        private void DrawEraseStroke(Point point)
        {
            using (Graphics graphics = Graphics.FromImage(workingBitmap))
            using (Pen pen = new Pen(Color.Transparent, ClampFloat(brushSizeSelector.Value, 1f, 64f)))
            {
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                pen.StartCap = LineCap.Square;
                pen.EndCap = LineCap.Square;
                graphics.DrawLine(pen, lastPoint, point);
            }

            lastPoint = point;
            RedrawCanvas();
        }

        private void PaintPixel(Point point, bool useSecondary)
        {
            using (Graphics graphics = Graphics.FromImage(workingBitmap))
            using (SolidBrush brush = new SolidBrush(useSecondary ? secondaryColor : primaryColor))
            {
                graphics.CompositingMode = CompositingMode.SourceOver;
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                int size = (int)ClampFloat(brushSizeSelector.Value, 1f, 64f);
                Rectangle rect = new Rectangle(point.X - size / 2, point.Y - size / 2, size, size);
                graphics.FillRectangle(brush, rect);
            }

            lastPoint = point;
        }

        private void ErasePixel(Point point)
        {
            using (Graphics graphics = Graphics.FromImage(workingBitmap))
            using (SolidBrush brush = new SolidBrush(Color.Transparent))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                int size = (int)ClampFloat(brushSizeSelector.Value, 1f, 64f);
                Rectangle rect = new Rectangle(point.X - size / 2, point.Y - size / 2, size, size);
                graphics.FillRectangle(brush, rect);
            }

            lastPoint = point;
        }

        private void DrawLineShape(Point start, Point end, bool useSecondary)
        {
            using (Graphics graphics = Graphics.FromImage(workingBitmap))
            using (Pen pen = new Pen(useSecondary ? secondaryColor : primaryColor, ClampFloat(brushSizeSelector.Value, 1f, 64f)))
            {
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                graphics.CompositingMode = CompositingMode.SourceOver;
                pen.StartCap = LineCap.Square;
                pen.EndCap = LineCap.Square;
                graphics.DrawLine(pen, start, end);
            }
        }

        private void DrawRectangleShape(Point start, Point end, bool useSecondary)
        {
            Rectangle rect = GetNormalizedRectangle(start, end);
            using (Graphics graphics = Graphics.FromImage(workingBitmap))
            using (SolidBrush brush = new SolidBrush(useSecondary ? secondaryColor : primaryColor))
            {
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                graphics.CompositingMode = CompositingMode.SourceOver;
                graphics.FillRectangle(brush, rect);
            }
        }

        private void DrawEllipseShape(Point start, Point end, bool useSecondary)
        {
            Rectangle rect = GetNormalizedRectangle(start, end);
            using (Graphics graphics = Graphics.FromImage(workingBitmap))
            using (SolidBrush brush = new SolidBrush(useSecondary ? secondaryColor : primaryColor))
            {
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                graphics.CompositingMode = CompositingMode.SourceOver;
                graphics.FillEllipse(brush, rect);
            }
        }

        private void FloodFill(Point start, bool useSecondary)
        {
            Color target = workingBitmap.GetPixel(start.X, start.Y);
            Color replacement = useSecondary ? secondaryColor : primaryColor;
            if (target.ToArgb() == replacement.ToArgb())
            {
                return;
            }

            Rectangle bounds = new Rectangle(0, 0, workingBitmap.Width, workingBitmap.Height);
            bool[,] visited = new bool[workingBitmap.Width, workingBitmap.Height];
            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                Point current = queue.Dequeue();
                if (!bounds.Contains(current))
                {
                    continue;
                }

                if (visited[current.X, current.Y])
                {
                    continue;
                }

                if (workingBitmap.GetPixel(current.X, current.Y).ToArgb() != target.ToArgb())
                {
                    continue;
                }

                workingBitmap.SetPixel(current.X, current.Y, replacement);
                visited[current.X, current.Y] = true;

                queue.Enqueue(new Point(current.X + 1, current.Y));
                queue.Enqueue(new Point(current.X - 1, current.Y));
                queue.Enqueue(new Point(current.X, current.Y + 1));
                queue.Enqueue(new Point(current.X, current.Y - 1));
            }
        }

        private void SampleColor(Point point, bool assignSecondary)
        {
            Color sampled = workingBitmap.GetPixel(point.X, point.Y);
            if (assignSecondary)
            {
                SetSecondaryColor(sampled);
            }
            else
            {
                SetPrimaryColor(sampled);
            }
        }

        private void SelectCustomColor(bool secondary)
        {
            using (ColorDialog dialog = new ColorDialog())
            {
                dialog.AllowFullOpen = true;
                dialog.FullOpen = true;
                dialog.Color = secondary ? secondaryColor : primaryColor;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (secondary)
                    {
                        SetSecondaryColor(dialog.Color);
                    }
                    else
                    {
                        SetPrimaryColor(dialog.Color);
                    }
                }
            }
        }

        private void SetPrimaryColor(Color color)
        {
            primaryColor = color;
            primaryPreview.BackColor = color;
            UpdateToolLabel();
        }

        private void SetSecondaryColor(Color color)
        {
            secondaryColor = color;
            secondaryPreview.BackColor = color;
            UpdateToolLabel();
        }

        private void SwapColors()
        {
            Color temp = primaryColor;
            primaryColor = secondaryColor;
            secondaryColor = temp;
            primaryPreview.BackColor = primaryColor;
            secondaryPreview.BackColor = secondaryColor;
        }

        private void NewSpriteRequested(object sender, EventArgs e)
        {
            using (SpriteSizeDialog dialog = new SpriteSizeDialog(workingBitmap.Width, workingBitmap.Height))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ReplaceBitmap(new Bitmap(dialog.SpriteWidth, dialog.SpriteHeight, PixelFormat.Format32bppArgb));
                    currentFilePath = string.Empty;
                    ClearUndoHistory();
                }
            }
        }

        private void OpenSpriteRequested(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image Files|*.png;*.bmp;*.gif;*.jpg;*.jpeg|All Files|*.*";
                dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap loaded = new Bitmap(dialog.FileName))
                    {
                        ReplaceBitmap(new Bitmap(loaded));
                        currentFilePath = dialog.FileName;
                        ClearUndoHistory();
                    }
                }
            }
        }

        private void SaveSpriteRequested(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentFilePath))
            {
                SaveSpriteAsRequested(sender, e);
                return;
            }

            workingBitmap.Save(currentFilePath);
        }

        private void SaveSpriteAsRequested(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "PNG Image|*.png|Bitmap Image|*.bmp";
                dialog.DefaultExt = "png";
                dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = dialog.FileName;
                    workingBitmap.Save(currentFilePath);
                }
            }
        }

        private void ExportSpriteRequested(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "PNG Image|*.png";
                dialog.DefaultExt = "png";
                dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    workingBitmap.Save(dialog.FileName, ImageFormat.Png);
                }
            }
        }

        private void ClearCanvas()
        {
            PushUndo();
            using (Graphics graphics = Graphics.FromImage(workingBitmap))
            {
                graphics.Clear(Color.Transparent);
            }

            RedrawCanvas();
        }

        private void FlipHorizontal()
        {
            PushUndo();
            workingBitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
            RedrawCanvas();
        }

        private void FlipVertical()
        {
            PushUndo();
            workingBitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            RedrawCanvas();
        }

        private void RotateSprite(RotateFlipType type)
        {
            PushUndo();
            workingBitmap.RotateFlip(type);
            UpdateCanvasSize();
            RedrawCanvas();
        }

        private void ToggleGrid()
        {
            showGrid = !showGrid;
            RedrawCanvas();
        }

        private void ChangeZoom(float multiplier)
        {
            zoomLevel = ClampFloat(zoomLevel * multiplier, 1f, 128f);
            zoomLabel.Text = string.Format("Zoom: {0}%", Math.Round(zoomLevel * 100));
            UpdateCanvasSize();
        }

        private void ResetZoom()
        {
            zoomLevel = 16f;
            zoomLabel.Text = "Zoom: 1600%";
            UpdateCanvasSize();
        }

        private void OpenToolSelectorDialog(object sender, EventArgs e)
        {
            using var dialog = new SpriteToolSelectorDialog(activeTool);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                SelectTool(dialog.SelectedTool);
            }
        }

        private void SelectTool(SpriteTool tool)
        {
            activeTool = tool;
            foreach (ToolStripItem item in toolStrip.Items)
            {
                if (item is ToolStripButton button && button.Tag is SpriteTool buttonTool)
                {
                    button.Checked = buttonTool == tool;
                }
            }

            UpdateToolLabel();
        }

        private void UpdateToolLabel()
        {
            toolLabel.Text = string.Format("Tool: {0} (Size {1})", activeTool, (int)brushSizeSelector.Value);
        }

        private void Undo()
        {
            if (undoStack.Count == 0)
            {
                return;
            }

            redoStack.Push((Bitmap)workingBitmap.Clone());
            workingBitmap.Dispose();
            workingBitmap = undoStack.Pop();
            UpdateCanvasSize();
            RedrawCanvas();
        }

        private void Redo()
        {
            if (redoStack.Count == 0)
            {
                return;
            }

            undoStack.Push((Bitmap)workingBitmap.Clone());
            workingBitmap.Dispose();
            workingBitmap = redoStack.Pop();
            UpdateCanvasSize();
            RedrawCanvas();
        }

        private void PushUndo()
        {
            undoStack.Push((Bitmap)workingBitmap.Clone());
            DisposeHistory(redoStack);
            redoStack.Clear();
        }

        private void ClearUndoHistory()
        {
            DisposeHistory(undoStack);
            undoStack.Clear();
            DisposeHistory(redoStack);
            redoStack.Clear();
        }

        private static void DisposeHistory(IEnumerable<Bitmap> history)
        {
            foreach (Bitmap bitmap in history)
            {
                bitmap.Dispose();
            }
        }

        private void ReplaceBitmap(Bitmap bitmap)
        {
            workingBitmap.Dispose();
            workingBitmap = bitmap;
            UpdateCanvasSize();
            RedrawCanvas();
        }

        private void UpdateCanvasSize()
        {
            canvasBox.Size = new Size((int)(workingBitmap.Width * zoomLevel), (int)(workingBitmap.Height * zoomLevel));
            canvasHost.AutoScrollMinSize = canvasBox.Size;
        }

        private void CanvasMouseWheel(object sender, MouseEventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                return;
            }

            Point location = e.Location;
            if (sender is Control control && control != canvasBox)
            {
                Point screenPoint = control.PointToScreen(location);
                location = canvasBox.PointToClient(screenPoint);
            }

            Point scrollPosition = canvasHost.AutoScrollPosition;
            float imageX = (-scrollPosition.X + location.X) / zoomLevel;
            float imageY = (-scrollPosition.Y + location.Y) / zoomLevel;

            float multiplier = e.Delta > 0 ? 1.25f : 0.8f;
            float previousZoom = zoomLevel;
            ChangeZoom(multiplier);

            if (Math.Abs(previousZoom - zoomLevel) > float.Epsilon)
            {
                int targetX = (int)Math.Round(imageX * zoomLevel - location.X);
                int targetY = (int)Math.Round(imageY * zoomLevel - location.Y);
                canvasHost.AutoScrollPosition = new Point(Math.Max(0, targetX), Math.Max(0, targetY));
            }

            if (e is HandledMouseEventArgs handled)
            {
                handled.Handled = true;
            }
        }

        private void RedrawCanvas()
        {
            canvasBox.Invalidate();
        }

        private Point ScreenToPixel(Point location)
        {
            int x = (int)Math.Floor(location.X / zoomLevel);
            int y = (int)Math.Floor(location.Y / zoomLevel);
            return new Point(x, y);
        }

        private bool IsInside(Point point)
        {
            return point.X >= 0 && point.Y >= 0 && point.X < workingBitmap.Width && point.Y < workingBitmap.Height;
        }

        private Point ClampPoint(Point point)
        {
            return new Point(ClampInt(point.X, 0, workingBitmap.Width - 1), ClampInt(point.Y, 0, workingBitmap.Height - 1));
        }

        private static int ClampInt(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        private static float ClampFloat(decimal value, float min, float max)
        {
            float floatValue = (float)value;
            if (floatValue < min)
            {
                return min;
            }

            if (floatValue > max)
            {
                return max;
            }

            return floatValue;
        }

        private static float ClampFloat(float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        private Rectangle GetNormalizedRectangle(Point start, Point end)
        {
            int x1 = Math.Min(start.X, end.X);
            int y1 = Math.Min(start.Y, end.Y);
            int x2 = Math.Max(start.X, end.X);
            int y2 = Math.Max(start.Y, end.Y);
            return new Rectangle(x1, y1, Math.Max(1, x2 - x1 + 1), Math.Max(1, y2 - y1 + 1));
        }

        private PointF ScalePoint(Point point)
        {
            return new PointF(point.X * zoomLevel, point.Y * zoomLevel);
        }

        private RectangleF ScaleRectangle(Rectangle rectangle)
        {
            return new RectangleF(rectangle.X * zoomLevel, rectangle.Y * zoomLevel, rectangle.Width * zoomLevel, rectangle.Height * zoomLevel);
        }
    }
}
