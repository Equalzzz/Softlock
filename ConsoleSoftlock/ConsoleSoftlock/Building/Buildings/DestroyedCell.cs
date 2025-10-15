namespace ConsoleSoftlock.Building.Buildings
{
    public class DestroyedCell(BuildingCell? previousCell = null) : BuildingCell
    {
        public BuildingCell? PreviousCell { get; set; } = previousCell;
        public override char GetSymbol() => '█'; // █ ■
    }
}
