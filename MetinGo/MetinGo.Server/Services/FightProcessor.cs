using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.Common;
using MetinGo.Server.Infrastructure.Database;
using MetinGo.Server.Infrastructure.Session;
using Microsoft.EntityFrameworkCore;

namespace MetinGo.Server.Services
{
    public class FightProcessor : IFightProcessor
    {
        private readonly ISessionManager _sessionManager;
        private readonly ILevelExperienceCalculator _levelExperienceCalculator;
        private readonly MetinGoDbContext _db;

        public FightProcessor(ISessionManager sessionManager, ILevelExperienceCalculator levelExperienceCalculator, MetinGoDbContext db)
        {
            _sessionManager = sessionManager;
            _levelExperienceCalculator = levelExperienceCalculator;
            _db = db;
        }

        public void ProcessFight(Entities.Fight fight)
        {
            var currentCharacter = _sessionManager.CurrentCharacter;
            currentCharacter.Experience += fight.Experience;
            var expNeeded = _levelExperienceCalculator.GetFullExpOnLevel(currentCharacter.Level + 1);
            if (currentCharacter.Experience >= expNeeded)
            {
                currentCharacter.Level += 1;
                currentCharacter.StatPoints += 4;
            }

            if (fight.PlayerWon)
                fight.Monster.IsAlive = false;
        }
    }
}
