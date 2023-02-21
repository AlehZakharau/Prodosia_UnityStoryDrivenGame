using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Core;
using UnityEngine;
using UnityEngine.Networking;

namespace Data
{
    public class Loader2 : MonoBehaviour
    {
        public event Action<GameStateModel> GameModelLoaded;
        public event Action<SettingsModel> SettingsModelLoaded;

        private bool hadLoadedGameModel;
        private bool hadLoadedSettingsModel;
        private Action callback;
        public void LoadData(Action callback)
        {
            this.callback = callback;
            LoadGameData();
            LoadSettingData();
        }
        private void LoadGameData()
        {
#if UNITY_WEBGL
            StartCoroutine(DownloadFileJson(Callback, "GameModel"));
#else
            var model = DataSystem.LoadFileJson<GameStateModel>("GameModel");
            GameModelLoaded?.Invoke(model);
            hadLoadedGameModel = true;
            CheckLoad();
#endif
            #if UNITY_EDITOR && UNITY_WEBGL
                var modelEditor = new GameStateModel();
                GameModelLoaded?.Invoke(modelEditor);
                hadLoadedGameModel = true;
                CheckLoad();
#endif
        }
        private void LoadSettingData()
        {
#if UNITY_WEBGL
            StartCoroutine(DownloadFileJson(Callback2,"SettingsModel"));
#else
            var model = DataSystem.LoadFileJson<SettingsModel>("SettingsModel");
            SettingsModelLoaded?.Invoke(model);
            hadLoadedSettingsModel = true;
            CheckLoad();
#endif

#if UNITY_EDITOR && UNITY_WEBGL
            var modelEditor = new SettingsModel();
            SettingsModelLoaded?.Invoke(modelEditor);
            hadLoadedSettingsModel = true;
            CheckLoad();
#endif
        }
        
        public  IEnumerator DownloadFileJson(Action<string> callback, string filename) 
        {
            var www = new UnityWebRequest(Path.Combine(Application.streamingAssetsPath, filename + ".json"));
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                // Or retrieve results as binary data
                var result = www.downloadHandler.text;
                Debug.Log($"Downloaded {filename}, {result}");
                callback?.Invoke(result);
            }
        } 

        private void Callback(string obj)
        {
            var data = JsonUtility.FromJson<GameStateModel>(obj);
            Debug.Log($"{data.GetType()}, ");
            
            StopCoroutine(DownloadFileJson(Callback, "GameModel"));
            hadLoadedGameModel = true;
            CheckLoad();
            GameModelLoaded?.Invoke(data);
        }
        
        private void Callback2(string obj)
        {
            var data = JsonUtility.FromJson<SettingsModel>(obj);
            Debug.Log($"{data.GetType()}");
            
            StopCoroutine(DownloadFileJson(Callback2, "SettingsModel"));
            hadLoadedSettingsModel = true;
            CheckLoad();
            SettingsModelLoaded?.Invoke(data);
        }
        private void CheckLoad()
        {
            if (hadLoadedGameModel && hadLoadedSettingsModel)
                callback.Invoke();
        }
    }
}