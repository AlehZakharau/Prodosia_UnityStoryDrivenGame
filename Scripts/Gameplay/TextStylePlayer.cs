using System;
using Assets.SimpleLocalization;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class TextStylePlayer : MonoBehaviour
    {
        public float englishFontSizeCof;
        public float russianFontSizeCof;
        public float belarusianFontSizeCof;
        public TMP_Text[] texts;
        public TMP_FontAsset englishAsset;
        public TMP_FontAsset belarusianAsset;
        public TMP_FontAsset russianAsset;


        private void Awake()
        {
            Localization.LocalizationChanged += OnLanguageChange;
        }

        private void OnLanguageChange()
        {
            foreach (var text in texts)
            {
                switch (Localization.Language)
                {
                    case ELanguage.English:
                        text.font = englishAsset;
                        text.fontSize *= englishFontSizeCof;
                        break;
                    case ELanguage.Belarusian:
                        text.font = belarusianAsset;
                        text.fontSize *= belarusianFontSizeCof;
                        break;
                    case ELanguage.Russian:
                        text.font = russianAsset;
                        text.fontSize *= russianFontSizeCof;
                        break;
                    default:
                        text.font = text.font;
                        break;
                }
            }
        }
    }
}