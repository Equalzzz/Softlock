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

                int? action;
                do action = CurrentPlayer.GetAction(State);
                while (IsActionValid(action));

                // Как я и говорил, пиздец ниже починим, но для этого нужен Илюша
                /*
                switch (action)
                {
                    case 1:
                        if (CurrentPlayer == P1)
                            State.Player1Field.Grid[0, 0] = new Barrack();
                        break;
                }
                */
                // А пока предположим что в 100% случаев игрок ставит казарму

                if (CurrentPlayer == P1)
                    State.Player1Field.Grid[(int)(action % 10), (int)(action / 10)] = new Barrack();
                else
                    State.Player2Field.Grid[(int)(action % 10), (int)(action / 10)] = new Barrack();

                //-----------------------------------------------

                var actionResult = State.ApplyAction(action.Value);

                if (actionResult == 1)
                {
                    State.Player1Field.Out();
                    Console.WriteLine("========");
                    State.Player2Field.Out();
                }

                State.CurrentPlayer = CurrentPlayer == P1 ? P2 : P1;
            }
            IsRunning = false;
        }
        private bool IsActionValid(int? a)
        {
            return a == null;
        }
    }
}
