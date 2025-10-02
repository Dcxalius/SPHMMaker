using System.ComponentModel;

namespace SPHMMaker.Loot
{
    internal static class LootManager
    {
        static readonly BindingList<LootTable> lootTables = new();

        public static BindingList<LootTable> LootTables => lootTables;

        public static LootTable CreateLootTable(int id, string name)
        {
            if (ContainsId(id))
            {
                throw new InvalidOperationException($"Loot table with id {id} already exists.");
            }

            string resolvedName = string.IsNullOrWhiteSpace(name) ? $"Loot Table {id}" : name.Trim();
            LootTable lootTable = new(id, resolvedName);
            lootTables.Add(lootTable);
            return lootTable;
        }

        public static bool ContainsId(int id) => lootTables.Any(t => t.Id == id);

        public static void Remove(LootTable lootTable)
        {
            lootTables.Remove(lootTable);
        }
    }
}
