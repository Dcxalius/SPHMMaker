using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SPHMMaker
{
    public partial class SpriteEditorForm : Form
    {
        private enum ToolType
        {
            Pencil,
            Eraser,
            Fill,
            ColorPicker,
            Line,
            Rectangle,
            Ellipse
        }

        private readonly Dictionary<ToolType, ToolStripButton> toolButtons;
        private readonly Color[] defaultPalette =
        [
            Color.Black, Color.White, Color.Gray, Color.Silver, Color.Maroon, Color.Red, Color.Orange, Color.Yellow,
            Color.Olive, Color.Green, Color.Teal, Color.Cyan, Color.Blue, Color.Navy, Color.Purple, Color.Magenta,
            Color.Brown, Color.Gold, Color.Khaki, Color.Lime, Color.Aquamarine, Color.SkyBlue, Color.Indigo,
            Color.Pink, Color.Beige, Color.Coral, Color.DarkRed, Color.DarkOrange, Color.DarkGreen, Color.DarkSlateBlue,
            Color.LightGray, Color.LightCoral, Color.LightGreen, Color.LightBlue, Color.WhiteSmoke, Color.Black
        ];

        private Bitmap canvas;
        private Bitmap? overlay;
        private float zoom = 1f;
        private const int DefaultCanvasSize = 256;
        private ToolType currentTool = ToolType.Pencil;
        private Color primaryColor = Color.Black;
        private Color secondaryColor = Color.White;
        private int brushSize = 1;
        private bool fillShape = true;
        private bool showGrid = true;
        private bool isDrawing;
        private Point startPoint;
        private Point lastPoint;
        private Color currentStrokeColor = Color.Black;
        private readonly Stack<Bitmap> undoStack = new();
        private readonly Stack<Bitmap> redoStack = new();
        private readonly ColorDialog colorDialog = new();
        private string? currentFilePath;
        private bool hasUnsavedChanges;

        public SpriteEditorForm()
        {
            InitializeComponent();

            toolButtons = new Dictionary<ToolType, ToolStripButton>
            {
                { ToolType.Pencil, pencilToolStripButton },
                { ToolType.Eraser, eraserToolStripButton },
                { ToolType.Fill, fillToolStripButton },
                { ToolType.ColorPicker, colorPickerToolStripButton },
                { ToolType.Line, lineToolStripButton },
                { ToolType.Rectangle, rectangleToolStripButton },
                { ToolType.Ellipse, ellipseToolStripButton }
            };

            foreach (ToolStripButton button in toolButtons.Values)
            {
                button.CheckOnClick = true;
            }

            brushSizeComboBox.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "8", "12", "16", "24", "32" });
            brushSizeComboBox.SelectedIndex = 0;
            fillShapeToolStripButton.Checked = true;

            InitializeCanvas(DefaultCanvasSize, DefaultCanvasSize);
            PopulatePalette();
            UpdateToolSelection(ToolType.Pencil);
            UpdateColorPanels();
            UpdateStatusLabels(Point.Empty);
            RefreshCanvas();
        }

        #region Initialization Helpers

        private void InitializeCanvas(int width, int height)
        {
            canvas?.Dispose();
            canvas = new Bitmap(width, height);
            using var g = Graphics.FromImage(canvas);
            g.Clear(Color.Transparent);
            overlay?.Dispose();
            overlay = null;
            ClearStack(undoStack);
            ClearStack(redoStack);
            currentFilePath = null;
            hasUnsavedChanges = false;
            ResetZoom();
        }

        private void PopulatePalette()
        {
            colorPalettePanel.SuspendLayout();
            colorPalettePanel.Controls.Clear();
            foreach (Color color in defaultPalette)
            {
                var swatch = new Panel
                {
                    BackColor = color,
                    Width = 32,
                    Height = 32,
                    Margin = new Padding(6)
                };
                colorToolTip.SetToolTip(swatch, color.Name);
                swatch.Click += (_, args) =>
                {
                    MouseEventArgs? mouse = args as MouseEventArgs;
                    if (mouse != null && mouse.Button == MouseButtons.Right)
                    {
                        secondaryColor = color;
                    }
                    else
                    {
                        primaryColor = color;
                    }
                    UpdateColorPanels();
                };

                colorPalettePanel.Controls.Add(swatch);
            }
            colorPalettePanel.ResumeLayout();
        }

        private void UpdateColorPanels()
        {
            primaryColorPanel.BackColor = primaryColor;
            secondaryColorPanel.BackColor = secondaryColor;
        }

        #endregion

        #region Canvas Rendering

        private void RefreshCanvas()
        {
            canvasView.Size = new Size((int)(canvas.Width * zoom), (int)(canvas.Height * zoom));
            canvasView.Invalidate();
            statusZoomLabel.Text = $"Zoom: {(int)(zoom * 100)}%";
        }

        private static void DrawCheckerboard(Graphics graphics, Rectangle area, int cellSize)
        {
            using SolidBrush light = new(Color.FromArgb(240, 240, 240));
            using SolidBrush dark = new(Color.FromArgb(200, 200, 200));
            bool toggle = false;
            for (int y = area.Top; y < area.Bottom; y += cellSize)
            {
                toggle = !toggle;
                for (int x = area.Left; x < area.Right; x += cellSize)
                {
                    Rectangle cell = new(x, y, cellSize, cellSize);
                    graphics.FillRectangle(toggle ? light : dark, cell);
                    toggle = !toggle;
                }
            }
        }

        private Bitmap GetCompositeImage()
        {
            Bitmap composite = new(canvas.Width, canvas.Height);
            using Graphics g = Graphics.FromImage(composite);
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            DrawCheckerboard(g, new Rectangle(Point.Empty, canvas.Size), 16);
            g.DrawImage(canvas, Point.Empty);
            if (overlay != null)
            {
                g.DrawImage(overlay, Point.Empty);
            }
            return composite;
        }

        private void canvasView_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.SmoothingMode = SmoothingMode.None;

            using Bitmap composite = GetCompositeImage();
            e.Graphics.Clear(Color.Gray);
            e.Graphics.ScaleTransform(zoom, zoom);
            e.Graphics.DrawImage(composite, 0, 0);

            if (showGrid && zoom >= 4f)
            {
                using Pen gridPen = new(Color.FromArgb(120, Color.Black), 0f);
                for (int x = 0; x <= canvas.Width; x++)
                {
                    e.Graphics.DrawLine(gridPen, x, 0, x, canvas.Height);
                }
                for (int y = 0; y <= canvas.Height; y++)
                {
                    e.Graphics.DrawLine(gridPen, 0, y, canvas.Width, y);
                }
            }
        }

        #endregion

        #region Mouse Handling

        private void canvasView_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right)
            {
                return;
            }

            Point canvasPoint = ScreenToCanvas(e.Location);
            if (!IsWithinCanvas(canvasPoint))
            {
                return;
            }

            bool modifiesCanvas = currentTool != ToolType.ColorPicker;
            if (modifiesCanvas)
            {
                SaveUndoState();
                ClearStack(redoStack);
            }

            isDrawing = true;
            startPoint = canvasPoint;
            lastPoint = canvasPoint;
            currentStrokeColor = GetActiveColor(e.Button);

            if (currentTool == ToolType.Fill)
            {
                PerformFill(canvasPoint, currentStrokeColor);
                FinalizeDrawing();
            }
            else if (currentTool == ToolType.ColorPicker)
            {
                PickColor(canvasPoint, e.Button);
                isDrawing = false;
                hasUnsavedChanges = false;
            }
            else if (currentTool == ToolType.Pencil || currentTool == ToolType.Eraser)
            {
                DrawBrushStroke(canvasPoint, lastPoint, currentStrokeColor);
            }
            else
            {
                UpdateOverlayShape(canvasPoint);
            }

            RefreshCanvas();
        }

        private void canvasView_MouseMove(object? sender, MouseEventArgs e)
        {
            Point canvasPoint = ScreenToCanvas(e.Location);
            statusPositionLabel.Text = $"X:{Math.Clamp(canvasPoint.X, 0, canvas.Width - 1)} Y:{Math.Clamp(canvasPoint.Y, 0, canvas.Height - 1)}";

            if (!isDrawing)
            {
                return;
            }

            canvasPoint = ClampToCanvas(canvasPoint);

            if (currentTool == ToolType.Pencil || currentTool == ToolType.Eraser)
            {
                DrawBrushStroke(canvasPoint, lastPoint, currentStrokeColor);
                lastPoint = canvasPoint;
                RefreshCanvas();
            }
            else if (currentTool == ToolType.Line || currentTool == ToolType.Rectangle || currentTool == ToolType.Ellipse)
            {
                UpdateOverlayShape(canvasPoint);
                RefreshCanvas();
            }
        }

        private void canvasView_MouseUp(object? sender, MouseEventArgs e)
        {
            if (!isDrawing)
            {
                return;
            }

            Point canvasPoint = ScreenToCanvas(e.Location);
            canvasPoint = ClampToCanvas(canvasPoint);

            if (currentTool == ToolType.Line || currentTool == ToolType.Rectangle || currentTool == ToolType.Ellipse)
            {
                CommitOverlayShape(canvasPoint, currentStrokeColor);
            }

            FinalizeDrawing();
        }

        private void FinalizeDrawing()
        {
            overlay?.Dispose();
            overlay = null;
            isDrawing = false;
            hasUnsavedChanges = true;
            RefreshCanvas();
        }

        private void UpdateOverlayShape(Point currentPoint)
        {
            overlay?.Dispose();
            overlay = new Bitmap(canvas.Width, canvas.Height);

            using Graphics g = Graphics.FromImage(overlay);
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            Rectangle rect = GetRectangle(startPoint, currentPoint);
            using Brush brush = new SolidBrush(currentStrokeColor);
            using Pen pen = new Pen(currentStrokeColor, brushSize);

            switch (currentTool)
            {
                case ToolType.Line:
                    pen.StartCap = LineCap.Flat;
                    pen.EndCap = LineCap.Flat;
                    g.DrawLine(pen, startPoint, currentPoint);
                    break;
                case ToolType.Rectangle:
                    if (fillShape)
                    {
                        g.FillRectangle(brush, rect);
                    }
                    g.DrawRectangle(pen, rect);
                    break;
                case ToolType.Ellipse:
                    if (fillShape)
                    {
                        g.FillEllipse(brush, rect);
                    }
                    g.DrawEllipse(pen, rect);
                    break;
            }
        }

        private void CommitOverlayShape(Point currentPoint, Color color)
        {
            overlay ??= new Bitmap(canvas.Width, canvas.Height);
            using Graphics g = Graphics.FromImage(canvas);
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            Rectangle rect = GetRectangle(startPoint, currentPoint);
            using Brush brush = new SolidBrush(color);
            using Pen pen = new Pen(color, brushSize) { StartCap = LineCap.Flat, EndCap = LineCap.Flat };

            switch (currentTool)
            {
                case ToolType.Line:
                    g.DrawLine(pen, startPoint, currentPoint);
                    break;
                case ToolType.Rectangle:
                    if (fillShape)
                    {
                        g.FillRectangle(brush, rect);
                    }
                    g.DrawRectangle(pen, rect);
                    break;
                case ToolType.Ellipse:
                    if (fillShape)
                    {
                        g.FillEllipse(brush, rect);
                    }
                    g.DrawEllipse(pen, rect);
                    break;
            }
        }

        private void DrawBrushStroke(Point currentPoint, Point previousPoint, Color color)
        {
            using Graphics g = Graphics.FromImage(canvas);
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            using SolidBrush brush = new(color);
            using Pen pen = new(color, brushSize)
            {
                StartCap = LineCap.Square,
                EndCap = LineCap.Square
            };

            if (currentTool == ToolType.Eraser)
            {
                brush.Color = Color.Transparent;
                pen.Color = Color.Transparent;
                g.CompositingMode = CompositingMode.SourceCopy;
            }
            else
            {
                g.CompositingMode = CompositingMode.SourceOver;
            }

            if (previousPoint == currentPoint)
            {
                Rectangle rect = new(currentPoint.X - brushSize / 2, currentPoint.Y - brushSize / 2, brushSize, brushSize);
                g.FillRectangle(brush, rect);
            }
            else
            {
                g.DrawLine(pen, previousPoint, currentPoint);
            }
        }

        private void PerformFill(Point location, Color color)
        {
            Color targetColor = canvas.GetPixel(location.X, location.Y);
            if (color.ToArgb() == targetColor.ToArgb())
            {
                return;
            }

            Queue<Point> queue = new();
            queue.Enqueue(location);
            bool[,] visited = new bool[canvas.Width, canvas.Height];
            using Graphics g = Graphics.FromImage(canvas);
            g.CompositingMode = CompositingMode.SourceCopy;
            using SolidBrush brush = new(color);

            while (queue.Count > 0)
            {
                Point point = queue.Dequeue();
                if (!IsWithinCanvas(point))
                {
                    continue;
                }
                if (visited[point.X, point.Y])
                {
                    continue;
                }
                visited[point.X, point.Y] = true;
                if (canvas.GetPixel(point.X, point.Y).ToArgb() != targetColor.ToArgb())
                {
                    continue;
                }

                Rectangle fillRect = new(point.X, point.Y, 1, 1);
                g.FillRectangle(brush, fillRect);

                queue.Enqueue(new Point(point.X + 1, point.Y));
                queue.Enqueue(new Point(point.X - 1, point.Y));
                queue.Enqueue(new Point(point.X, point.Y + 1));
                queue.Enqueue(new Point(point.X, point.Y - 1));
            }
        }

        private void PickColor(Point location, MouseButtons button)
        {
            Color sampled = canvas.GetPixel(location.X, location.Y);
            if (button == MouseButtons.Right)
            {
                secondaryColor = sampled;
            }
            else
            {
                primaryColor = sampled;
            }
            UpdateColorPanels();
        }

        #endregion

        #region Utility Methods

        private static Rectangle GetRectangle(Point start, Point end)
        {
            int x = Math.Min(start.X, end.X);
            int y = Math.Min(start.Y, end.Y);
            int width = Math.Abs(start.X - end.X);
            int height = Math.Abs(start.Y - end.Y);
            width = Math.Max(1, width);
            height = Math.Max(1, height);
            return new Rectangle(x, y, width, height);
        }

        private Point ScreenToCanvas(Point location)
        {
            return new Point(
                (int)Math.Floor(location.X / zoom),
                (int)Math.Floor(location.Y / zoom));
        }

        private Point ClampToCanvas(Point location)
        {
            int x = Math.Clamp(location.X, 0, canvas.Width - 1);
            int y = Math.Clamp(location.Y, 0, canvas.Height - 1);
            return new Point(x, y);
        }

        private bool IsWithinCanvas(Point point)
        {
            return point.X >= 0 && point.Y >= 0 && point.X < canvas.Width && point.Y < canvas.Height;
        }

        private Color GetActiveColor(MouseButtons button)
        {
            if (currentTool == ToolType.Eraser)
            {
                return Color.Transparent;
            }
            return button == MouseButtons.Right ? secondaryColor : primaryColor;
        }

        private void SaveUndoState()
        {
            Bitmap snapshot = (Bitmap)canvas.Clone();
            undoStack.Push(snapshot);
            TrimStack(undoStack, 50);
        }

        private void RestoreFromStack(Stack<Bitmap> sourceStack, Stack<Bitmap> destinationStack)
        {
            if (sourceStack.Count == 0)
            {
                return;
            }

            destinationStack.Push((Bitmap)canvas.Clone());
            Bitmap snapshot = sourceStack.Pop();
            canvas.Dispose();
            canvas = (Bitmap)snapshot.Clone();
            snapshot.Dispose();
            overlay?.Dispose();
            overlay = null;
            RefreshCanvas();
            hasUnsavedChanges = true;
        }

        private void UpdateStatusLabels(Point location)
        {
            statusToolLabel.Text = $"Tool: {currentTool}";
            statusPositionLabel.Text = $"X:{location.X} Y:{location.Y}";
        }

        private void ResetZoom()
        {
            zoom = Math.Max(0.25f, Math.Min(8f, MathF.Round((float)canvasContainer.Width / canvas.Width, 2)));
            if (float.IsNaN(zoom) || zoom <= 0f)
            {
                zoom = 1f;
            }
            zoom = Math.Clamp(zoom, 0.25f, 8f);
            RefreshCanvas();
        }

        private void SetZoom(float newZoom)
        {
            zoom = Math.Clamp(newZoom, 0.25f, 16f);
            RefreshCanvas();
        }

        private DialogResult ConfirmDiscardChanges()
        {
            if (!hasUnsavedChanges)
            {
                return DialogResult.Yes;
            }

            return MessageBox.Show(this, "The current sprite has unsaved changes. Continue?", "Unsaved Changes",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        #endregion

        #region Toolstrip Events

        private void toolButton_Click(object? sender, EventArgs e)
        {
            if (sender is ToolStripButton button)
            {
                ToolType tool = toolButtons.First(pair => pair.Value == button).Key;
                UpdateToolSelection(tool);
            }
        }

        private void UpdateToolSelection(ToolType tool)
        {
            currentTool = tool;
            foreach ((ToolType toolType, ToolStripButton button) in toolButtons)
            {
                button.Checked = toolType == tool;
            }
            statusToolLabel.Text = $"Tool: {currentTool}";
        }

        private void brushSizeComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (int.TryParse(brushSizeComboBox.SelectedItem?.ToString(), out int size))
            {
                brushSize = size;
            }
        }

        private void fillShapeToolStripButton_CheckedChanged(object? sender, EventArgs e)
        {
            fillShape = fillShapeToolStripButton.Checked;
        }

        private void zoomPreset_Click(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item && item.Text.EndsWith('%'))
            {
                if (int.TryParse(item.Text.TrimEnd('%'), out int value))
                {
                    SetZoom(value / 100f);
                }
            }
        }

        private void zoomInToolStripMenuItem_Click(object? sender, EventArgs e) => SetZoom(zoom * 1.25f);
        private void zoomOutToolStripMenuItem_Click(object? sender, EventArgs e) => SetZoom(zoom / 1.25f);
        private void resetZoomToolStripMenuItem_Click(object? sender, EventArgs e) => SetZoom(1f);
        private void toggleGridToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            showGrid = !showGrid;
            RefreshCanvas();
        }

        private void swapColorsToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            (primaryColor, secondaryColor) = (secondaryColor, primaryColor);
            UpdateColorPanels();
        }

        private void choosePrimaryColorToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            colorDialog.Color = primaryColor;
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                primaryColor = colorDialog.Color;
                UpdateColorPanels();
            }
        }

        private void chooseSecondaryColorToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            colorDialog.Color = secondaryColor;
            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                secondaryColor = colorDialog.Color;
                UpdateColorPanels();
            }
        }

        #endregion

        #region File Operations

        private void newCanvasToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (ConfirmDiscardChanges() != DialogResult.Yes)
            {
                return;
            }

            using var dialog = new NewCanvasDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                InitializeCanvas(dialog.CanvasWidth, dialog.CanvasHeight);
                RefreshCanvas();
            }
        }

        private void openToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (ConfirmDiscardChanges() != DialogResult.Yes)
            {
                return;
            }

            using OpenFileDialog dialog = new()
            {
                Filter = "Image Files|*.png;*.bmp;*.gif;*.jpg;*.jpeg|All Files|*.*"
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    using Bitmap loaded = new(dialog.FileName);
                    InitializeCanvas(loaded.Width, loaded.Height);
                    using Graphics g = Graphics.FromImage(canvas);
                    g.DrawImage(loaded, 0, 0);
                    currentFilePath = dialog.FileName;
                    hasUnsavedChanges = false;
                    Text = $"Sprite Editor - {Path.GetFileName(currentFilePath)}";
                    RefreshCanvas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"Failed to load image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                SaveToFile(currentFilePath);
            }
            else
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private void saveAsToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            using SaveFileDialog dialog = new()
            {
                Filter = "PNG Image|*.png|Bitmap Image|*.bmp|JPEG Image|*.jpg;*.jpeg"
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                SaveToFile(dialog.FileName);
                currentFilePath = dialog.FileName;
                Text = $"Sprite Editor - {Path.GetFileName(currentFilePath)}";
            }
        }

        private void SaveToFile(string filePath)
        {
            try
            {
                canvas.Save(filePath);
                hasUnsavedChanges = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to save image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void undoToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            RestoreFromStack(undoStack, redoStack);
        }

        private void redoToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            RestoreFromStack(redoStack, undoStack);
        }

        private void clearCanvasToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Clear the canvas?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveUndoState();
                using Graphics g = Graphics.FromImage(canvas);
                g.Clear(Color.Transparent);
                hasUnsavedChanges = true;
                RefreshCanvas();
            }
        }

        #endregion

        #region Color Panel Events

        private void primaryColorPanel_Click(object? sender, EventArgs e)
        {
            choosePrimaryColorToolStripMenuItem_Click(sender, e);
        }

        private void secondaryColorPanel_Click(object? sender, EventArgs e)
        {
            chooseSecondaryColorToolStripMenuItem_Click(sender, e);
        }

        #endregion

        private void SpriteEditorForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (ConfirmDiscardChanges() == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            overlay?.Dispose();
            canvas.Dispose();
            ClearStack(undoStack);
            ClearStack(redoStack);
        }

        private static void ClearStack(Stack<Bitmap> stack)
        {
            while (stack.Count > 0)
            {
                stack.Pop().Dispose();
            }
        }

        private static void TrimStack(Stack<Bitmap> stack, int maxItems)
        {
            if (stack.Count <= maxItems)
            {
                return;
            }

            Bitmap[] buffer = stack.ToArray();
            stack.Clear();

            for (int i = 0; i < buffer.Length; i++)
            {
                if (i >= maxItems)
                {
                    buffer[i].Dispose();
                }
            }

            for (int i = Math.Min(maxItems, buffer.Length) - 1; i >= 0; i--)
            {
                stack.Push(buffer[i]);
            }
        }
    }
}
