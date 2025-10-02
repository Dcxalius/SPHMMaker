namespace SPHMMaker.Loot;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public static class LootManager
{
    public static BindingList<LootTable> LootTables { get; } = new();

    public static LootTable Create(string id)
    {
        LootTable table = new()
        {
            Id = id
        };

        LootTables.Add(table);
        return table;
    }

    public static void Remove(LootTable table)
    {
        LootTables.Remove(table);
    }

    public static bool ContainsId(string id) =>
        LootTables.Any(table => table.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

    public static IEnumerable<string> GetIds() => LootTables.Select(table => table.Id);
}
