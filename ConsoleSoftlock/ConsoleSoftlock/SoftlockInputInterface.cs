using System;
using System.Reflection;

namespace ConsoleSoftlock
{
    public interface IPlayerAction
    {
        public void PerformAction(GameState state);
    }
    public readonly struct BuildAction(int x, int y, int id, Direction direction) : IPlayerAction
    {
        public readonly int x = x, y = y, id = id;
        public readonly Direction direction = direction;

        public void PerformAction(GameState state)
        {
            if (!state.TryGetField(state.CurrentPlayer, out var field)) return;
            if (Activator.CreateInstance(BuildingTypeRegistry.AllTypes[id]) is BuildingCell cell)
                field.Grid[x, y] = cell;
        }
    }
    public readonly struct ActivateAction(int x, int y, Direction direction) : IPlayerAction
    {
        public readonly int x = x, y = y;
        public readonly Direction direction = direction;

        public void PerformAction(GameState state)
        {
            // TODO: 
            Console.Beep();
        }
    }
    public abstract class Player
    {
        public int Score;
        public abstract IPlayerAction? GetAction(GameState state);
    }
    public class KeyboardPlayer : Player
    {
        public override IPlayerAction? GetAction(GameState state)
        {
            // Вспомогательные методы
            Queue<string> inputQueue = new();
            string GetNextToken(string prompt)
            {
                if (inputQueue.Count > 0)
                    return inputQueue.Dequeue();
                Console.Write(prompt);
                string? input = "";
                while (string.IsNullOrEmpty(input))
                    input = Console.ReadLine()?.Trim().ToLower();
                var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (string token in tokens)
                    inputQueue.Enqueue(token);
                return inputQueue.Dequeue();
            }
            void PrintError(string errmsg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(errmsg);
                Console.ResetColor();
            }
            
            // Данные
            bool isBuild;
            int row = -1, column = -1, ID = -1;
            Direction direction = Direction.None;
            
            // Валидность выбора действия
            while (true) 
            {
                string token = GetNextToken("Build or Activate? (B/A): ");
                if (token.Length != 1 || token[0] != 'a' && token[0] != 'b')
                {
                    PrintError("Incorrect input. You can only input [ab].");
                    inputQueue.Clear();
                    continue;
                }
                isBuild = token[0] == 'b';
                break;
            }
            
            // Валидность координаты x
            while (column == -1) 
            {
                state.TryGetField(this, out var field);
                string token = GetNextToken($"Input col [1-{field.Width}]: ");
                if (int.TryParse(token, out int x) && x >= 1 && x <= field.Width)
                    column = x - 1;
                else
                    PrintError($"Incorrect input. You can only input an integer in range [1-{field.Width}].");
            }
            
            // Валидность координаты y
            while (row == -1) 
            {
                state.TryGetField(this, out var field);
                string token = GetNextToken($"Input row [1-{field.Height}]: ");
                if (int.TryParse(token, out int y) && y >= 1 && y <= field.Height)
                    row = y - 1;
                else
                    PrintError($"Incorrect input. You can only input an integer in range [1-{field.Height}].");
            }
            
            // Валидность выбора id постройки
            var types = BuildingTypeRegistry.AllTypes;
            if (isBuild)
            {
                while (ID == -1)
                {
                    string token = GetNextToken($"Input building ID [1-{types.Count}] or 'help' for building list: ");
                    if (token == "help")
                    {
                        for (int i = 0; i < BuildingTypeRegistry.AllTypes.Count; i++)
                        {
                            var attribute = BuildingTypeRegistry.AllTypes[i].GetCustomAttribute<NamedBuildingAttribute>()!;
                            if (attribute.Description == null)
                                Console.WriteLine($"{i + 1}. {attribute.Name}");
                            else
                                Console.WriteLine($"{i + 1}. {attribute.Name} - {attribute.Description}");
                        }
                        continue;
                    }
                    if (int.TryParse(token, out int id) && id >= 1 && id <= types.Count)
                        ID = id - 1;
                    else
                        PrintError($"Incorrect input. You can only input an integer in range [1-{types.Count}] or 'help'.");
                }
            }

            // Валидность выбора направления
            if (!state.TryGetField(this, out Field f)) return null;
            if (isBuild && types[ID] is IDirectional directionalBuild && directionalBuild.IsPlacementDirectional ||
                !isBuild && f.Grid[column, row] is IDirectional foundDirectionalBuild && foundDirectionalBuild.IsActivationDirectional)
            {
                while (direction == Direction.None)
                {
                    string token = GetNextToken($"Input direction (RIGHT/R/1, LEFT/L/2, UP/U/3, DOWN/D/4): ");
                    if (int.TryParse(token, out int dir))
                        direction = dir switch
                        {
                            1 => Direction.Right,
                            2 => Direction.Left,
                            3 => Direction.Up,
                            4 => Direction.Down,
                            _ => Direction.None
                        };
                    else
                        direction = token switch
                        {
                            "r" or "right" => Direction.Right,
                            "l" or "left" => Direction.Left,
                            "u" or "up" => Direction.Up,
                            "d" or "down" => Direction.Down,
                            _ => Direction.None,
                        };
                    if (direction == Direction.None)
                        PrintError("Incorrect input. You can only input an integer in range [1-4] or letters [rlud] or words [right, left, up, down].");
                }
            }
            
            // Финальная валидация 
            if (isBuild)
            {
                if (f.Grid[column, row] is BuildingCell cell && !cell.IsReplaceableBy(types[ID]))
                {
                    PrintError($"Incorrect placement. You cannot replace {cell.GetType().GetCustomAttribute<NamedBuildingAttribute>()?.Name} building by {types[ID].GetCustomAttribute<NamedBuildingAttribute>()?.Name}\nPress ANY to re-enter values...");
                    Console.ReadKey();
                }
                else return new BuildAction(column, row, ID, direction);
            }
            else
            {
                var action = new ActivateAction(column, row, direction);
                if (f.Grid[column, row] is IActivateable cell && !cell.CanBeActivated(state, action))
                {
                    PrintError($"Incorrect action. You currently cannot activate building {cell.GetType().GetCustomAttribute<NamedBuildingAttribute>()?.Name}\nPress ANY to re-enter values...");
                    Console.ReadKey();
                }
                else return action;
            }
            return null;
        }
    }
}
