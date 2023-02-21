namespace Core
{
    public interface IStateModelContainer
    {
        public GameStateModel GameStateModel { get; }
        public LoadGameStateModel LoadGameStateModel { get; }
    }
    public class StateModelContainer : IStateModelContainer
    {
        public GameStateModel GameStateModel { get; }
        public LoadGameStateModel LoadGameStateModel { get; }

        public StateModelContainer()
        {
            GameStateModel = new GameStateModel();
            LoadGameStateModel = new LoadGameStateModel();
        }
    }
}