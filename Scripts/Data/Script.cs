using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Data
{
    [CreateAssetMenu(fileName = "Script", order = 0)]
    public class Script : ScriptableObject
    {
        public Stage[] Stages;
        public Comments[] Comments;
        public Answers[] Answers;
        public Messages[] Messages;

        public void DefaultEvents()
        {
            foreach (var stage in Stages)
            {
                foreach (var line in stage.LinesData)
                {
                    if (line.Event != null) line.Event.HasExecuted = false;
                }
            }
        }

        public Stage GetStage(Stages name)
        {
            foreach (var stage in Stages)
            {
                if (stage.name == name)
                {
                    return stage;
                }
            }
            throw new Exception($"There is no stage: {name}");
        }

        public AnswerLine[] GetAnswer(Message type)
        {
            foreach (var answer in Answers)
            {
                if (answer.Message == type)
                {
                    var clone = (AnswerLine[]) answer.AnswerLines.Clone(); 
                    clone.Shuffle();
                    return clone;
                }
            }
            throw new Exception($"There is no message: {type}");
        }

        public Sprite GetSymbol(Message type)
        {
            foreach (var answer in Answers)
            {
                if (answer.Message == type)
                {
                    return answer.Symbol;
                }
            }
            throw new Exception($"There is no message: {type}");
        }

        public CommentLine GetComment(List<Character> characters, Answer answer)
        {
            var index = Random.Range(0, Comments.Length);
            var comment = Comments[index];
            if (comment.Character == characters[0] || comment.Character == characters[1])
                return GetComment(characters, answer);

            switch (answer)
            {
                case Answer.Wrong:
                    return comment.CommentLines[2];
                case Answer.Right:
                    var commentIndex = Random.Range(0, 2);
                    return comment.CommentLines[commentIndex];
                case Answer.CriticalWrong:
                    return comment.CommentLines[3];
                default:
                    throw new ArgumentOutOfRangeException(nameof(answer), answer, null);
            }
        }

        public void DisableCharacter(Character character, bool value)
        {
            foreach (var comment in Comments)
            {
                if (comment.Character == character)
                {
                    comment.disable = value;
                }
            }
        }

        public Message[] GetMessages(int group)
        {
            if(Messages.Length <= group)
                throw new Exception($"There is no message group: {group}");
            var messages = (Messages)Messages[group].Clone();
            messages.MessageGroups.Shuffle();
            return messages.MessageGroups;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            foreach (var stage in Stages)
            {
                stage.inspectorName = stage.name.ToString();
                for (var index = 0; index < stage.LinesData.Length; index++)
                {
                    var lineData = stage.LinesData[index];
                    lineData.id = stage.name + $"._{index + 1}";
                    
                }
            }

            foreach (var comment in Comments)
            {
                for (int i = 0; i < comment.CommentLines.Length; i++)
                {
                    var commentData = comment.CommentLines[i];
                    commentData.id = comment.name + $"._{i + 1}";
                    commentData.Character = comment.Character;
                }
            }

            foreach (var answer in Answers)
            {
                answer.name = answer.Message.ToString();
                for (int i = 0; i < answer.AnswerLines.Length; i++)
                {
                    var answerData = answer.AnswerLines[i];
                    answerData.id = "Message." + answer.name + $"._{i + 1}";
                }
            }
        }
#endif
    }

    [Serializable]
    public class Stage
    {
        [HideInInspector]public string inspectorName;
        public Stages name;
        public LineData[] LinesData;
    }

    [Serializable]
    public class Comments
    {
        public string name;
        public bool disable;
        public Character Character;
        public CommentLine[] CommentLines;
    }

    [Serializable]
    public class Answers
    {
        [HideInInspector]public string name;
        public Message Message;
        public Sprite Symbol;
        public AnswerLine[] AnswerLines;
    }

    [Serializable]
    public class Messages : ICloneable
    {
        public Message[] MessageGroups;
        public object Clone() => MemberwiseClone();
    }

}