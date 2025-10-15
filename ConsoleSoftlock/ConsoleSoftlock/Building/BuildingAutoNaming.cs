using System.Reflection;

namespace ConsoleSoftlock.Building
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
}
