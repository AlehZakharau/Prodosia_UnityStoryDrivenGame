using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Data;
using ReactiveSystem;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class CommentsController
    {
        private readonly IEventBus eventBus;
        private readonly Script script;
        private readonly LineScriptController lineController;
        private readonly DecorationsController decorationsController;
        private readonly GameStateModel stateModel;


        private Answer currentAnswer;
        private readonly Queue<string> previousIds = new();
        private Character finishedCharacter;
        private int commentAmount;
        private int answerIndex;

        public CommentsController(IEventBus eventBus, Script script, LineScriptController lineController, 
            DecorationsController decorationsController, GameStateModel stateModel)
        {
            this.eventBus = eventBus;
            this.script = script;
            this.lineController = lineController;
            this.decorationsController = decorationsController;
            this.stateModel = stateModel;

            eventBus.Subscribe<AnswerSignal>(Answer);
        }

        public void Skip() => AfterComment();

        private void Answer(AnswerSignal obj)
        {
            currentAnswer = obj.Answer;
            if (currentAnswer == Data.Answer.Right)
            {
                answerIndex = Random.Range(0, 2);
            }
            else if (currentAnswer == Data.Answer.Wrong)
            {
                answerIndex = 2;
                if (!stateModel.hadFail)
                {
                    stateModel.hadFail = true;
                    lineController.PlayStage(Stages.BlockDialogue_Fail);
                    decorationsController.PlayDangerEnvironment();
                    return;
                }
            }
            else if (currentAnswer == Data.Answer.CriticalWrong)
            {
                answerIndex = 3;
                if (!stateModel.hadCriticalFail)
                {
                    stateModel.hadCriticalFail = true;
                    lineController.PlayStage(Stages.BlockDialogue_CriticalFail);
                    decorationsController.PlayAggressiveMother();
                    return;
                }
            }

            PlayNextComment();
        }

        private CommentLine GetComment(int index)
        {
            while (true)
            {
                var commentIndex = Random.Range(0, script.Comments.Length);
                if (script.Comments[commentIndex].disable) continue;
                var comment = script.Comments[commentIndex].CommentLines[index];
                if(CheckPreviousComments(comment.id)) continue;
                if(comment.Character == finishedCharacter) continue;
                if(previousIds.Count > 3) previousIds.Dequeue();
                previousIds.Enqueue(comment.id);
                finishedCharacter = comment.Character;
                return comment;
            }
        }

        private void AfterComment()
        {
            commentAmount++;
            eventBus.Fire(new EndLineSignal());
            if (commentAmount >= 2)
            {
                commentAmount = 0;
                eventBus.Fire(new AfterCommentSignal(currentAnswer));
            }
            else
                PlayNextComment();
        }

        private void PlayNextComment()
        {
            lineController.PlayLine(GetComment(answerIndex), AfterComment);
        }

        private bool CheckPreviousComments(string id)
        {
            return previousIds.Any(previousId => id == previousId);
        }
    }
}