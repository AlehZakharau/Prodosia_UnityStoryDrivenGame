namespace UI
{
    public class CreditsController
    {
        private readonly CreditsView creditsView;
        private readonly IWindowsMediator windowsMediator;

        public CreditsController(CreditsView creditsView, IWindowsMediator windowsMediator)
        {
            this.creditsView = creditsView;
            this.windowsMediator = windowsMediator;
            
            creditsView.backBt.onClick.AddListener(Back);
        }
        private void Back() => windowsMediator.OpenWindow(WindowType.Menu);
    }
}