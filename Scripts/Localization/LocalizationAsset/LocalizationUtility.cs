using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.SimpleLocalization
{
    public class LocalizationUtility
    {
        public string[] FindLocalizationMarkerMistake(TextAsset textAsset)
        {
            var text = textAsset.text;
            text = ReplaceLineMarkers(text);
            text = ReplaceQuoteMarkers(text);
            
            var matches = Regex.Matches(text, "\"[\\s\\S]+?\"");
            
            foreach (Match match in matches)
            {
                text = text
                    .Replace(match.Value, match.Value
                    .Replace("\"", null)
                    .Replace(",", "[comma]")
                    .Replace("\n", "[newline]"));
            }

            return text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }
        
        private string ReplaceLineMarkers(string text)
        {
            return text.Replace("[Newline]", "\n");
        }

        private string ReplaceQuoteMarkers(string text)
        {
            return text.Replace("\"\"", "[quotes]");
        }
    }
}