using Data;
using ReactiveSystem;
using UI;

namespace Core
{
    public interface IServiceContainer
    {
        public IEventBus EventBus { get; set; }
        public GameStateMachine GameSM { get; set; }
        public InputActions InputActions { get; set; }
        public IWindowsMediator WindowsMediator { get; set; }
    }
    public class ServiceContainer : IServiceContainer
    {
        public IEventBus EventBus { get; set; }
        public GameStateMachine GameSM { get; set; }
        public InputActions InputActions { get; set; }
        public IWindowsMediator WindowsMediator { get; set; }
    }
}