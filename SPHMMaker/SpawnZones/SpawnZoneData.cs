using System;
using System.ComponentModel;

namespace SPHMMaker.SpawnZones
{
    /// <summary>
    /// Describes a spawn zone and the unit assignments that can appear within it.
    /// </summary>
    public class SpawnZoneData : INotifyPropertyChanged
    {
        string name = string.Empty;
        string notes = string.Empty;

        public SpawnZoneData()
        {
            Assignments = new BindingList<SpawnZoneAssignment>();
        }

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

        public BindingList<SpawnZoneAssignment> Assignments { get; }

        public string DisplayText => string.IsNullOrWhiteSpace(Name) ? "Unnamed Spawn Zone" : Name;

        public override string ToString() => DisplayText;

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Represents a connection between a spawn zone and a unit definition.
    /// </summary>
    public class SpawnZoneAssignment : INotifyPropertyChanged
    {
        int minimum;
        int maximum;

        public SpawnZoneAssignment(UnitData unit, int min, int max)
        {
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            Unit.PropertyChanged += Unit_PropertyChanged;
            minimum = min;
            maximum = max;
        }

        public UnitData Unit { get; }

        public int Minimum
        {
            get => minimum;
            set
            {
                if (minimum == value)
                {
                    return;
                }

                minimum = value;
                OnPropertyChanged(nameof(Minimum));
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public int Maximum
        {
            get => maximum;
            set
            {
                if (maximum == value)
                {
                    return;
                }

                maximum = value;
                OnPropertyChanged(nameof(Maximum));
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public string DisplayText => $"{Unit.DisplayText} ({Minimum}-{Maximum})";

        public override string ToString() => DisplayText;

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        void Unit_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UnitData.DisplayText) ||
                e.PropertyName == nameof(UnitData.Name) ||
                e.PropertyName == nameof(UnitData.Level))
            {
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public void NotifyUnitUpdated() => OnPropertyChanged(nameof(DisplayText));

        public void Detach()
        {
            Unit.PropertyChanged -= Unit_PropertyChanged;
        }
    }
}
