using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SPHMMaker.Races;

namespace SPHMMaker
{
    public partial class MainForm
    {
        TabPage? raceTabPage;
        ListBox? raceListBox;
        Label? selectedRaceLabel;
        TextBox? raceNameInput;
        TextBox? raceDescriptionInput;
        Button? createRaceButton;
        Button? updateRaceButton;
        Button? deleteRaceButton;
        private readonly BindingSource raceBindingSource = new();
        private readonly BindingList<RaceData> raceDefinitions = new();

        void InitializeRaceTab()
        {
            if (MainTab == null)
            {
                return;
            }

            raceTabPage = new TabPage("Races")
            {
                Padding = new Padding(8)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            raceTabPage.Controls.Add(layout);
            MainTab.Controls.Add(raceTabPage);

            raceListBox = new ListBox
            {
                Dock = DockStyle.Fill
            };
            raceListBox.SelectedIndexChanged += RaceListBox_SelectedIndexChanged;
            layout.Controls.Add(raceListBox, 0, 0);
            layout.SetRowSpan(raceListBox, 2);

            var detailGroup = new GroupBox
            {
                Text = "Race Details",
                Dock = DockStyle.Fill
            };
            layout.Controls.Add(detailGroup, 1, 0);

            var detailLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4
            };
            detailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            detailLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            detailLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            detailLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            detailLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            detailLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            detailGroup.Controls.Add(detailLayout);

            selectedRaceLabel = new Label
            {
                Text = "Selected race: None",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            detailLayout.Controls.Add(selectedRaceLabel, 0, 0);
            detailLayout.SetColumnSpan(selectedRaceLabel, 2);

            var nameLabel = new Label
            {
                Text = "Name",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            detailLayout.Controls.Add(nameLabel, 0, 1);

            raceNameInput = new TextBox
            {
                Dock = DockStyle.Fill
            };
            detailLayout.Controls.Add(raceNameInput, 1, 1);

            var descriptionLabel = new Label
            {
                Text = "Description",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            detailLayout.Controls.Add(descriptionLabel, 0, 2);
            detailLayout.SetColumnSpan(descriptionLabel, 2);

            raceDescriptionInput = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            detailLayout.Controls.Add(raceDescriptionInput, 0, 3);
            detailLayout.SetColumnSpan(raceDescriptionInput, 2);

            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true
            };
            layout.Controls.Add(buttonPanel, 1, 1);

            createRaceButton = new Button
            {
                Text = "Create Race"
            };
            createRaceButton.Click += CreateRaceButton_Click;
            buttonPanel.Controls.Add(createRaceButton);

            updateRaceButton = new Button
            {
                Text = "Save Changes",
                Enabled = false
            };
            updateRaceButton.Click += UpdateRaceButton_Click;
            buttonPanel.Controls.Add(updateRaceButton);

            deleteRaceButton = new Button
            {
                Text = "Remove Race",
                Enabled = false
            };
            deleteRaceButton.Click += DeleteRaceButton_Click;
            buttonPanel.Controls.Add(deleteRaceButton);
        }

        void InitializeRaceDataBindings()
        {
            raceBindingSource.DataSource = raceDefinitions;

            if (raceListBox != null)
            {
                raceListBox.DisplayMember = nameof(RaceData.DisplayText);
                raceListBox.DataSource = raceBindingSource;
            }

            PopulateRaceFields(raceListBox?.SelectedItem as RaceData);
        }

        void RaceListBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            PopulateRaceFields(raceListBox?.SelectedItem as RaceData);
        }

        void PopulateRaceFields(RaceData? race)
        {
            if (raceNameInput == null || raceDescriptionInput == null)
            {
                return;
            }

            if (race == null)
            {
                raceNameInput.Text = string.Empty;
                raceDescriptionInput.Text = string.Empty;
            }
            else
            {
                raceNameInput.Text = race.Name;
                raceDescriptionInput.Text = race.Description;
            }

            if (selectedRaceLabel != null)
            {
                selectedRaceLabel.Text = race == null ? "Selected race: None" : $"Selected race: {race.DisplayText}";
            }

            if (updateRaceButton != null)
            {
                updateRaceButton.Enabled = race != null;
            }

            if (deleteRaceButton != null)
            {
                deleteRaceButton.Enabled = race != null;
            }
        }

        void CreateRaceButton_Click(object? sender, EventArgs e)
        {
            if (raceNameInput == null)
            {
                return;
            }

            string name = raceNameInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter a race name before adding it.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var race = new RaceData
            {
                Name = name,
                Description = raceDescriptionInput?.Text.Trim() ?? string.Empty
            };

            raceDefinitions.Add(race);
            raceBindingSource.ResetBindings(false);

            if (raceListBox != null)
            {
                raceListBox.SelectedItem = race;
            }
        }

        void UpdateRaceButton_Click(object? sender, EventArgs e)
        {
            if (raceNameInput == null)
            {
                return;
            }

            if (raceListBox?.SelectedItem is not RaceData race)
            {
                MessageBox.Show("Select a race to update first.", "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string name = raceNameInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("A race must have a name.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            race.Name = name;
            race.Description = raceDescriptionInput?.Text.Trim() ?? string.Empty;

            raceBindingSource.ResetCurrentItem();
            PopulateRaceFields(race);
        }

        void DeleteRaceButton_Click(object? sender, EventArgs e)
        {
            if (raceListBox?.SelectedItem is not RaceData race)
            {
                MessageBox.Show("Select a race to remove first.", "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            raceDefinitions.Remove(race);
            raceBindingSource.ResetBindings(false);
            PopulateRaceFields(raceListBox?.SelectedItem as RaceData);
        }
    }
}
