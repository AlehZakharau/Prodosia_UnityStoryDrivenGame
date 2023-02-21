using System;
using Data;
using Data.Events;
using Gameplay;
using ReactiveSystem;
using UI;

namespace Core.States
{
    public class StageController
    {
        private GameStateModel model;
        private readonly DecorationsController decorationsController;
        private readonly AnswerController answerController;
        private readonly LineScriptController lineController;
        private readonly Script script;
        private readonly IEventBus eventBus;
        private readonly IWindowsMediator windowsMediator;

        public StageController(GameStateModel model, DecorationsController decorationsController,
            AnswerController answerController, LineScriptController lineController, Script script, IEventBus eventBus,
            IWindowsMediator windowsMediator)
        {
            this.model = model;
            this.decorationsController = decorationsController;
            this.answerController = answerController;
            this.lineController = lineController;
            this.script = script;
            this.eventBus = eventBus;
            this.windowsMediator = windowsMediator;

            eventBus.Subscribe<EndStageSignal>(ChangeStage);
            eventBus.Subscribe<TutorialAnswerSignal>(TutorialAnswer);
            eventBus.Subscribe<EndMessageGroupSignal>(MessageAnswer);
            eventBus.Subscribe<AfterCommentSignal>(Answer);
            eventBus.Subscribe<TurnButtonsSignal>(TurnButtons);
        }

        private void TurnButtons(TurnButtonsSignal obj)
        {
            model.firstMessageChoose = true;
            answerController.TurnButtons(true);
        }

        public void StartScript(GameStateModel gameStateModel)
        {
            model = gameStateModel;
            decorationsController.HideSymbol();
            decorationsController.ShowDecoration(model.decoration);
            decorationsController.PlayGameMusic();
            if(!model.motherIdle)
                decorationsController.ShowNewMotherPose(model.motherStage, model.life);
            if(model.tutorialChoose)
                answerController.ShowTutorialChoose();
            else if (model.messageChoose)
                answerController.LoadGroup();
            else if(model.firstMessageChoose)
            {
                eventBus.Fire(new ShowSunSignal());
                eventBus.Fire(new ShowFirstAnswersSignal());
                model.firstMessageChoose = true;
                answerController.TurnButtons(true);
            }
            else if (model.winEffectChoose)
            {
                decorationsController.ShowNewMotherPose(model.motherStage, model.life);
                decorationsController.ShowWinEffect(new ShowWinEffectSignal());
                lineController.PlayStage(model.stage, model.line);
                answerController.Default();
            }
            else
            {
                lineController.PlayStage(model.stage, model.line);
                answerController.Default();
            }
        }

        public void Stop()
        {
            eventBus.Fire(new EndLineSignal());
            lineController.StopPlayLine();
            decorationsController.StopShake();
            decorationsController.StopWinEffect();
            decorationsController.HideSymbol();
            decorationsController.MotherIdle(true);
            answerController.HideAnswers();
        }

        private void TutorialAnswer(TutorialAnswerSignal obj)
        {
            lineController.PlayStage(obj.Value ? Stages.BlockDialogueTutorial : Stages.BlockDialogue_2);
            model.tutorialChoose = false;
        }

        private void Answer(AfterCommentSignal obj)
        {
            model.life += obj.Answer switch
            {
                Data.Answer.Right => +0,
                Data.Answer.Wrong => -1,
                Data.Answer.CriticalWrong => -2,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            decorationsController.ChangeMotherStatus(model.life);
            if (model.life > 0) answerController.ShowNextSymbol();
            else
            {
                lineController.PlayStage(Stages.BlockDialogue_CriticalFailEndGame);
                decorationsController.PlayGameOverMusic();
                model.stage = Stages.BlockDialogue_CriticalFailEndGame;
            }
        }

        private void MessageAnswer(EndMessageGroupSignal obj)
        {
            switch (obj.EndGroupIndex)
            {
                case 0 :
                    lineController.PlayStage(Stages.BlockDialogue_3);
                    decorationsController.AnswerEffect(false);
                    model.motherIdle = false;
                    decorationsController.ShowNewMotherPose(model.motherStage, model.life);
                    break;
                case 1 :
                    lineController.PlayStage(Stages.BlockDialogue_4);
                    decorationsController.AnswerEffect(false);
                    model.motherStage = 1;
                    decorationsController.ShowNewMotherPose(model.motherStage, model.life);
                    break;
                case 2: 
                    lineController.PlayStage(Stages.BlockDialogue_5);
                    decorationsController.AnswerEffect(false);
                    model.motherStage = 2;
                    decorationsController.ShowNewMotherPose(model.motherStage, model.life);
                    break;
                case 3:
                    lineController.PlayStage(Stages.BlockDialogue_6);
                    decorationsController.AnswerEffect(false);
                    model.motherIdle = true;
                    break;
            }
        }

        private void ChangeStage(EndStageSignal obj)
        {
            switch (obj.LastStageName)
            {
                case Stages.Intro:
                    lineController.PlayStage(Stages.BlockDialogue_1);
                    model.stage = Stages.BlockDialogue_1;
                    model.decoration = Decorations.Scene;
                    decorationsController.ShowDecoration(Decorations.Scene);
                    decorationsController.ShakeCamera();
                    decorationsController.AnswerEffect(false);
                    break;
                case Stages.GameOver:
                    model.Default();
                    windowsMediator.OpenWindow(WindowType.Menu);
                    break;
                case Stages.Epilogue:
                    model.Default();
                    windowsMediator.OpenWindow(WindowType.Credits);
                    break;
                case Stages.BlockDialogue_1:
                    answerController.ShowTutorialChoose();
                    model.tutorialChoose = true;
                    break;
                case Stages.BlockDialogue_2:
                    break;
                case Stages.BlockDialogue_3:
                    answerController.ShowNextGroupSymbols();
                    break;
                case Stages.BlockDialogue_4:
                    answerController.ShowNextGroupSymbols();
                    decorationsController.ChangePlatform();
                    script.DisableCharacter(Character.Pacific, true);
                    break;
                case Stages.BlockDialogue_5:
                    answerController.ShowNextGroupSymbols();
                    break;
                case Stages.BlockDialogue_6:
                    lineController.PlayStage(Stages.BlockDialogue_Win);
                    model.stage = Stages.BlockDialogue_Win;
                    decorationsController.PlayEpilogueMusic();
                    break;
                case Stages.BlockDialogue_Win:
                    lineController.PlayStage(Stages.Epilogue);
                    model.stage = Stages.Epilogue;
                    decorationsController.ShowDecoration(Decorations.Epilogue);
                    break;
                case Stages.BlockDialogue_Fail:
                    FailAction(1);
                    break;
                case Stages.BlockDialogue_CriticalFail:
                    FailAction(2);
                    break;
                case Stages.BlockDialogue_CriticalFailEndGame:
                    lineController.PlayStage(Stages.GameOver);
                    model.stage = Stages.GameOver;
                    script.DisableCharacter(Character.Pacific, false);
                    decorationsController.ShowDecoration(Decorations.GameOver);
                    break;
                case Stages.BlockDialogueTutorial:
                    lineController.PlayStage(Stages.BlockDialogue_2);
                    model.stage = Stages.BlockDialogue_2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FailAction(int damage)
        {
            model.life -= damage; // TODO change to animation and effect during dialogue
            decorationsController.ChangeMotherStatus(model.life);
            answerController.ShowNextSymbol();
        }
    }
}