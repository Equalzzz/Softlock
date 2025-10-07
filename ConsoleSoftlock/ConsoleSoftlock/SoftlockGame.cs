using ConsoleSoftlock.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock
{
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

            State.Player1Field.Fill(new VoidPlace());
            State.Player2Field.Fill(new VoidPlace());

            PrintFields();

            while (IsRunning)
            {
                if (CurrentPlayer == null) break;

                if (CurrentPlayer == P1)
                    Console.Write("[P1] ");
                else
                    Console.Write("[P2] ");

                int[]? action;
                do action = CurrentPlayer.GetAction(State);
                while (IsActionValid(action));

                switch (action[0])
                {
                    case 1:
                        // Стрельба
                        break;
                    case 2:
                        if (action[3] == 1)
                        {
                            if (CurrentPlayer == P1)
                                State.Player1Field.SetCell(new Barrack(), action[2], action[1]);
                            else
                                State.Player2Field.SetCell(new Barrack(), action[2], action[1]);
                        } else if (action[3] == 2)
                        {
                            if (CurrentPlayer == P1)
                                State.Player1Field.SetCell(new Trap(), action[2], action[1]);
                            else
                                State.Player2Field.SetCell(new Trap(), action[2], action[1]);
                        }
                        break;
                }

                PrintFields();

                State.CurrentPlayer = CurrentPlayer == P1 ? P2 : P1;
            }
            IsRunning = false;
        }
        private bool IsActionValid(int[]? a)
        {
            return a == null;
        }

        private void PrintFields()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("  ");
            for (int x = 0; x < State.Player1Field.Width; x++)
            {
                Console.Write((x + 1));
            }

            Console.Write("     ");

            for (int x = 0; x < State.Player1Field.Width; x++)
            {
                Console.Write((x + 1));
            }

            Console.Write("\n");

            for (int y = 0; y < State.Player1Field.Height; y++)
            {
                Console.Write((y + 1) + " ");
                State.Player1Field.OutLine(y);
                Console.Write(" [ ] ");
                State.Player2Field.OutLine(y);
                Console.Write(" " + (y + 1) + "\n");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
