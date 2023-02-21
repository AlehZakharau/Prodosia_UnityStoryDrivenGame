using System;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Text", order = 1)]
    public class LinesText : ScriptableObject
    {
        public LineText[] LineTexts;
    }


    [Serializable]
    public class LineText
    {
        public string Id;
        public string English;
        public string Russian;
        public string Belarusian;
    }
}