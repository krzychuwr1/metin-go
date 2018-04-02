using System;
using MetinGo.Fight.Model;

namespace MetinGo.Fight
{
    public class FightSimulator : IFightSimulator
    {
        public FightResult Fight(Character character, Monster monster)
        {
            return character.Level >= monster.Level ? new FightResult() {PlayerWon = true} : new FightResult {PlayerWon = false};
        }
    }
}
