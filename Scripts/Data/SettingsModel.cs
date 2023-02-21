using System;
using Assets.SimpleLocalization;
using UnityEditor;

namespace Data
{
    [Serializable]
    public class SettingsModel
    {
        public int currentLanguage;
        public float soundVolume = 1;
        public float musicVolume = 1;
        public float voiceVolume = 1;
        public bool fullScreenMode;
        public int resolution;
    }
}