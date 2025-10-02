using System.ComponentModel;

namespace SPHMMaker.Loot
{
    public class LootTable : INotifyPropertyChanged
    {
        int id;
        string name;

        public LootTable()
        {
            name = string.Empty;
            Entries = new BindingList<LootEntry>();
        }

        public LootTable(int id, string name) : this()
        {
            Id = id;
            Name = name;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id
        {
            get => id;
            set
            {
                if (id == value)
                {
                    return;
                }

                id = value;
                OnPropertyChanged(nameof(Id));
                OnPropertyChanged(nameof(Display));
            }
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
                OnPropertyChanged(nameof(Display));
            }
        }

        public BindingList<LootEntry> Entries { get; }

        public string Display => $"{Id}: {Name}";

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString() => Display;
    }
}
