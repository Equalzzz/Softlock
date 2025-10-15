using ConsoleSoftlock.InputInterface;

namespace ConsoleSoftlock.Building.Buildings
{
    public abstract class BuildingCell
    {
        public BuildingCell() { }
        public BuildingCell(int x, int y, int? id = null)
        {
            X = x;
            Y = y;
            ID = id == null ? BuildingTypeRegistry.AllTypes.IndexOf(GetType()) : id.Value;
        }
        public BuildingCell(ref BuildAction action) : this(action.x, action.y, action.id) { }
        public int X { get; }
        public int Y { get; }
        public int ID { get; }
        public Player? Owner { get; set; }
        public bool IsPowered { get; set; }
        public virtual char GetSymbol() => ' ';
        public virtual bool CanBePlacedOn(Type other) => other == null;
        public virtual bool IsReplaceableBy(Type other) => false;
    }
}
