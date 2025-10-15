namespace ConsoleSoftlock.InputInterface
{
    public abstract class Player
    {
        public int Score;
        public abstract IPlayerAction? GetAction(GameState state);
    }
}
