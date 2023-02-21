using Data;
using ReactiveSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class AnswerPlayer : MonoBehaviour
    {
        public AnswerView answer1;
        public AnswerView answer2;
        public AnswerView answer3;
        public AnswerView answer4;

        private AnswerView[] answerViews = new AnswerView[4];

        private void Awake()
        {
            answerViews[0] = answer1;
            answerViews[1] = answer2;
            answerViews[2] = answer3;
            answerViews[3] = answer4;
        }

        private void Start()
        {
            MonoEventBus.Subscribe<ShowAnswerSignal>(ShowAnswer);
        }

        public void ShowAnswer(AnswerLine[] ans)
        {
            gameObject.SetActive(true);
            FillAnswers(ans);
        }

        public void HideAnswers()
        {
            foreach (var view in answerViews) view.Clear();
        }

        public void AfterAnswer()
        {
            gameObject.SetActive(false);
            foreach (var view in answerViews) view.Clear();
        }

        public void TurnButtons(bool value)
        {
            foreach (var view in answerViews)
            {
                view.answerBt.interactable = value;
            }
        }

        private void ShowAnswer(ShowAnswerSignal obj)
        {
            FillAnswers(obj.Answers);
        }

        private void FillAnswers(AnswerLine[] answers)
        {
            var range = Random.Range(0, 3);
            for (int i = 0; i < 3; i++)
            {
                if (range >= answerViews.Length)
                    range = 0;
                answerViews[range++].AddLine(answers[i]);
            }
        }
    }
}