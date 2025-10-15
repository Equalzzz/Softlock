using ConsoleSoftlock.InputInterface;

namespace ConsoleSoftlock
{
    public static class Launcher
    {
        static void Main()
        {
            // For ability to work with most symbols needed
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Player p1 = new KeyboardPlayer();
            Player p2 = new KeyboardPlayer();

            SoftlockGame game = new(p1, p2, 8);
            game.StartGameLoop(p1);
        }
    }
}
