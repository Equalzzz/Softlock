using System.Reflection;

namespace ConsoleSoftlock
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NamedBuildingAttribute(string name, string? description = null) : Attribute
    {
        public string Name { get; } = name;
        public string? Description { get; } = description;
    }
    public static class BuildingTypeRegistry
    {
        public static readonly List<Type> AllTypes = [];
        static BuildingTypeRegistry()
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
                if (type.GetCustomAttribute<NamedBuildingAttribute>() != null)
                    AllTypes.Add(type);
            //AllTypes = [.. AllTypes.OrderBy(x => x)];
        }
    }

    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
    public interface IActivateable
    {
        public bool CanBeActivated(GameState state, ActivateAction? action = null) => true;
        public void Activate(GameState state, ActivateAction? action = null);
    }
    public interface IPowerable
    {
        public bool IsPowered { get; set; }
    }
    public interface IDirectional
    {
        public Direction Direction { get; set; }
        public bool IsActivationDirectional { get; }
        public bool IsPlacementDirectional { get; }
    }
    public abstract class BuildingCell
    {
        public Player? Owner { get; set; }
        public virtual char GetSymbol() => ' ';
        public virtual bool IsReplaceableBy(Type other) => false;
    }
    [NamedBuilding("Barracks")]
    public class Barracks : BuildingCell, IDirectional, IActivateable
    {
        public Direction Direction { get; set; }
        public bool IsActivationDirectional => true;
        public bool IsPlacementDirectional => false;

        public void Activate(GameState state, ActivateAction? action = null)
        {
#pragma warning disable CA1416 // Проверка совместимости платформы
            Console.Beep(600, 500);
#pragma warning restore CA1416
        }

        public override char GetSymbol() => 'O';
    }
    [NamedBuilding("Extended barracks")]
    public class ExtendedBarracks : Barracks
    {
        public override char GetSymbol() => '0';
    }

    [NamedBuilding("Сверх пипо", "Самый длинный пипо на диком западе...")]
    public class Chlen : BuildingCell
    {
        public override char GetSymbol() => 'T';
    }
}