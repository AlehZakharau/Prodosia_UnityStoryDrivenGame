using Core;
using Data;
using ReactiveSystem;
using UnityEngine;

namespace UI
{
    public class MenuController
    {
        private readonly MenuView menuView;
        private readonly IEventBus eventBus;
        private readonly GameStateMachine gameSM;
        private GameStateModel gameStateModel;
        private readonly Script script;
        private readonly WindowsMediator windowsMediator;

        public MenuController(MenuView menuView, IServiceContainer sl, 
            GameStateModel gameStateModel, Script script, WindowsMediator windowsMediator)
        {
            this.menuView = menuView;
            this.gameStateModel = gameStateModel;
            this.script = script;
            this.windowsMediator = windowsMediator;

            eventBus = sl.EventBus;
            gameSM = sl.GameSM;

            this.menuView.ShowingContinueBt(!this.gameStateModel.isNewGame);
            this.menuView.OnOpening += () => menuView.ShowingContinueBt(!gameStateModel.isNewGame);

            this.menuView.startGameBt.onClick.AddListener(StartGame);
            this.menuView.settingsBt.onClick.AddListener(OpenSettings);
            this.menuView.creditsBt.onClick.AddListener(OpenCredits);
            this.menuView.helpBt.onClick.AddListener(OpenHelp);
            this.menuView.exitBt.onClick.AddListener(Exit);
            this.menuView.continueBt.onClick.AddListener(Continue);
        }


        private void Exit() => Application.Quit();
        private void OpenHelp() => windowsMediator.OpenWindow(WindowType.Help);
        private void OpenCredits() => windowsMediator.OpenWindow(WindowType.Credits);
        private void OpenSettings() => windowsMediator.OpenWindow(WindowType.Settings);
        private void Continue()
        {
            gameSM.ChangeState(gameSM.GameState);
            menuView.Hide();
        }
        private void StartGame()
        {
            gameStateModel.Default();
            gameStateModel.isNewGame = false;
            script.DefaultEvents();
            DataSystem.SaveFileJson("BaseModel", gameStateModel);
            gameSM.ChangeState(gameSM.GameState);
            menuView.Hide();
        }
    }
}