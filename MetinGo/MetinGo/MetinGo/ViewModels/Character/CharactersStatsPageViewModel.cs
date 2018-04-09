using System;
using System.Collections.Generic;
using System.Text;
using MetinGo.Infrastructure.Session;

namespace MetinGo.ViewModels.Character
{
    public class CharactersStatsPageViewModel : ObservableObject
    {
        private readonly ISessionManager _sessionManager;

        public CharactersStatsPageViewModel(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public Models.Character.Character Character => _sessionManager.Character;
    }
}
