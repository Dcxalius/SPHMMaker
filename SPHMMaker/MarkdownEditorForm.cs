using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SPHMMaker
{
    public class MarkdownEditorForm : Form
    {
        readonly ToolStrip toolStrip;
        readonly ToolStripButton openButton;
        readonly ToolStripButton saveButton;
        readonly ToolStripButton saveAsButton;
        readonly ToolStripLabel fileLabel;

        readonly TabControl tabControl;
        readonly TabPage previewTab;
        readonly TabPage editTab;
        readonly WebBrowser previewBrowser;
        readonly TextBox editorTextBox;

        string? currentFilePath;
        bool isUpdatingFromFile;

        public MarkdownEditorForm()
        {
            Text = "Markdown Editor";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(600, 400);

            toolStrip = new ToolStrip();
            openButton = new ToolStripButton("Open")
            {
                DisplayStyle = ToolStripItemDisplayStyle.Text
            };
            openButton.Click += OpenButton_Click;

            saveButton = new ToolStripButton("Save")
            {
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                Enabled = false
            };
            saveButton.Click += SaveButton_Click;

            saveAsButton = new ToolStripButton("Save As")
            {
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                Enabled = true
            };
            saveAsButton.Click += SaveAsButton_Click;

            fileLabel = new ToolStripLabel("No file loaded");

            toolStrip.Items.Add(openButton);
            toolStrip.Items.Add(saveButton);
            toolStrip.Items.Add(saveAsButton);
            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(fileLabel);

            tabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };

            previewTab = new TabPage("Preview");
            previewBrowser = new WebBrowser
            {
                Dock = DockStyle.Fill,
                AllowWebBrowserDrop = false,
                WebBrowserShortcutsEnabled = false,
                IsWebBrowserContextMenuEnabled = false
            };
            previewTab.Controls.Add(previewBrowser);

            editTab = new TabPage("Editor");
            editorTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Both,
                AcceptsReturn = true,
                AcceptsTab = true,
                WordWrap = false,
                Font = new Font(FontFamily.GenericMonospace, 10f, FontStyle.Regular)
            };
            editorTextBox.TextChanged += EditorTextBox_TextChanged;
            editTab.Controls.Add(editorTextBox);

            tabControl.TabPages.Add(previewTab);
            tabControl.TabPages.Add(editTab);

            Controls.Add(tabControl);
            Controls.Add(toolStrip);

            toolStrip.Dock = DockStyle.Top;

            UpdatePreview();
        }

        void OpenButton_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Filter = "Markdown files (*.md;*.markdown)|*.md;*.markdown|Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Open Markdown File"
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                LoadFile(dialog.FileName);
            }
        }

        void SaveButton_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                SaveContentToFile(currentFilePath);
            }
        }

        void SaveAsButton_Click(object? sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog
            {
                Filter = "Markdown files (*.md;*.markdown)|*.md;*.markdown|Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Save Markdown File",
                FileName = Path.GetFileName(currentFilePath) ?? string.Empty
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                SaveContentToFile(dialog.FileName);
                currentFilePath = dialog.FileName;
                UpdateFileLabel();
            }
        }

        void LoadFile(string filePath)
        {
            try
            {
                var content = File.ReadAllText(filePath);
                isUpdatingFromFile = true;
                editorTextBox.Text = content;
                isUpdatingFromFile = false;
                currentFilePath = filePath;
                UpdateFileLabel();
                saveButton.Enabled = true;
                saveAsButton.Enabled = true;
                UpdatePreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to open file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void SaveContentToFile(string filePath)
        {
            try
            {
                File.WriteAllText(filePath, editorTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to save file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void EditorTextBox_TextChanged(object? sender, EventArgs e)
        {
            if (isUpdatingFromFile)
            {
                return;
            }

            UpdatePreview();
        }

        void UpdatePreview()
        {
            var markdown = editorTextBox.Text ?? string.Empty;
            var html = BuildHtml(markdown);
            try
            {
                previewBrowser.DocumentText = html;
            }
            catch
            {
                // Ignore rendering errors from the WebBrowser control.
            }
        }

        void UpdateFileLabel()
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                fileLabel.Text = "No file loaded";
            }
            else
            {
                fileLabel.Text = currentFilePath;
            }
        }

        static string BuildHtml(string markdown)
        {
            var builder = new StringBuilder();
            builder.Append("<html><head><meta charset='utf-8'><style>");
            builder.Append("body{font-family:'Segoe UI',sans-serif;margin:16px;}");
            builder.Append("code{background-color:#f2f2f2;padding:2px 4px;border-radius:4px;}");
            builder.Append("pre{background-color:#f2f2f2;padding:12px;border-radius:4px;overflow:auto;}");
            builder.Append("table{border-collapse:collapse;}");
            builder.Append("th,td{border:1px solid #ccc;padding:6px;}");
            builder.Append("</style></head><body>");

            var lines = markdown.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');
            var inList = false;
            var inCodeBlock = false;
            var codeBlockBuilder = new StringBuilder();

            foreach (var rawLine in lines)
            {
                var line = rawLine;

                if (line.StartsWith("```"))
                {
                    if (!inCodeBlock)
                    {
                        inCodeBlock = true;
                        codeBlockBuilder.Clear();
                    }
                    else
                    {
                        inCodeBlock = false;
                        builder.Append("<pre><code>");
                        builder.Append(WebUtility.HtmlEncode(codeBlockBuilder.ToString()));
                        builder.Append("</code></pre>");
                    }

                    continue;
                }

                if (inCodeBlock)
                {
                    codeBlockBuilder.AppendLine(line);
                    continue;
                }

                if (string.IsNullOrWhiteSpace(line))
                {
                    if (inList)
                    {
                        builder.Append("</ul>");
                        inList = false;
                    }

                    builder.Append("<p></p>");
                    continue;
                }

                var headingMatch = Regex.Match(line, "^(#{1,6})\\s+(.*)");
                if (headingMatch.Success)
                {
                    if (inList)
                    {
                        builder.Append("</ul>");
                        inList = false;
                    }

                    var level = headingMatch.Groups[1].Value.Length;
                    var content = FormatInline(headingMatch.Groups[2].Value.Trim());
                    builder.AppendFormat("<h{0}>{1}</h{0}>", level, content);
                    continue;
                }

                if (Regex.IsMatch(line, "^[-*+] "))
                {
                    if (!inList)
                    {
                        builder.Append("<ul>");
                        inList = true;
                    }

                    var itemContent = FormatInline(line[2..].Trim());
                    builder.Append("<li>");
                    builder.Append(itemContent);
                    builder.Append("</li>");
                    continue;
                }

                if (inList)
                {
                    builder.Append("</ul>");
                    inList = false;
                }

                builder.Append("<p>");
                builder.Append(FormatInline(line));
                builder.Append("</p>");
            }

            if (inCodeBlock)
            {
                builder.Append("<pre><code>");
                builder.Append(WebUtility.HtmlEncode(codeBlockBuilder.ToString()));
                builder.Append("</code></pre>");
            }

            if (inList)
            {
                builder.Append("</ul>");
            }

            builder.Append("</body></html>");
            return builder.ToString();
        }

        static string FormatInline(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var encoded = WebUtility.HtmlEncode(text);

            encoded = Regex.Replace(encoded, "\\*\\*(.+?)\\*\\*", "<strong>$1</strong>");
            encoded = Regex.Replace(encoded, "__(.+?)__", "<strong>$1</strong>");
            encoded = Regex.Replace(encoded, "\\*(.+?)\\*", "<em>$1</em>");
            encoded = Regex.Replace(encoded, "_(.+?)_", "<em>$1</em>");
            encoded = Regex.Replace(encoded, "`(.+?)`", "<code>$1</code>");
            encoded = Regex.Replace(encoded, "!\\[(.*?)\\]\\((.*?)\\)", "<img src='$2' alt='$1' />");
            encoded = Regex.Replace(encoded, "\\[(.*?)\\]\\((.*?)\\)", "<a href='$2'>$1</a>");

            return encoded;
        }
    }
}
