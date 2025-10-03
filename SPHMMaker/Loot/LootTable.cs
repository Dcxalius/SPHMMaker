using System.ComponentModel;

namespace SPHMMaker.Loot;

public class LootTable : INotifyPropertyChanged
{
    private int id;

    public LootTable()
    {
        Entries = new BindingList<LootEntry>();
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
        }
    }

    public BindingList<LootEntry> Entries { get; }

    public override string ToString() => Id.ToString();

    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
