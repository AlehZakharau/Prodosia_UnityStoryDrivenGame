using Assets.SimpleLocalization;
using UnityEngine;

namespace UI.UiLocalization
{
    public class LocalizationView : MonoBehaviour
    {
        public virtual void Initialize()
        {
            Localization.LocalizationChanged += ApplyLocalization;
        }

        protected virtual void ApplyLocalization()
        {
            
        }
    }
}