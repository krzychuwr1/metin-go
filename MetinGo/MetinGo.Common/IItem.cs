namespace MetinGo.Common
{
    public interface IItem
    {
        int Attack { get; set; }
        int Defence { get; set; }
        int Id { get; set; }
        int MaxHP { get; set; }
        int PerLevelAttack { get; set; }
        int PerLevelDefence { get; set; }
        int PerLevelMaxHP { get; set; }
    }
}