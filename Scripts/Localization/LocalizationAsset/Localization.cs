using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.SimpleLocalization
{
    public static class Localization
    {
        public static event Action LocalizationChanged;
        public static readonly Dictionary<ELanguage, Dictionary<string, string>> Dictionary = new();
        public static ELanguage Language;
        public static LocalizationUtility LocalizationUtility = new();
        
        public static void ChangeLanguage(ELanguage language)
        {
            Language = language;
            LocalizationChanged?.Invoke();
        }
        
        public static string Localize(string localizationKey)
        {
#if UNITY_EDITOR
            if (!CheckLocalizationText(localizationKey)) return null;
#endif
            return Dictionary[Language][localizationKey];
        }
        
        public static bool HasKey(string localizationKey)
        {
            return Dictionary[Language].ContainsKey(localizationKey);
        }

        private static bool CheckLocalizationText(string localizationKey)
        {
            if (Dictionary.Count == 0)
            {
                Debug.LogWarningFormat($"Dictionary null");
                return false;
            }

            if (!Dictionary[Language].ContainsKey(localizationKey))
            {
                Debug.Log($"Key has been not found");
                return false;
            }

            var missed = !Dictionary[Language].ContainsKey(localizationKey) 
                         || string.IsNullOrEmpty(Dictionary[Language][localizationKey]);

            if (missed)
            {
                Debug.LogWarningFormat("Translation not found: {localizationKey} ({0}).", Language);

                return false;
            }
            return true;
        }
        public static void FillDictionary(TextAsset textAsset)
        {
            var lines = LocalizationUtility.FindLocalizationMarkerMistake(textAsset);
            var languages = lines[0].Split(',').Select(i => i.Trim()).ToList();

            for (var i = 1; i < languages.Count; i++)
            {
                var result = Enum.TryParse<ELanguage>(languages[i], out var language);
                if(!result) continue;
                if (!Dictionary.ContainsKey(language))
                {
                    Dictionary.Add(language, new Dictionary<string, string>());
                }
            }

            for (var i = 1; i < lines.Length; i++)
            {
                var columns = lines[i].Split(',')
                    .Select(j => j.Trim())
                    .Select(j => j
                        .Replace("[comma]", ",")
                        .Replace("[newline]", "\n")
                        .Replace("[quotes]", "\""))
                    .ToList();
                var key = columns[0];

                if (key == "") continue;

                for (var j = 1; j < languages.Count; j++)
                {
                    var language = Enum.Parse<ELanguage>(languages[j]);
                    Dictionary[language].Add(key, columns[j]);
                }
            }
            LocalizationChanged?.Invoke();
        }
    }
}