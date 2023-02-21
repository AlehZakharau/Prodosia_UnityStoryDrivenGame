using Data;
using UnityEngine;
using Utilities;

namespace Core
{
    public class BaseState : IState
    {
        protected GameStateModel gameStateModel;
        protected readonly GameStateMachine gameSm;
        protected readonly IServiceContainer sl;

        protected BaseState(GameStateMachine gameSM, GameStateModel gameStateModel)
        {
            gameSm = gameSM;
            this.gameStateModel = gameStateModel;
            sl = gameSM.ServiceContainer;
        }

        public virtual void Enter()
        {
            Debug.Log($"Enter // <color={C.Additional}>{this}</color> \\\\");
        }

        public virtual void Exit()
        {
            
        }

        public virtual void Update()
        {
        }
    }
}