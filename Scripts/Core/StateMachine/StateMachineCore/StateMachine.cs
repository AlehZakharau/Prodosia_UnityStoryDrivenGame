namespace Core
{
    public abstract class StateMachine
    {
        protected IState currentState;
        public void ChangeState(IState state)
        {
            currentState?.Exit();
            currentState = state;
            currentState.Enter();
        }

        public virtual void Tick()
        {
            currentState?.Update();
        }
    }
}