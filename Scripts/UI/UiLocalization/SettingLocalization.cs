using System;
using Assets.SimpleLocalization;
using TMPro;
using UnityEngine;

namespace UI.UiLocalization
{
    public sealed class SettingLocalization : LocalizationView
    {
        public TMP_Text fullscreenText;
        public TMP_Text soundText;
        public TMP_Text musicText;
        public TMP_Text voiceText;
        public TMP_Text backText;

        protected override void ApplyLocalization()
        {
            fullscreenText.text = Localization.Localize("UI.Fullscreen");
            soundText.text = Localization.Localize("UI.S_Sound");
            musicText.text = Localization.Localize("UI.S_Music");
            voiceText.text = Localization.Localize("UI.S_Voice");
            backText.text = Localization.Localize("UI.Bt_Back");
        }
    }
}