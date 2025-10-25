using ConsoleSoftlock.DanRound.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock.DanRound
{
    public class GameManager
    {
        public Player Player1;
        public Player Player2;

        public Player? CurrentPlayer;

        public GameManager(Player p1, Player p2)
        {
            Player1 = p1;
            Player2 = p2;
        }

        public void PrintField ()
        {
            Console.WriteLine(GetField());
        }

        public string GetField()
        {
            string field = "";
            field += Player1.Score + " 1 2 3 4 5 6 7 8 | 1 2 3 4 5 6 7 8 " + Player2.Score + "\n";
            for (int y = 0; y < 8; y++)
            {
                field += (y + 1).ToString() + " ";
                for (int x = 0; x < 8; x++)
                {
                    field += Player1.Field.Field[y, x].Symbol + " ";
                }
                field += "| ";
                for (int x = 0; x < 8; x++)
                {
                    field += Player2.Field.Field[y, x].Symbol + " ";
                }
                field += (y + 1).ToString() + "\n";
            }
            field += "Ход " + CurrentPlayer.Name + "\n";
            return field;
        }

        public void StartGame ()
        {
            CurrentPlayer = Player1;
            bool GameProcess = true;
            while (GameProcess)
            {
                Action(CurrentPlayer);

                if (CurrentPlayer.Score == 8)
                {
                    Console.WriteLine(CurrentPlayer.Name + " победил!");
                    GameProcess = false;
                }

                CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;
            }
        }

        public void Action (Player player)
        {
            List<string> options = new List<string>() { "> Возведение постройки", "> Активация постройки" };
            Menu actionMenu = new Menu("Выбор действия:", options);

            switch(actionMenu.MenuProccess(GetField()))
            {
                case 0:
                    options = new List<string>() { "> Казарма", "> Пушка", "> Ловушка", "> Стена" };
                    Menu buildingsMenu = new Menu("Выбор постройки:", options);
                    int building = buildingsMenu.MenuProccess(GetField());
                    List<Building> buildings = new List<Building>() { new Barracks (0, 0), new Cannon(0, 0), new Trap(0, 0), new Wall(3, 0) };
                    options = new List<string>() { "> 1", "> 2", "> 3", "> 4", "> 5", "> 6", "> 7", "> 8" };
                    Menu coordMenu = new Menu("Выбор координаты X:", options);
                    int x = coordMenu.MenuProccess(GetField());
                    coordMenu = new Menu("Выбор координаты Y:", options);
                    int y = coordMenu.MenuProccess(GetField());
                    buildings[building].Position = new Vector2(x, y);
                    if (!player.Field.Field[y, x].IsCollapsed && player.Field.Field[y, x].Building == null)
                        player.Field.Field[y, x].SetBuilding(buildings[building]);
                    break;
                case 1:
                    options = new List<string>() { "> 1", "> 2", "> 3", "> 4", "> 5", "> 6", "> 7", "> 8" };
                    Menu bcoordMenu = new Menu("Выбор координаты X:", options);
                    int bx = bcoordMenu.MenuProccess(GetField());
                    coordMenu = new Menu("Выбор координаты Y:", options);
                    int by = coordMenu.MenuProccess(GetField());
                    Building b = player.Field.Field[by, bx].Building;
                    if (b is Shooting)
                    {
                        Direction dir;
                        if (player == Player1)
                            dir = Direction.Right;
                        else
                            dir = Direction.Left;

                        if (((Shooting)b).Shoot(player.Field, CurrentPlayer == Player1 ? Player2.Field : Player1.Field, dir))
                            player.Score++;
                    }
                    break;
            }
        }
    }
}
