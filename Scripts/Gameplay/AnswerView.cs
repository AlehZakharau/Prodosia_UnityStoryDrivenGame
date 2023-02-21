using Assets.SimpleLocalization;
using Data;
using ReactiveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class AnswerView: MonoBehaviour
    {
        public TMP_Text answer;
        public Button answerBt;

        private AnswerLine currentLine;

        private void Awake()
        {
            answerBt.onClick.AddListener(Answer);
            gameObject.SetActive(false);

            Localization.LocalizationChanged += ChangeLanguage;
        }

        private void Answer()
        {
            Debug.Log($"Answer is {currentLine.Answer}");
            MonoEventBus.Fire(new AnswerSignal(currentLine.Answer));
        }

        public void AddLine(AnswerLine answerLine)
        {
            currentLine = answerLine;
            answer.text = Localization.Localize(answerLine.id);
            gameObject.SetActive(true);
        }

        private void ChangeLanguage()
        {
            if(currentLine == null) return;
            AddLine(currentLine);
        }

        public void Clear()
        {
            currentLine = null;
            answer.text = "";
            gameObject.SetActive(false);
        }
    }
}