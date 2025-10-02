using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SPHMMaker.Loot
{
    public class LootEntry : INotifyPropertyChanged
    {
        int itemId;
        double dropChance;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int ItemId
        {
            get => itemId;
            set
            {
                if (itemId == value)
                {
                    return;
                }

                itemId = value;
                OnPropertyChanged();
            }
        }

        public double DropChance
        {
            get => dropChance;
            set
            {
                double clamped = Math.Clamp(value, 0d, 1d);
                if (Math.Abs(dropChance - clamped) < double.Epsilon)
                {
                    return;
                }

                dropChance = clamped;
                OnPropertyChanged();
            }
        }

        void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
