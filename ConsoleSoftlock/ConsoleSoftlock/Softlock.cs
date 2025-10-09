namespace ConsoleSoftlock
{
    public readonly struct Field(Player owner, int width, int height)
    {
        public Player Owner { get; } = owner;
        public BuildingCell[,] Grid { get; } = new BuildingCell[width, height];
        public int Width { get; } = width;
        public int Height { get; } = height;
    }

    public class GameState
    {
        public Field Player1Field { get; }
        public Field Player2Field { get; }
        public Player? CurrentPlayer { get; set; }
        public GameState(in Field p1field, in Field p2field)
        {
            Player1Field = p1field;
            Player2Field = p2field;
        }
        public GameState(Player p1, Player p2, int width, int height)
        {
            Player1Field = new Field(p1, width, height);
            Player2Field = new Field(p2, width, height);
        }
        public GameState(Player p1, Player p2, int size) : this(p1, p2, size, size) { }
        public int ApplyAction(Player p, IPlayerAction a)
        {
            a.PerformAction(this);
            return 0;
        }
        public Field GetCurrentPlayerField() =>
            CurrentPlayer == Player1Field.Owner ? Player1Field : Player2Field;
        public bool TryGetField(Player? p, out Field field)
        {
            if (p == Player1Field.Owner)
            {
                field = Player1Field;
                return true;
            }
            if (p == Player2Field.Owner)
            {
                field = Player2Field;
                return true;
            }
            field = default;
            return false;
        }
        public string[] GetGraphics() // <- Это самый уебещный комит ever!
        {
            List<string> res = [];
            string top = "  ";
            for (int x = 0; x < Player1Field.Width; top += (1 + x++) + " ") ;
            top += "  ";
            for (int x = Player2Field.Width; x > 0; top += x-- + " ") ;
            res.Add(top);

            for (int y = 0; y < Player1Field.Height; y++)
            {
                string line = (y + 1) + " ";
                for (int x = 0; x < Player1Field.Width; x++)
                    line += (Player1Field.Grid[x, y] == null ? '.' : Player1Field.Grid[x, y].GetSymbol()) + " ";
                line += "| ";
                for (int x = Player2Field.Width - 1; x > -1; x--)
                    line += (Player2Field.Grid[x, y] == null ? '.' : Player2Field.Grid[x, y].GetSymbol()) + " ";
                line += (y + 1);
                res.Add(line);
            }
            return [.. res];
        }
    }

    public sealed class SoftlockGame(Player p1, Player p2, int width, int height)
    {
        public GameState State { get; private set; } = new GameState(p1, p2, width, height);
        public Player? CurrentPlayer => State.CurrentPlayer; // Shorthand
        public Player? P1 => State.Player1Field.Owner;
        public Player? P2 => State.Player2Field.Owner;
        public bool IsRunning { get; private set; } = false;

        public SoftlockGame(Player p1, Player p2, int size) : this(p1, p2, size, size) { }
        public void StartGameLoop(Player whoStarts)
        {
            if (whoStarts == null) return;
            State.CurrentPlayer = whoStarts;
            IsRunning = true;
            while (IsRunning)
            {
                if (CurrentPlayer == null) break;
                
                IPlayerAction? action;
                do
                {
                    Console.Clear();
                    foreach (var line in State.GetGraphics())
                        Console.WriteLine(line);
                    action = CurrentPlayer.GetAction(State);
                }
                while (action == null || !IsActionValid(action));


                var actionResult = State.ApplyAction(CurrentPlayer, action);

                State.CurrentPlayer = CurrentPlayer == P1 ? P2 : P1;
            }
            IsRunning = false;
        }
        private bool IsActionValid(IPlayerAction action)
        {
            if (action is BuildAction build)
            {
                if (State.TryGetField(State.CurrentPlayer, out var f) &&
                    (f.Grid[build.x, build.y] is BuildingCell cell &&
                    cell.IsReplaceableBy(BuildingTypeRegistry.AllTypes[build.id]) ||
                    f.Grid[build.x, build.y] == null))
                    return true;
            }
            else if (action is ActivateAction activate)
            {
                if (State.TryGetField(State.CurrentPlayer, out var f) &&
                    f.Grid[activate.x, activate.y] is IActivateable cell &&
                    cell.CanBeActivated(State, activate))
                    return true;
            }
            return false;
        }
    }
}
