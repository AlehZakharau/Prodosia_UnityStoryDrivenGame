using Assets.SimpleLocalization;

namespace UI
{
    public class LocalizationController
    {
        private readonly LocalizationMenuView view;
        private readonly IWindowsMediator windowsMediator;

        public LocalizationController(LocalizationMenuView view, IWindowsMediator windowsMediator)
        {
            this.view = view;
            this.windowsMediator = windowsMediator;
            
            view.engBt.onClick.AddListener(TurnEnglish);
            view.rusBt.onClick.AddListener(TurnRussian);
        }

        private void TurnRussian()
        {
            Localization.ChangeLanguage(ELanguage.Russian);
            windowsMediator.OpenWindow(WindowType.Menu);
        }

        private void TurnEnglish()
        {
            Localization.ChangeLanguage(ELanguage.English);
            windowsMediator.OpenWindow(WindowType.Menu);
        }
    }
}