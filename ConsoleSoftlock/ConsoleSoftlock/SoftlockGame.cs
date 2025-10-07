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

            while (IsRunning)
            {
                if (CurrentPlayer == null) break;

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

                /*
                var actionResult = State.ApplyAction(1); // Уточнить у Илюшки

                if (actionResult == 1)
                {
                    State.Player1Field.Out();
                    Console.WriteLine("========");
                    State.Player2Field.Out();
                }
                */

                State.Player1Field.Out();
                Console.WriteLine("========");
                State.Player2Field.Out();

                State.CurrentPlayer = CurrentPlayer == P1 ? P2 : P1;
            }
            IsRunning = false;
        }
        private bool IsActionValid(int[]? a)
        {
            return a == null;
        }
    }
}
