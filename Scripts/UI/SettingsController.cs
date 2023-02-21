using Assets.SimpleLocalization;
using Data;
using UI.UiLocalization;
using UnityEngine;
using UnityEngine.Audio;

namespace UI
{
    public class SettingsController
    {
        private readonly SettingsView settingsView;
        private readonly AudioMixer mixer;
        private readonly SettingsModel settingsModel;
        private readonly IWindowsMediator windowsMediator;

        public SettingsController(SettingsView settingsView, AudioMixer mixer, SettingsModel settingsModel,
            IWindowsMediator windowsMediator, LocalizationView[] localizationViews)
        {
            this.settingsView = settingsView;
            this.mixer = mixer;
            this.settingsModel = settingsModel;
            this.windowsMediator = windowsMediator;

            settingsView.engBt.onClick.AddListener(TurnEnglish);
            settingsView.rusBt.onClick.AddListener(TurnRussian);
            settingsView.belBt.onClick.AddListener(TurnBelarusian);
            this.settingsView.resolution.onValueChanged.AddListener(ChangeResolution);
            this.settingsView.backBt.onClick.AddListener(Back);
            this.settingsView.soundSl.onValueChanged.AddListener(ChangeSoundVolume);
            this.settingsView.musicSl.onValueChanged.AddListener(ChangeMusicVolume);
            this.settingsView.voiceSl.onValueChanged.AddListener(ChangeVoiceVolume);
            
            this.settingsView.fullscreenBt.onClick.AddListener(ScreenMode);

            foreach (var localizationView in localizationViews) localizationView.Initialize();
            ApplySavedSettings(settingsModel);
        }

        private void ChangeResolution(int arg0)
        {
            settingsModel.resolution = arg0;
            switch (arg0)
            {
                case 0:
                    Screen.SetResolution(3840, 2160, !settingsModel.fullScreenMode);
                    break;
                case 1:
                    Screen.SetResolution(2560, 1440, !settingsModel.fullScreenMode);
                    break;
                case 2:
                    Screen.SetResolution(1920, 1080, !settingsModel.fullScreenMode);
                    break;
                case 3:
                    Screen.SetResolution(1600, 900, !settingsModel.fullScreenMode);
                    break;
                case 4:
                    Screen.SetResolution(1080, 720, !settingsModel.fullScreenMode);
                    break;
            }
        }

        private void ApplySavedSettings(SettingsModel model)
        {
            Localization.ChangeLanguage((ELanguage)model.currentLanguage);
            Screen.fullScreen = model.fullScreenMode;
            ChangeResolution(model.resolution);

            settingsView.voiceSl.value = model.voiceVolume;
            mixer.SetFloat("VoiceVolume", Mathf.Log10(settingsView.voiceSl.value) * 20);
            settingsView.soundSl.value = model.soundVolume;
            mixer.SetFloat("SoundVolume", Mathf.Log10(settingsView.soundSl.value) * 20);
            settingsView.musicSl.value = model.musicVolume;
            mixer.SetFloat("MusicVolume", Mathf.Log10(settingsView.musicSl.value) * 20);
            
            #if UNITY_WEBGL
            settingsView.voiceSl.value = 1;
            mixer.SetFloat("VoiceVolume", 0);
            settingsView.soundSl.value = 1;
            mixer.SetFloat("SoundVolume", 0);
            settingsView.musicSl.value = 1;
            mixer.SetFloat("MusicVolume", 0);
            #endif
        }

        private void ChangeSoundVolume(float value)
        {
            var volume = Mathf.Log10(value) * 20;
            mixer.SetFloat("SoundVolume", volume);
            settingsModel.soundVolume = value;
        }

        private void ChangeMusicVolume(float value)
        {
            var volume = Mathf.Log10(value) * 20;
            mixer.SetFloat("MusicVolume", volume);
            settingsModel.musicVolume = value;
        }

        private void ChangeVoiceVolume(float value)
        {
            var volume = Mathf.Log10(value) * 20;
            mixer.SetFloat("VoiceVolume", volume);
            settingsModel.voiceVolume = value;
        }


        private void ScreenMode()
        {
            Screen.fullScreen = !Screen.fullScreen;
            settingsModel.fullScreenMode = Screen.fullScreen;
        }

        private void TurnEnglish()
        {
            Localization.ChangeLanguage(ELanguage.English);
            settingsModel.currentLanguage = (int)ELanguage.English;
        }

        private void TurnRussian()
        {
            Localization.ChangeLanguage(ELanguage.Russian);
            settingsModel.currentLanguage = (int)ELanguage.Russian;
        }

        private void TurnBelarusian()
        {
            Localization.ChangeLanguage(ELanguage.Belarusian);
            settingsModel.currentLanguage = (int)ELanguage.Belarusian;
        }

        private void Back()
        {
            DataSystem.SaveFileJson("SettingsModel", settingsModel);
            windowsMediator.OpenPrevious();
        }
    }
}