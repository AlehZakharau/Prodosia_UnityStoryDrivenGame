using Data;

namespace Gameplay
{
    public struct EndStageSignal
    {
        public Stages LastStageName;

        public EndStageSignal(Stages currentLastStage)
        {
            LastStageName = currentLastStage;
        }
    }
    
    public struct TutorialAnswerSignal
    {
        public bool Value;
        public TutorialAnswerSignal(bool value)
        {
            Value = value;
        }
    }

    public struct EndLineSignal
    {
        
    }

    public struct AnswerSignal
    {
        public Answer Answer;

        public AnswerSignal(Answer answer)
        {
            Answer = answer;
        }
    }
    public struct AfterCommentSignal
    {
        public Answer Answer;

        public AfterCommentSignal(Answer answer)
        {
            Answer = answer;
        }
    }

    public struct ShowAnswerSignal
    {
        public AnswerLine[] Answers;

        public ShowAnswerSignal(AnswerLine[] answers)
        {
            Answers = answers;
        }
    }

    public struct StartLineSignal
    {
        public Character Character;
        public string Id;
        
        public StartLineSignal(Character actor, string id)
        {
            Character = actor;
            Id = id;
        }
    }

    public struct StartCommentSignal
    {
        public Character Character;
        public string Id;
        
        public StartCommentSignal(Character actor, string id)
        {
            Character = actor;
            Id = id;
        }
    }
    
    
    public struct EndMessageGroupSignal
    {
        public int EndGroupIndex;

        public EndMessageGroupSignal(int endGroup)
        {
            EndGroupIndex = endGroup;
        }
    }
}