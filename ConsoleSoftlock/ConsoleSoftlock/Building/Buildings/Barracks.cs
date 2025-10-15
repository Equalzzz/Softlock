using ConsoleSoftlock.InputInterface;

namespace ConsoleSoftlock.Building.Buildings
{
    [NamedBuilding("Barracks")]
    public class Barracks : BuildingCell, IDirectional, IInteractive, IUpdatable
    {
        public Direction Direction { get; set; }
        public bool IsActivationDirectional => true;
        public bool IsPlacementDirectional => false;

        public void Interact(GameState state, InteractAction action)
        {

        }
        public override bool IsReplaceableBy(Type other) => other == typeof(ExtendedBarracks);
        public override char GetSymbol() => 'O'; // O ○ 

        public void OnTurnStart(GameState state)
        {
            if (!IsPowered) return; // If barracks are jammend
            if (!state.TryGetField(Owner, out var field)) return;

            TryPowerBuilding(field, X - 1, Y);
            TryPowerBuilding(field, X + 1, Y);
            TryPowerBuilding(field, X, Y - 1);
            TryPowerBuilding(field, X, Y + 1);
        }
        protected static void TryPowerBuilding(in Field ownerField, int x, int y)
        {
            if (x > 0 && x < ownerField.Width - 1 &&
                y > 0 && y < ownerField.Height - 1 &&
                ownerField.Grid[x, y] is BuildingCell cell && cell is not DestroyedCell)
                cell.IsPowered = true; // TODO: implement jammer specifics
        }
    }
}
