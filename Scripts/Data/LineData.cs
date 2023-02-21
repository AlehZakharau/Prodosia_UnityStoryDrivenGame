using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Data
{
    [Serializable]
    public class LineData
    {
        public string id;
        public Character Character;
        public AudioClip Clip;
        public int Stage;
        public float DelayAfter;
        public float EventTime;
        public Event Event;
    }

    [Serializable]
    public class CommentLine
    {
        public string id;
        public Character Character;
        public AudioClip Clip;
        public float DelayAfter;
    }

    [Serializable]
    public class AnswerLine : ICloneable
    {
        public string id;
        public Answer Answer;
        public object Clone() => MemberwiseClone();
    }
}