using Assets.SimpleLocalization;
using ReactiveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class TutorialPlayer : MonoBehaviour
    {
        public Button answerTutorialBt;
        public Button answerGame2Bt;
        public TMP_Text answerTutorialText;
        public TMP_Text answerGameText;

        private void Awake()
        {
            Localization.LocalizationChanged += ChangeText;
            answerTutorialBt.onClick.AddListener(ShowTutorial);
            answerGame2Bt.onClick.AddListener(ShowGame);
            
            
            answerTutorialBt.gameObject.SetActive(false);
            answerGame2Bt.gameObject.SetActive(false);
        }

        private void ChangeText()
        {
            answerTutorialText.text = Localization.Localize("Message.SelectTutorial");
            answerGameText.text = Localization.Localize("Message.SkipTutorial");
        }
        private void ShowGame()
        {
            MonoEventBus.Fire(new TutorialAnswerSignal(false));
            answerTutorialBt.gameObject.SetActive(false);
            answerGame2Bt.gameObject.SetActive(false);
        }

        private void ShowTutorial()
        {
            MonoEventBus.Fire(new TutorialAnswerSignal(true));
            answerTutorialBt.gameObject.SetActive(false);
            answerGame2Bt.gameObject.SetActive(false);
        }

        public void ShowAnswers()
        {
            answerTutorialBt.gameObject.SetActive(true);
            answerGame2Bt.gameObject.SetActive(true);
        }
    }
}