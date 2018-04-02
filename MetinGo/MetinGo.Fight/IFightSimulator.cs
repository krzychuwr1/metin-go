using MetinGo.Fight.Model;

namespace MetinGo.Fight
{
    public interface IFightSimulator
    {
        FightResult Fight(Character character, Monster monster);
    }
}