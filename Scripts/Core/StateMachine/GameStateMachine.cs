using Core.States;
using Data;
using Gameplay;
using UI;
using UnityEngine.InputSystem;

namespace Core
{
    public class StateContainer
    {
        public MenuState MenuState { get; set; }
        public GameState GameState { get; set; }

        private GameStateMachine gameStateMachine;

        public StateContainer(GameStateMachine gameStateMachine, Skipper skipper,
            StageController stageController)
        {
            this.gameStateMachine = gameStateMachine;
            MenuState = new MenuState(gameStateMachine, gameStateMachine.GameStateModel);
            GameState = new GameState(gameStateMachine, gameStateMachine.GameStateModel, skipper, stageController);
        }
    }
    public class GameStateMachine : StateMachine
    {
        public Loader Loader { get; set; }
        public MenuState MenuState { get; set; }
        public GameState GameState { get; set; }
        public GameStateModel GameStateModel { get; set; }
        public IServiceContainer ServiceContainer { get; }

        private readonly InputActions inputActions;

        public GameStateMachine(IServiceContainer serviceContainer, GameStateModel gameStateModel)
        {
            GameStateModel = gameStateModel;
            serviceContainer.GameSM = this;
            ServiceContainer = serviceContainer;
            inputActions = serviceContainer.InputActions;
            
            serviceContainer.EventBus.Subscribe<ShowMainMenuSignal>(obj => Pause());

            inputActions.Menu.Enable();
        }

        public void InjectStates(StateContainer stateContainer)
        {
            MenuState = stateContainer.MenuState;
            GameState = stateContainer.GameState;
        }

        public override void Tick()
        {
            inputActions.Menu.Pause.performed += Pause;
            base.Tick();
        }

        private void Pause(InputAction.CallbackContext obj)
        {
            Pause();
        }

        private void Pause()
        {
            if (currentState is GameState)
                ChangeState(MenuState);
            else
                ChangeState(GameState);
        }
    }
}