using System;
using System.Collections.Generic;
using Core;
using Core.States;

namespace UI
{
    public interface IWindowsMediator
    {
        public void OpenWindow(WindowType windowType);
        public void CloseAllWindows();
        public void OpenPrevious();
    }
    public class WindowsMediator : IWindowsMediator
    {
        private readonly GameStateMachine gameStateMachine;
        private Dictionary<WindowType, IView> windows = new();

        private IView activeWindow;
        private IView previousWindow;
        public WindowsMediator(ViewDataBase viewDataBase, IServiceContainer sl, GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
            windows = viewDataBase.Views;
            
            sl.EventBus.Subscribe<OpenWindowSignal>(OpenWindow);
            sl.EventBus.Subscribe<ShowHelpSignal>(OpenHelp);
        }

        public void OpenWindow(OpenWindowSignal obj)
        {
            previousWindow = activeWindow;
            activeWindow?.Hide();
            activeWindow = windows[obj.WindowType];
            activeWindow.Show();
        }

        public void OpenWindow(WindowType windowType)
        {
            previousWindow = activeWindow;
            activeWindow?.Hide();
            activeWindow = windows[windowType];
            activeWindow.Show();
        }

        public void CloseAllWindows()
        {
            foreach (var window in windows)
            {
                window.Value.Hide();
            }
        }

        public void OpenPrevious()
        {
            activeWindow?.Hide();
            (previousWindow, activeWindow) = (activeWindow, previousWindow);
            activeWindow?.Show();
        }

        private void OpenHelp(ShowHelpSignal obj)
        {
            gameStateMachine.ChangeState(gameStateMachine.MenuState);
            OpenWindow(WindowType.Help);
        }
    }
}