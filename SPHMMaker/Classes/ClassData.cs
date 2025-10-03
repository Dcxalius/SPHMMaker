using System.ComponentModel;

namespace SPHMMaker.Classes
{
    /// <summary>
    /// Represents a player class configuration used by the editor.
    /// </summary>
    public class ClassData : INotifyPropertyChanged
    {
        string name = string.Empty;
        string role = string.Empty;
        int baseHealth = 100;
        int baseMana;
        string description = string.Empty;

        /// <summary>
        /// Gets or sets the display name of the class.
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                if (name == value)
                {
                    return;
                }

                name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        /// <summary>
        /// Gets or sets the general role or archetype of the class.
        /// </summary>
        public string Role
        {
            get => role;
            set
            {
                if (role == value)
                {
                    return;
                }

                role = value;
                OnPropertyChanged(nameof(Role));
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        /// <summary>
        /// Gets or sets the base health value for the class.
        /// </summary>
        public int BaseHealth
        {
            get => baseHealth;
            set
            {
                if (baseHealth == value)
                {
                    return;
                }

                baseHealth = Math.Max(1, value);
                OnPropertyChanged(nameof(BaseHealth));
            }
        }

        /// <summary>
        /// Gets or sets the base mana value for the class.
        /// </summary>
        public int BaseMana
        {
            get => baseMana;
            set
            {
                if (baseMana == value)
                {
                    return;
                }

                baseMana = Math.Max(0, value);
                OnPropertyChanged(nameof(BaseMana));
            }
        }

        /// <summary>
        /// Gets or sets the descriptive text for the class.
        /// </summary>
        public string Description
        {
            get => description;
            set
            {
                if (description == value)
                {
                    return;
                }

                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        /// <summary>
        /// Gets the text shown in list controls for this class.
        /// </summary>
        public string DisplayText
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Role))
                {
                    return Name;
                }

                return $"{Name} ({Role})";
            }
        }

        public override string ToString() => DisplayText;

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
