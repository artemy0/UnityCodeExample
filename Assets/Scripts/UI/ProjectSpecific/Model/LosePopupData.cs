using System;

namespace UI.ProjectSpecific.Model
{
    public struct LosePopupData
    {
        public bool IsRestoreLevelRewardVideoReady;

        public Action OnRestartLevelClicked;
        public Action OnRestoreLevelClicked;
        public Action OnMoveToLobbyClicked;
    }
}