using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.SimpleLocalization
{
    public class LocalizationDownloader : MonoBehaviour
    {
        private string resultText;
        
        public void LoadLocalizationText(Action<TextAsset> callback)
        {
#if UNITY_WEBGL
            Debug.Log($"Start Downloading");
            StartCoroutine(LoadText(callback));
#else
            callback?.Invoke(LoadLocalizationText());
#endif
            #if UNITY_EDITOR && UNITY_WEBGL
            callback?.Invoke(LoadLocalizationText());
            #endif
        }


        private TextAsset LoadLocalizationText()
        {
            return Resources.Load("Localization", typeof(TextAsset)) as TextAsset;
        }
        

        private IEnumerator LoadText(Action<TextAsset> callback) {
            var www = new UnityWebRequest(Path.Combine(Application.streamingAssetsPath, "Localization.csv"));
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {

                // Or retrieve results as binary data
                resultText = www.downloadHandler.text;
                callback?.Invoke(new TextAsset(resultText));
                Debug.Log($"Downloaded Text");
            }
        }
    }
}