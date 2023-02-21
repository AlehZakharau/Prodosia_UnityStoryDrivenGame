using ReactiveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public struct ShowHelpSignal { }
    public struct ShowMainMenuSignal { }

    public class HelpButtonView : MonoBehaviour
    {
        public Button helpBt;
        public Button pauseBt;


        private void Awake()
        {
            helpBt.onClick.AddListener(ShowHelp);
            pauseBt.onClick.AddListener(ShowMainMenu);
        }
        
        public void ShowPauseButton()
        {
            pauseBt.gameObject.SetActive(true);
        }

        private void ShowMainMenu()
        {
            MonoEventBus.Fire(new ShowMainMenuSignal());
            pauseBt.gameObject.SetActive(false);
        }

        private void ShowHelp()
        {
            MonoEventBus.Fire(new ShowHelpSignal());
            helpBt.gameObject.SetActive(false);
        }
    }
}