namespace UI
{
    public class HelpController
    {
        private readonly IWindowsMediator windowsMediator;

        public HelpController(HelpView helpView, IWindowsMediator windowsMediator)
        {
            this.windowsMediator = windowsMediator;

            helpView.backBt.onClick.AddListener(OpenMainMenu);
        }

        private void OpenMainMenu()
        {
            windowsMediator.OpenWindow(WindowType.Menu);
        }
    }
}