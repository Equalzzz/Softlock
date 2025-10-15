using ConsoleSoftlock.Building;
using ConsoleSoftlock.Building.Buildings;
using System.Reflection;

namespace ConsoleSoftlock.InputInterface
{
    public class KeyboardPlayer : Player
    {
        public override IPlayerAction? GetAction(GameState state)
        {
            // Utils
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

            // Data
            bool isBuild;
            int row = -1, column = -1, ID = -1;
            Direction direction = Direction.None;

            // Validate action choice
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

            // Validate X coord
            while (column == -1)
            {
                state.TryGetField(this, out var field);
                string token = GetNextToken($"Input collumn [1-{field.Width}]: ");
                if (int.TryParse(token, out int x) && x >= 1 && x <= field.Width)
                    column = x - 1;
                else
                    PrintError($"Incorrect input. You can only input an integer in range [1-{field.Width}].");
            }

            // Validate Y coord
            while (row == -1)
            {
                state.TryGetField(this, out var field);
                string token = GetNextToken($"Input row [1-{field.Height}]: ");
                if (int.TryParse(token, out int y) && y >= 1 && y <= field.Height)
                    row = y - 1;
                else
                    PrintError($"Incorrect input. You can only input an integer in range [1-{field.Height}].");
            }

            // Validate building id
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

            // Validate direction choice
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

            // Final validation
            if (isBuild)
            {
                if (f.Grid[column, row] is BuildingCell cell && !cell.IsReplaceableBy(types[ID]))
                {
                    PrintError($"Incorrect placement. You cannot replace {cell.GetType().GetCustomAttribute<NamedBuildingAttribute>()?.Name} building by {types[ID].GetCustomAttribute<NamedBuildingAttribute>()?.Name}\nPress ANY to re-enter values...");
                    Console.ReadKey();
                }
                else if (Activator.CreateInstance(types[ID]) is BuildingCell thisCell && !thisCell.CanBePlacedOn(f.Grid[column, row]?.GetType()!))
                {
                    PrintError($"Incorrect placement. You cannot place {thisCell.GetType().GetCustomAttribute<NamedBuildingAttribute>()?.Name} building on top of {(f.Grid[column, row] == null ? "empty cell" : f.Grid[column, row].GetType().GetCustomAttribute<NamedBuildingAttribute>()?.Name)}\nPress ANY to re-enter values...");
                    Console.ReadKey();
                }
                else return new BuildAction(column, row, ID, direction);
            }
            else
            {
                var action = new InteractAction(column, row, direction);
                if (f.Grid[column, row] is IInteractive cell && !cell.CanBeInteracted(state, action))
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
