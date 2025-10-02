using System.Text;

namespace SPHMMaker.Tiles
{
    public class TileData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Texture { get; set; }
        public bool IsWalkable { get; set; }
        public int MovementCost { get; set; }
        public string Notes { get; set; }

        public TileData()
        {
            Name = string.Empty;
            Texture = string.Empty;
            Notes = string.Empty;
        }

        public TileData(int id, string name, string texture, bool isWalkable, int movementCost, string notes)
        {
            ID = id;
            Name = name;
            Texture = texture;
            IsWalkable = isWalkable;
            MovementCost = movementCost;
            Notes = notes;
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
