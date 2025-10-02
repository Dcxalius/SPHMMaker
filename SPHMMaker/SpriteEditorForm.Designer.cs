namespace SPHMMaker
{
    partial class SpriteEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newCanvasToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            clearCanvasToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            zoomInToolStripMenuItem = new ToolStripMenuItem();
            zoomOutToolStripMenuItem = new ToolStripMenuItem();
            resetZoomToolStripMenuItem = new ToolStripMenuItem();
            toggleGridToolStripMenuItem = new ToolStripMenuItem();
            colorsToolStripMenuItem = new ToolStripMenuItem();
            swapColorsToolStripMenuItem = new ToolStripMenuItem();
            choosePrimaryColorToolStripMenuItem = new ToolStripMenuItem();
            chooseSecondaryColorToolStripMenuItem = new ToolStripMenuItem();
            toolStrip = new ToolStrip();
            pencilToolStripButton = new ToolStripButton();
            eraserToolStripButton = new ToolStripButton();
            fillToolStripButton = new ToolStripButton();
            colorPickerToolStripButton = new ToolStripButton();
            lineToolStripButton = new ToolStripButton();
            rectangleToolStripButton = new ToolStripButton();
            ellipseToolStripButton = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            brushSizeLabel = new ToolStripLabel();
            brushSizeComboBox = new ToolStripComboBox();
            toolStripSeparator2 = new ToolStripSeparator();
            fillShapeToolStripButton = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            zoomDropDownButton = new ToolStripDropDownButton();
            zoom25ToolStripMenuItem = new ToolStripMenuItem();
            zoom50ToolStripMenuItem = new ToolStripMenuItem();
            zoom100ToolStripMenuItem = new ToolStripMenuItem();
            zoom200ToolStripMenuItem = new ToolStripMenuItem();
            zoom400ToolStripMenuItem = new ToolStripMenuItem();
            colorPalettePanel = new FlowLayoutPanel();
            colorToolTip = new ToolTip(components);
            canvasContainer = new Panel();
            canvasView = new PictureBox();
            statusStrip = new StatusStrip();
            statusPositionLabel = new ToolStripStatusLabel();
            statusToolLabel = new ToolStripStatusLabel();
            statusZoomLabel = new ToolStripStatusLabel();
            primaryColorPanel = new Panel();
            secondaryColorPanel = new Panel();
            primaryColorLabel = new Label();
            secondaryColorLabel = new Label();
            colorPreviewContainer = new TableLayoutPanel();
            menuStrip.SuspendLayout();
            toolStrip.SuspendLayout();
            canvasContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)canvasView).BeginInit();
            statusStrip.SuspendLayout();
            colorPreviewContainer.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem, colorsToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1184, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newCanvasToolStripMenuItem, openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newCanvasToolStripMenuItem
            // 
            newCanvasToolStripMenuItem.Name = "newCanvasToolStripMenuItem";
            newCanvasToolStripMenuItem.Size = new Size(180, 22);
            newCanvasToolStripMenuItem.Text = "New";
            newCanvasToolStripMenuItem.Click += newCanvasToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(180, 22);
            openToolStripMenuItem.Text = "Open...";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(180, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(180, 22);
            saveAsToolStripMenuItem.Text = "Save As...";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem, clearCanvasToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new Size(135, 22);
            undoToolStripMenuItem.Text = "Undo";
            undoToolStripMenuItem.Click += undoToolStripMenuItem_Click;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new Size(135, 22);
            redoToolStripMenuItem.Text = "Redo";
            redoToolStripMenuItem.Click += redoToolStripMenuItem_Click;
            // 
            // clearCanvasToolStripMenuItem
            // 
            clearCanvasToolStripMenuItem.Name = "clearCanvasToolStripMenuItem";
            clearCanvasToolStripMenuItem.Size = new Size(135, 22);
            clearCanvasToolStripMenuItem.Text = "Clear";
            clearCanvasToolStripMenuItem.Click += clearCanvasToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { zoomInToolStripMenuItem, zoomOutToolStripMenuItem, resetZoomToolStripMenuItem, toggleGridToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // zoomInToolStripMenuItem
            // 
            zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            zoomInToolStripMenuItem.Size = new Size(134, 22);
            zoomInToolStripMenuItem.Text = "Zoom In";
            zoomInToolStripMenuItem.Click += zoomInToolStripMenuItem_Click;
            // 
            // zoomOutToolStripMenuItem
            // 
            zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            zoomOutToolStripMenuItem.Size = new Size(134, 22);
            zoomOutToolStripMenuItem.Text = "Zoom Out";
            zoomOutToolStripMenuItem.Click += zoomOutToolStripMenuItem_Click;
            // 
            // resetZoomToolStripMenuItem
            // 
            resetZoomToolStripMenuItem.Name = "resetZoomToolStripMenuItem";
            resetZoomToolStripMenuItem.Size = new Size(134, 22);
            resetZoomToolStripMenuItem.Text = "Reset";
            resetZoomToolStripMenuItem.Click += resetZoomToolStripMenuItem_Click;
            // 
            // toggleGridToolStripMenuItem
            // 
            toggleGridToolStripMenuItem.Name = "toggleGridToolStripMenuItem";
            toggleGridToolStripMenuItem.Size = new Size(134, 22);
            toggleGridToolStripMenuItem.Text = "Toggle Grid";
            toggleGridToolStripMenuItem.Click += toggleGridToolStripMenuItem_Click;
            // 
            // colorsToolStripMenuItem
            // 
            colorsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { swapColorsToolStripMenuItem, choosePrimaryColorToolStripMenuItem, chooseSecondaryColorToolStripMenuItem });
            colorsToolStripMenuItem.Name = "colorsToolStripMenuItem";
            colorsToolStripMenuItem.Size = new Size(53, 20);
            colorsToolStripMenuItem.Text = "Colors";
            // 
            // swapColorsToolStripMenuItem
            // 
            swapColorsToolStripMenuItem.Name = "swapColorsToolStripMenuItem";
            swapColorsToolStripMenuItem.Size = new Size(190, 22);
            swapColorsToolStripMenuItem.Text = "Swap";
            swapColorsToolStripMenuItem.Click += swapColorsToolStripMenuItem_Click;
            // 
            // choosePrimaryColorToolStripMenuItem
            // 
            choosePrimaryColorToolStripMenuItem.Name = "choosePrimaryColorToolStripMenuItem";
            choosePrimaryColorToolStripMenuItem.Size = new Size(190, 22);
            choosePrimaryColorToolStripMenuItem.Text = "Choose Primary...";
            choosePrimaryColorToolStripMenuItem.Click += choosePrimaryColorToolStripMenuItem_Click;
            // 
            // chooseSecondaryColorToolStripMenuItem
            // 
            chooseSecondaryColorToolStripMenuItem.Name = "chooseSecondaryColorToolStripMenuItem";
            chooseSecondaryColorToolStripMenuItem.Size = new Size(190, 22);
            chooseSecondaryColorToolStripMenuItem.Text = "Choose Secondary...";
            chooseSecondaryColorToolStripMenuItem.Click += chooseSecondaryColorToolStripMenuItem_Click;
            // 
            // toolStrip
            // 
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip.Items.AddRange(new ToolStripItem[] { pencilToolStripButton, eraserToolStripButton, fillToolStripButton, colorPickerToolStripButton, lineToolStripButton, rectangleToolStripButton, ellipseToolStripButton, toolStripSeparator1, brushSizeLabel, brushSizeComboBox, toolStripSeparator2, fillShapeToolStripButton, toolStripSeparator3, zoomDropDownButton });
            toolStrip.Location = new Point(0, 24);
            toolStrip.Name = "toolStrip";
            toolStrip.Padding = new Padding(5, 0, 1, 0);
            toolStrip.Size = new Size(1184, 25);
            toolStrip.TabIndex = 1;
            toolStrip.Text = "toolStrip1";
            // 
            // pencilToolStripButton
            // 
            pencilToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            pencilToolStripButton.Name = "pencilToolStripButton";
            pencilToolStripButton.Size = new Size(43, 22);
            pencilToolStripButton.Text = "Pencil";
            pencilToolStripButton.Click += toolButton_Click;
            // 
            // eraserToolStripButton
            // 
            eraserToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            eraserToolStripButton.Name = "eraserToolStripButton";
            eraserToolStripButton.Size = new Size(44, 22);
            eraserToolStripButton.Text = "Eraser";
            eraserToolStripButton.Click += toolButton_Click;
            // 
            // fillToolStripButton
            // 
            fillToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            fillToolStripButton.Name = "fillToolStripButton";
            fillToolStripButton.Size = new Size(29, 22);
            fillToolStripButton.Text = "Fill";
            fillToolStripButton.Click += toolButton_Click;
            // 
            // colorPickerToolStripButton
            // 
            colorPickerToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            colorPickerToolStripButton.Name = "colorPickerToolStripButton";
            colorPickerToolStripButton.Size = new Size(76, 22);
            colorPickerToolStripButton.Text = "Color Picker";
            colorPickerToolStripButton.Click += toolButton_Click;
            // 
            // lineToolStripButton
            // 
            lineToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            lineToolStripButton.Name = "lineToolStripButton";
            lineToolStripButton.Size = new Size(34, 22);
            lineToolStripButton.Text = "Line";
            lineToolStripButton.Click += toolButton_Click;
            // 
            // rectangleToolStripButton
            // 
            rectangleToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            rectangleToolStripButton.Name = "rectangleToolStripButton";
            rectangleToolStripButton.Size = new Size(63, 22);
            rectangleToolStripButton.Text = "Rectangle";
            rectangleToolStripButton.Click += toolButton_Click;
            // 
            // ellipseToolStripButton
            // 
            ellipseToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            ellipseToolStripButton.Name = "ellipseToolStripButton";
            ellipseToolStripButton.Size = new Size(45, 22);
            ellipseToolStripButton.Text = "Ellipse";
            ellipseToolStripButton.Click += toolButton_Click;
            // 
            // brushSizeLabel
            // 
            brushSizeLabel.Name = "brushSizeLabel";
            brushSizeLabel.Size = new Size(65, 22);
            brushSizeLabel.Text = "Brush Size";
            // 
            // brushSizeComboBox
            // 
            brushSizeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            brushSizeComboBox.Name = "brushSizeComboBox";
            brushSizeComboBox.Size = new Size(75, 25);
            brushSizeComboBox.SelectedIndexChanged += brushSizeComboBox_SelectedIndexChanged;
            // 
            // fillShapeToolStripButton
            // 
            fillShapeToolStripButton.CheckOnClick = true;
            fillShapeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            fillShapeToolStripButton.Name = "fillShapeToolStripButton";
            fillShapeToolStripButton.Size = new Size(60, 22);
            fillShapeToolStripButton.Text = "Fill Shape";
            fillShapeToolStripButton.ToolTipText = "Toggle between filled and outlined shapes";
            fillShapeToolStripButton.CheckedChanged += fillShapeToolStripButton_CheckedChanged;
            // 
            // zoomDropDownButton
            // 
            zoomDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            zoomDropDownButton.DropDownItems.AddRange(new ToolStripItem[] { zoom25ToolStripMenuItem, zoom50ToolStripMenuItem, zoom100ToolStripMenuItem, zoom200ToolStripMenuItem, zoom400ToolStripMenuItem });
            zoomDropDownButton.Name = "zoomDropDownButton";
            zoomDropDownButton.Size = new Size(50, 22);
            zoomDropDownButton.Text = "Zoom";
            // 
            // zoom25ToolStripMenuItem
            // 
            zoom25ToolStripMenuItem.Name = "zoom25ToolStripMenuItem";
            zoom25ToolStripMenuItem.Size = new Size(102, 22);
            zoom25ToolStripMenuItem.Text = "25%";
            zoom25ToolStripMenuItem.Click += zoomPreset_Click;
            // 
            // zoom50ToolStripMenuItem
            // 
            zoom50ToolStripMenuItem.Name = "zoom50ToolStripMenuItem";
            zoom50ToolStripMenuItem.Size = new Size(102, 22);
            zoom50ToolStripMenuItem.Text = "50%";
            zoom50ToolStripMenuItem.Click += zoomPreset_Click;
            // 
            // zoom100ToolStripMenuItem
            // 
            zoom100ToolStripMenuItem.Name = "zoom100ToolStripMenuItem";
            zoom100ToolStripMenuItem.Size = new Size(102, 22);
            zoom100ToolStripMenuItem.Text = "100%";
            zoom100ToolStripMenuItem.Click += zoomPreset_Click;
            // 
            // zoom200ToolStripMenuItem
            // 
            zoom200ToolStripMenuItem.Name = "zoom200ToolStripMenuItem";
            zoom200ToolStripMenuItem.Size = new Size(102, 22);
            zoom200ToolStripMenuItem.Text = "200%";
            zoom200ToolStripMenuItem.Click += zoomPreset_Click;
            // 
            // zoom400ToolStripMenuItem
            // 
            zoom400ToolStripMenuItem.Name = "zoom400ToolStripMenuItem";
            zoom400ToolStripMenuItem.Size = new Size(102, 22);
            zoom400ToolStripMenuItem.Text = "400%";
            zoom400ToolStripMenuItem.Click += zoomPreset_Click;
            // 
            // colorPalettePanel
            // 
            colorPalettePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            colorPalettePanel.AutoScroll = true;
            colorPalettePanel.Location = new Point(964, 52);
            colorPalettePanel.Name = "colorPalettePanel";
            colorPalettePanel.Padding = new Padding(6);
            colorPalettePanel.Size = new Size(208, 264);
            colorPalettePanel.TabIndex = 3;
            // 
            // canvasContainer
            // 
            canvasContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            canvasContainer.AutoScroll = true;
            canvasContainer.BackColor = Color.DarkGray;
            canvasContainer.Controls.Add(canvasView);
            canvasContainer.Location = new Point(12, 52);
            canvasContainer.Name = "canvasContainer";
            canvasContainer.Size = new Size(946, 657);
            canvasContainer.TabIndex = 2;
            // 
            // canvasView
            // 
            canvasView.BackColor = Color.Transparent;
            canvasView.Location = new Point(0, 0);
            canvasView.Name = "canvasView";
            canvasView.Size = new Size(512, 512);
            canvasView.TabIndex = 0;
            canvasView.TabStop = false;
            canvasView.Paint += canvasView_Paint;
            canvasView.MouseDown += canvasView_MouseDown;
            canvasView.MouseMove += canvasView_MouseMove;
            canvasView.MouseUp += canvasView_MouseUp;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { statusPositionLabel, statusToolLabel, statusZoomLabel });
            statusStrip.Location = new Point(0, 732);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(1184, 22);
            statusStrip.TabIndex = 4;
            statusStrip.Text = "statusStrip1";
            // 
            // statusPositionLabel
            // 
            statusPositionLabel.Name = "statusPositionLabel";
            statusPositionLabel.Size = new Size(25, 17);
            statusPositionLabel.Text = "X:0";
            // 
            // statusToolLabel
            // 
            statusToolLabel.Name = "statusToolLabel";
            statusToolLabel.Size = new Size(60, 17);
            statusToolLabel.Text = "Tool: N/A";
            // 
            // statusZoomLabel
            // 
            statusZoomLabel.Name = "statusZoomLabel";
            statusZoomLabel.Size = new Size(67, 17);
            statusZoomLabel.Text = "Zoom: 100%";
            // 
            // primaryColorPanel
            // 
            primaryColorPanel.BorderStyle = BorderStyle.FixedSingle;
            primaryColorPanel.Dock = DockStyle.Fill;
            primaryColorPanel.Location = new Point(3, 18);
            primaryColorPanel.Name = "primaryColorPanel";
            primaryColorPanel.Size = new Size(94, 64);
            primaryColorPanel.TabIndex = 5;
            primaryColorPanel.Click += primaryColorPanel_Click;
            // 
            // secondaryColorPanel
            // 
            secondaryColorPanel.BorderStyle = BorderStyle.FixedSingle;
            secondaryColorPanel.Dock = DockStyle.Fill;
            secondaryColorPanel.Location = new Point(103, 18);
            secondaryColorPanel.Name = "secondaryColorPanel";
            secondaryColorPanel.Size = new Size(94, 64);
            secondaryColorPanel.TabIndex = 6;
            secondaryColorPanel.Click += secondaryColorPanel_Click;
            // 
            // primaryColorLabel
            // 
            primaryColorLabel.AutoSize = true;
            primaryColorLabel.Dock = DockStyle.Fill;
            primaryColorLabel.Location = new Point(3, 0);
            primaryColorLabel.Name = "primaryColorLabel";
            primaryColorLabel.Size = new Size(94, 15);
            primaryColorLabel.TabIndex = 7;
            primaryColorLabel.Text = "Primary";
            primaryColorLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // secondaryColorLabel
            // 
            secondaryColorLabel.AutoSize = true;
            secondaryColorLabel.Dock = DockStyle.Fill;
            secondaryColorLabel.Location = new Point(103, 0);
            secondaryColorLabel.Name = "secondaryColorLabel";
            secondaryColorLabel.Size = new Size(94, 15);
            secondaryColorLabel.TabIndex = 8;
            secondaryColorLabel.Text = "Secondary";
            secondaryColorLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // colorPreviewContainer
            // 
            colorPreviewContainer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            colorPreviewContainer.ColumnCount = 2;
            colorPreviewContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            colorPreviewContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            colorPreviewContainer.Controls.Add(primaryColorLabel, 0, 0);
            colorPreviewContainer.Controls.Add(secondaryColorLabel, 1, 0);
            colorPreviewContainer.Controls.Add(primaryColorPanel, 0, 1);
            colorPreviewContainer.Controls.Add(secondaryColorPanel, 1, 1);
            colorPreviewContainer.Location = new Point(964, 322);
            colorPreviewContainer.Name = "colorPreviewContainer";
            colorPreviewContainer.RowCount = 2;
            colorPreviewContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));
            colorPreviewContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            colorPreviewContainer.Size = new Size(200, 85);
            colorPreviewContainer.TabIndex = 5;
            // 
            // SpriteEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 754);
            Controls.Add(colorPreviewContainer);
            Controls.Add(statusStrip);
            Controls.Add(colorPalettePanel);
            Controls.Add(canvasContainer);
            Controls.Add(toolStrip);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            MinimumSize = new Size(1000, 600);
            Name = "SpriteEditorForm";
            Text = "Sprite Editor";
            FormClosing += SpriteEditorForm_FormClosing;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            canvasContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)canvasView).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            colorPreviewContainer.ResumeLayout(false);
            colorPreviewContainer.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newCanvasToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripMenuItem clearCanvasToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem zoomInToolStripMenuItem;
        private ToolStripMenuItem zoomOutToolStripMenuItem;
        private ToolStripMenuItem resetZoomToolStripMenuItem;
        private ToolStripMenuItem toggleGridToolStripMenuItem;
        private ToolStripMenuItem colorsToolStripMenuItem;
        private ToolStripMenuItem swapColorsToolStripMenuItem;
        private ToolStripMenuItem choosePrimaryColorToolStripMenuItem;
        private ToolStripMenuItem chooseSecondaryColorToolStripMenuItem;
        private ToolStrip toolStrip;
        private ToolStripButton pencilToolStripButton;
        private ToolStripButton eraserToolStripButton;
        private ToolStripButton fillToolStripButton;
        private ToolStripButton colorPickerToolStripButton;
        private ToolStripButton lineToolStripButton;
        private ToolStripButton rectangleToolStripButton;
        private ToolStripButton ellipseToolStripButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel brushSizeLabel;
        private ToolStripComboBox brushSizeComboBox;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton fillShapeToolStripButton;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripDropDownButton zoomDropDownButton;
        private ToolStripMenuItem zoom25ToolStripMenuItem;
        private ToolStripMenuItem zoom50ToolStripMenuItem;
        private ToolStripMenuItem zoom100ToolStripMenuItem;
        private ToolStripMenuItem zoom200ToolStripMenuItem;
        private ToolStripMenuItem zoom400ToolStripMenuItem;
        private FlowLayoutPanel colorPalettePanel;
        private ToolTip colorToolTip;
        private Panel canvasContainer;
        private PictureBox canvasView;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusPositionLabel;
        private ToolStripStatusLabel statusToolLabel;
        private ToolStripStatusLabel statusZoomLabel;
        private Panel primaryColorPanel;
        private Panel secondaryColorPanel;
        private Label primaryColorLabel;
        private Label secondaryColorLabel;
        private TableLayoutPanel colorPreviewContainer;
    }
}
