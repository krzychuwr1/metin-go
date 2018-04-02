﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.ApiModel;
using MetinGo.ApiModel.Fight;
using MetinGo.ApiModel.Monster;
using MetinGo.Fight;
using MetinGo.Server.Infrastructure.Database;
using MetinGo.Server.Infrastructure.Filters;
using MetinGo.Server.Infrastructure.Session;
using MetinGo.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MetinGo.Server.Controllers
{
    [Route("api/" + Endpoints.Fight)]
    public class FightController : Controller
    {
        private readonly IFightService _fightService;
        private readonly IFightProcessor _fightProcessor;
        private readonly MetinGoDbContext _db;
        private readonly ISessionManager _sessionManager;

        public FightController(IFightService fightService, IFightProcessor fightProcessor, MetinGoDbContext db, ISessionManager sessionManager)
        {
            _fightService = fightService;
            _fightProcessor = fightProcessor;
            _db = db;
            _sessionManager = sessionManager;
        }

        [HttpPost]
        [ServiceFilter(typeof(UserContextFilter))]
        [ServiceFilter(typeof(CharacterContextFilter))]
        [ServiceFilter(typeof(PositionContextFilter))]
        public async Task<IActionResult> Post(FightRequest request)
        {
            var fightResult = await _fightService.Fight(request.MonsterId);
            _fightProcessor.ProcessFight(fightResult);
            await _db.SaveChangesAsync();
            return Ok(new FightResponse {Experience = fightResult.Experience, PlayerWon = fightResult.PlayerWon, LevelAfterFight = _sessionManager.CurrentCharacter.Level});
        }
    }
}
