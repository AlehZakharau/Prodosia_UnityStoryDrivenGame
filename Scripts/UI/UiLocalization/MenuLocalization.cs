using System;
using Assets.SimpleLocalization;
using TMPro;
using UnityEngine;

namespace UI.UiLocalization
{
    public sealed class MenuLocalization : LocalizationView
    {
        public TMP_Text newGameText;
        public TMP_Text continueText;
        public TMP_Text settingsText;
        public TMP_Text creditsText;
        public TMP_Text exitText;
        public TMP_Text helpText;



        protected override void ApplyLocalization()
        {
            newGameText.text = Localization.Localize("UI.Bt_Start");
            continueText.text = Localization.Localize("UI.Bt_Continue");
            settingsText.text = Localization.Localize("UI.Bt_Settings");
            creditsText.text = Localization.Localize("UI.Bt_Credits");
            exitText.text = Localization.Localize("UI.Bt_Exit");
            helpText.text = Localization.Localize("UI.Bt_Help");
        }
    }
}