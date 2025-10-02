namespace SPHMMaker.Loot;

using System;
using System.ComponentModel;

public class LootEntry : INotifyPropertyChanged
{
    private string itemId = string.Empty;
    private double dropRate;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string ItemId
    {
        get => itemId;
        set
        {
            if (itemId == value)
            {
                return;
            }

            itemId = value;
            OnPropertyChanged(nameof(ItemId));
        }
    }

    /// <summary>
    /// Gets or sets the drop rate as a value between 0 and 1.
    /// </summary>
    public double DropRate
    {
        get => dropRate;
        set
        {
            double clamped = Math.Clamp(value, 0d, 1d);
            if (Math.Abs(dropRate - clamped) < double.Epsilon)
            {
                return;
            }

            dropRate = clamped;
            OnPropertyChanged(nameof(DropRate));
            OnPropertyChanged(nameof(DropRatePercent));
        }
    }

    /// <summary>
    /// Gets or sets the drop rate as a percentage value between 0 and 100.
    /// </summary>
    public double DropRatePercent
    {
        get => DropRate * 100d;
        set => DropRate = value / 100d;
    }

    public override string ToString() => $"{ItemId} ({DropRatePercent:0.##}%)";

    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
