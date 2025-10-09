using System.Text;

namespace SPHMMaker.Tiles
{
    public class TileData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Texture { get; set; }
        public bool IsWalkable { get; set; }
        public decimal MovementCost { get; set; }
        public bool Transparent { get; set; }

        public TileData()
        {
            Name = string.Empty;
            Texture = string.Empty;
            Transparent = true;
            MovementCost = 1m;
        }

        public TileData(int id, string name, string texture, bool isWalkable, decimal movementCost, bool transparent)
        {
            ID = id;
            Name = name;
            Texture = texture;
            IsWalkable = isWalkable;
            MovementCost = movementCost;
            Transparent = transparent;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append('[');
            builder.Append(ID);
            builder.Append("] ");
            builder.Append(Name);
            return builder.ToString();
        }
    }
}
