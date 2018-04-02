using MetinGo.Server.Entities;

namespace MetinGo.Server.Services
{
    public interface IFightProcessor
    {
        void ProcessFight(Entities.Fight fight);
    }
}