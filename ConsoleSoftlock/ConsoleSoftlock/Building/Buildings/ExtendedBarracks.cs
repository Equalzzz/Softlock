namespace ConsoleSoftlock.Building.Buildings
{
    [NamedBuilding("Extended barracks")]
    public class ExtendedBarracks : Barracks
    {
        public override bool CanBePlacedOn(Type other) => other == typeof(Barracks);
        public override char GetSymbol() => 'ʘ'; // ʘ Ø
    }
}
