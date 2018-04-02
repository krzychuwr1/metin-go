using System;
using System.Threading.Tasks;
using MetinGo.Server.Entities;

namespace MetinGo.Server.Services
{
    public interface IFightService
    {
        Task<Entities.Fight> Fight(Guid monsterId);
    }
}