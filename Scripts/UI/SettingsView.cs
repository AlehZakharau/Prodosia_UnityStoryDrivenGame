using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsView : View
    {

        [Header("Localisation Settings")]
        public Button engBt;
        public Button rusBt;
        public Button belBt;

        [Header("Sound Settings")]
        public Button soundBt;

        public Slider soundSl;
        public Slider musicSl;
        public Slider voiceSl;

        [Header("Video Settings")]
        public Button fullscreenBt;
        public Button backBt;
        public TMP_Dropdown resolution;


        private void Start()
        {
            #if UNITY_WEBGL
            resolution.gameObject.SetActive(false);
#endif
        }
    }
}