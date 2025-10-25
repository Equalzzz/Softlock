using ConsoleSoftlock.DanRound;

namespace ConsoleSoftlock
{
    public static class Launcher
    {
        static void Main()
        {
            // For ability to work with most symbols needed
            //Console.OutputEncoding = System.Text.Encoding.UTF8;
            //Player p1 = new KeyboardPlayer();
            //Player p2 = new KeyboardPlayer();

            //SoftlockGame game = new(p1, p2, 8);
            //game.StartGameLoop(p1);

            GameField f1 = new GameField();
            GameField f2 = new GameField();

            Player p1 = new Player("p1", f1);
            Player p2 = new Player("p2", f2);

            GameManager gameManager = new GameManager(p1, p2);

            Console.ForegroundColor = ConsoleColor.Green;

            gameManager.StartGame();
        }
    }
}
