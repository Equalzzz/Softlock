namespace ConsoleSoftlock
{
    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    public interface IPowerable
    {
        public bool IsPowered { get; set; }
    }
    public interface IDirectional
    {
        public Direction Direction { get; set; }
    }
    public readonly struct Field(Player owner, int width, int height)
    {
        public Player Owner { get; } = owner;
        public ICell[,] Grid { get; } = new ICell[width, height];
        public int Width { get; } = width;
        public int Height { get; } = height;
    }
    public abstract class Player
    {
        public int Score;
        public abstract int GetAction(GameState state);
    }
    public class KeyboardPlayer : Player
    {
        public override int GetAction(GameState state)
        {
            Console.Beep();
            string? input;
            (int, int) pos;
            do
            {
                Console.Write("Input position (row column): ");
                input = Console.ReadLine();
            }
            while (!TryParseInputToPosition(input, out pos));
            Console.Write("You've inputed these coords: ");
            Console.Write(pos + "\n");
            return 0;
        }
        static bool TryParseInputToPosition(string? input, out (int x, int y) position)
        {
            position = default;
            if (input == null) return false;
            string[] arr = input.Split(' ');
            if (arr.Length != 2) return false;
            if (int.TryParse(arr[0], out int x) && int.TryParse(arr[1], out int y))
            {
                position = (x, y);
                return true;
            }
            return false;
        }
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
        public int ApplyAction(int a) { return 1; }
    }
    public interface ICell
    {

    }
    
    public sealed class SoftlockGame
    {
        public GameState State { get; private set; }
        public Player? CurrentPlayer => State.CurrentPlayer; // Shorthand
        public Player? P1 => State.Player1Field.Owner;
        public Player? P2 => State.Player2Field.Owner;
        public bool IsRunning { get; private set; } = false;
        public SoftlockGame(Player p1, Player p2, int width, int height)
        {
            State = new GameState(p1, p2, width, height);
        }
        public SoftlockGame(Player p1, Player p2, int size) : this(p1, p2, size, size) { }
        public void StartGameLoop(Player whoStarts)
        {
            if (whoStarts == null) return;
            State.CurrentPlayer = whoStarts;
            IsRunning = true;
            while (IsRunning)
            {
                if (CurrentPlayer == null) break;

                int? action;
                do action = CurrentPlayer.GetAction(State);
                while (IsActionValid(action));
                
                var actionResult = State.ApplyAction(action.Value);

                State.CurrentPlayer = CurrentPlayer == P1 ? P2 : P1;
            }
            IsRunning = false;
        }
        private bool IsActionValid(int? a)
        {
            return a != null;
        }
    }
}
