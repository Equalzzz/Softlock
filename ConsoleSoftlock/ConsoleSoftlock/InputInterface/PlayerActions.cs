using ConsoleSoftlock.Building;
using ConsoleSoftlock.Building.Buildings;

namespace ConsoleSoftlock.InputInterface
{
    public interface IPlayerAction
    {
        public void PerformAction(GameState state);
    }
    public readonly struct BuildAction(int x, int y, int id, Direction direction) : IPlayerAction
    {
        public readonly int x = x, y = y, id = id;
        public readonly Direction direction = direction;

        public void PerformAction(GameState state)
        {
            if (!state.TryGetField(state.CurrentPlayer, out var field)) return;
            if (Activator.CreateInstance(BuildingTypeRegistry.AllTypes[id]) is BuildingCell cell)
                field.Grid[x, y] = cell;
        }
    }
    public readonly struct InteractAction(int x, int y, Direction direction) : IPlayerAction
    {
        public readonly int x = x, y = y;
        public readonly Direction direction = direction;

        public void PerformAction(GameState state)
        {
            // TODO: 
            Console.Beep();
        }
    }
}
