using System;
using Data;
using Gameplay;
using UnityEngine.InputSystem;

namespace Core.States
{
    public sealed class GameState : BaseState
    {
        private readonly Skipper skipper;
        private readonly StageController stageController;
        private readonly InputActions inputActions;

        public GameState(GameStateMachine gameSm,GameStateModel gameStateModel, Skipper skipper, 
            StageController stageController) : base(gameSm, gameStateModel)
        {
            this.skipper = skipper;
            this.stageController = stageController;
            inputActions = gameSm.ServiceContainer.InputActions;
        }
        public override void Enter()
        {
            stageController.StartScript(gameStateModel);

            inputActions.Player.Enable();
            inputActions.Player.Skip.performed += Skip;
            base.Enter();
        }
        public override void Exit()
        {
            inputActions.Player.Disable();
            inputActions.Player.Skip.performed -= Skip;
            stageController.Stop();
            base.Exit();
        }
        private void Skip(InputAction.CallbackContext obj) => skipper.Skip();
    }
}