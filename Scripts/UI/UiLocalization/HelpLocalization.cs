using Assets.SimpleLocalization;
using TMPro;
using UnityEngine.Serialization;

namespace UI.UiLocalization
{
    public sealed class HelpLocalization : LocalizationView
    {
        public TMP_Text control;
        public TMP_Text zoom;
        public TMP_Text skip;
        public TMP_Text backBt; 
        public TMP_Text pause;


        protected override void ApplyLocalization()
        {
            control.text = Localization.Localize("UI.HelpControl");
            zoom.text = Localization.Localize("UI.HelpZoom");
            skip.text = Localization.Localize("UI.HelpSkip");
            backBt.text = Localization.Localize("UI.Bt_Back");
            pause.text = Localization.Localize("UI.HelpPause");
        }
    }
}