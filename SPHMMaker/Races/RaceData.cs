using System.ComponentModel;

namespace SPHMMaker.Races
{
    /// <summary>
    /// Represents a race definition that can be referenced throughout the tool.
    /// </summary>
    public class RaceData : INotifyPropertyChanged
    {
        string name = string.Empty;
        string description = string.Empty;

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

        public string DisplayText => string.IsNullOrWhiteSpace(Name) ? "(Unnamed Race)" : Name;

        public override string ToString() => DisplayText;

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
