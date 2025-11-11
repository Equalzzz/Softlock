using ConsoleSoftlock.DanRound;
using System.Net;
using System.Net.Sockets;

namespace ConsoleSoftlock
{
    public static class Launcher
    {
        static string GetLogo ()
        {
            return File.ReadAllText("MainMenuLogo.txt");
        }
        static void Main()
        {
            GameField f1 = new GameField();
            GameField f2 = new GameField();

            Player p1 = new Player("p1", f1);
            Player p2 = new Player("p2", f2);

            GameManager gameManager = new GameManager(p1, p2);

            Console.ForegroundColor = ConsoleColor.Green;

            Menu MainMenu = new Menu("Режим игры:", new List<string>() { "> Игра на одном устройстве", "> Игра по сети" });
            int gameMode = MainMenu.MenuProccess(GetLogo());
            if (gameMode == 0)
                gameManager.StartGame();
            else
            {
                MainMenu = new Menu("Выберите опцию:", [ "> Создать сервер", "> Подключиться" ]);
                gameMode = MainMenu.MenuProccess(GetLogo());
                Console.WriteLine(GetLogo() + "\n Введите айпи:");
                if (gameMode == 0)
                {
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(Console.ReadLine()), 12345);
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Bind(endPoint);
                    socket.Listen(1);
                    Socket client = socket.Accept();
                    Console.Clear();
                    gameManager.StartMultiplayerGameHostSide(client);
                    
                } else
                {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(Console.ReadLine(), 12345);
                    Console.Clear();
                    gameManager.StartMultiplayerGameClientSide(socket);
                }
            }
        }
    }
}
