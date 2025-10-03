using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SPHMMaker.Loot;

public static class LootManager
{
    public static BindingList<LootTable> LootTables { get; } = new();

    public static LootTable Create(int id)
    {
        var table = new LootTable
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

    public static bool ContainsId(int id) =>
        LootTables.Any(table => table.Id == id);

    public static IEnumerable<int> GetIds() => LootTables.Select(table => table.Id);
}
