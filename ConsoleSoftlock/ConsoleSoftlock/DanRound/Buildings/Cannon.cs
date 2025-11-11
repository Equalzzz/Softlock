using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSoftlock.DanRound.Buildings
{
    public class Cannon : Building, Shooting, Powerable
    {
        public bool IsShootingSoliders { get; set; }
        public bool IsShootingRockets { get; set; }
        public override Vector2 Position { get; set; }
        public override char Symbol { get; set; }
        public override bool IsTransparent { get; set; }

        public Cannon (int x, int y)
        {
            IsShootingSoliders = false;
            IsShootingRockets = true;

            Position = new Vector2(x, y);

            Symbol = '=';
            IsTransparent = false;
        }

        public bool Shoot(GameField field1, GameField field2, Direction direction)
        {
            int x = Position.Pos.x;
            int y = Position.Pos.y;
            bool isScoreUp = true;

            if (direction == Direction.Right)
            {
                for (int i = x + 1; i < 8; i++)
                {
                    Building? building = field1.Field[y, i].Building;
                    if (building != null)
                    {
                        if (building is Powerable && building is Protection && ((Powerable)building).IsPowered(field1) && ((Protection)building).IsProtectingRockets)
                        {
                            isScoreUp = false;
                            break;
                        }
                        else if (!building.IsTransparent && IsPowered(field1))
                        {
                            isScoreUp = false;
                            field1.Field[y, i].Collapse();
                            break;
                        }
                    }
                }

                if (isScoreUp)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Building? building = field2.Field[y, i].Building;
                        if (building != null)
                        {
                            if (building is Powerable && building is Protection && ((Powerable)building).IsPowered(field2) && ((Protection)building).IsProtectingRockets)
                            {
                                isScoreUp = false;
                                break;
                            }
                            else if (!building.IsTransparent && IsPowered(field1))
                            {
                                isScoreUp = false;
                                field2.Field[y, i].Collapse();
                                break;
                            }
                        }
                    }
                }
            }

            if (direction == Direction.Left)
            {
                for (int i = x - 1; i >= 0; i--)
                {
                    Building? building = field1.Field[y, i].Building;
                    if (building != null)
                    {
                        if (building is Powerable && building is Protection && ((Powerable)building).IsPowered(field1) && ((Protection)building).IsProtectingRockets)
                        {
                            isScoreUp = false;
                            break;
                        }
                        else if (!building.IsTransparent && IsPowered(field2))
                        {
                            isScoreUp = false;
                            field1.Field[y, i].Collapse();
                            break;
                        }
                    }
                }

                if (isScoreUp)
                {
                    for (int i = 7; i >= 0; i--)
                    {
                        Building? building = field2.Field[y, i].Building;
                        if (building != null)
                        {
                            if (building is Powerable && building is Protection && ((Powerable)building).IsPowered(field2) && ((Protection)building).IsProtectingRockets)
                            {
                                isScoreUp = false;
                                break;
                            }
                            else if (!building.IsTransparent && IsPowered(field2))
                            {
                                isScoreUp = false;
                                field2.Field[y, i].Collapse();
                                break;
                            }
                        }
                    }
                }
            }

            return isScoreUp && IsPowered(field1);
        }

        public bool IsPowered(GameField field)
        {
            bool isPowered = false;

            int x = Position.Pos.x;
            int y = Position.Pos.y;

            if (y != 7 && field.Field[y + 1, x].Building is Barracks)
                isPowered = true;
            if (y != 0 && field.Field[y - 1, x].Building is Barracks)
                isPowered = true;
            if (x != 0 && field.Field[y, x - 1].Building is Barracks)
                isPowered = true;
            if (x != 7 && field.Field[y, x + 1].Building is Barracks)
                isPowered = true;

            return isPowered;
        }
    }
}
