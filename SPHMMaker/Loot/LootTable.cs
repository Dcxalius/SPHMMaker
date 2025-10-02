
namespace SPHMMaker.Loot;

using System.ComponentModel;

public class LootTable : INotifyPropertyChanged
{
    private string id = string.Empty;

    public LootTable()
    {
        Entries = new BindingList<LootEntry>();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Id
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

    public override string ToString() => Id;

    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}