using Core;
using Data;
using Data.Events;
using ReactiveSystem;

namespace Gameplay
{
    public class AnswerController
    {
        private readonly AnswerPlayer answerPlayer;
        private readonly TutorialPlayer tutorialPlayer;
        private readonly DecorationsController decorationsController;
        private readonly GameStateModel model;

        private readonly Script script;
        private readonly IEventBus eventBus;

        private Message[] currentGroup;
        private int currentGroupIndex;
        private int currentIndex;
        
        private Message firstMessage = Message.Sun;

        public AnswerController(AnswerPlayer answerPlayer, TutorialPlayer tutorialPlayer, Script script,
            IEventBus eventBus, DecorationsController decorationsController, GameStateModel model)
        {
            this.answerPlayer = answerPlayer;
            this.tutorialPlayer = tutorialPlayer;
            this.script = script;
            this.eventBus = eventBus;
            this.decorationsController = decorationsController;
            this.model = model;

            this.tutorialPlayer.gameObject.SetActive(false);
            this.answerPlayer.gameObject.SetActive(false);
            
            eventBus.Subscribe<AnswerSignal>(Answer);
            eventBus.Subscribe<ShowSunSignal>(ShowSun);
            eventBus.Subscribe<ShowFirstAnswersSignal>(ShowAnswers);
            //eventBus.Subscribe<ShowNewSignSignal>(ShowNewSign);
        }

        private void ShowAnswers(ShowFirstAnswersSignal obj)
        {
            answerPlayer.ShowAnswer(script.GetAnswer(firstMessage));
            answerPlayer.TurnButtons(false);
        }

        public void TurnButtons(bool value)
        {
            answerPlayer.TurnButtons(value);
        }

        private void ShowNewSign(ShowNewSignSignal obj)
        {
            currentIndex = 0;
            currentGroup = script.GetMessages(currentGroupIndex);
            var message = GetNextAnswer(currentGroup);
            decorationsController.ShowSymbol(script.GetSymbol(message));
        }

        private void ShowSun(ShowSunSignal obj)
        {
            currentIndex = 0;
            currentGroup = script.GetMessages(currentGroupIndex);
            firstMessage = GetNextAnswer(currentGroup);
            decorationsController.ShowSymbol(script.GetSymbol(firstMessage));
        }

        private void Answer(AnswerSignal obj)
        {
            model.firstMessageChoose = false;
            answerPlayer.AfterAnswer();
            decorationsController.HideSymbol();
            decorationsController.AnswerEffect(obj.Answer == Data.Answer.Right);
        }

        public void ShowTutorialChoose() => tutorialPlayer.ShowAnswers();

        public void ShowNextSymbol()
        {
            var message = GetNextAnswer(currentGroup);
            if (message == Message.EndGroup)
            {
                eventBus.Fire(new EndMessageGroupSignal(currentGroupIndex));
                currentGroupIndex++;
                currentIndex = 0;
                model.messageGroupIndex = currentGroupIndex;
                model.messageChoose = false;
                model.messageIndex = currentIndex;
            }
            else
            {
                model.messageIndex = currentIndex;
                answerPlayer.ShowAnswer(script.GetAnswer(message));
                decorationsController.ShowSymbol(script.GetSymbol(message));
            }
        }

        public void ShowNextGroupSymbols()
        {
            currentGroup = script.GetMessages(currentGroupIndex);
            model.messageChoose = true;
            model.messagesGroup = currentGroup;
            var message = GetNextAnswer(currentGroup);
            answerPlayer.ShowAnswer(script.GetAnswer(message));
            decorationsController.ShowSymbol(script.GetSymbol(message));
        }

        private Message GetNextAnswer(Message[] group)
        {
            if (group.Length <= currentIndex)
            {
                return Message.EndGroup;
            }
            var message = group[currentIndex++];
            model.messageIndex = currentIndex;
            return message;
        }

        public void LoadGroup()
        {
            answerPlayer.HideAnswers();
            currentGroupIndex = model.messageGroupIndex;
            currentIndex = model.messageIndex - 1;
            currentGroup = model.messagesGroup;
            ShowNextSymbol();
        }

        public void Default()
        {
            currentGroupIndex = 0;
            currentIndex = 0;
            currentGroup = null;
        }

        public void HideAnswers()
        {
            answerPlayer.HideAnswers();
        }
    }
}