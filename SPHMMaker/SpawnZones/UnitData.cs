using System.ComponentModel;

namespace SPHMMaker.SpawnZones
{
    /// <summary>
    /// Represents a unit definition that can be referenced by spawn zones.
    /// </summary>
    public class UnitData : INotifyPropertyChanged
    {
        string name = string.Empty;
        int level = 1;
        string notes = string.Empty;

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

        public int Level
        {
            get => level;
            set
            {
                if (level == value)
                {
                    return;
                }

                level = value;
                OnPropertyChanged(nameof(Level));
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public string Notes
        {
            get => notes;
            set
            {
                if (notes == value)
                {
                    return;
                }

                notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }

        public string DisplayText => $"{Name} (Lv {Level})";

        public override string ToString() => DisplayText;

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
