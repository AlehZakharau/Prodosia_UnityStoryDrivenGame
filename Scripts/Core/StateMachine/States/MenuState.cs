using Data;
using UI;
using UnityEngine;
using Utilities;

namespace Core.States
{
    public class MenuState : BaseState
    {
        public MenuState(GameStateMachine gameSM, GameStateModel gameStateModel) : base(gameSM, gameStateModel)
        {
        }

        public override void Enter()
        {
            sl.WindowsMediator.OpenWindow(WindowType.Menu);
            DataSystem.SaveFileJson("GameModel", gameStateModel);
        }

        public override void Exit()
        {
            sl.WindowsMediator.CloseAllWindows();
        }
    }
}