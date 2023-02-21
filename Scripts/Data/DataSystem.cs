using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Data
{
    public static class DataSystem
    {
        public static T LoadFileJson<T>(string filename) where T : class, new()
        {
            var filePath = Path.Combine(Application.persistentDataPath, filename + ".json");
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                return JsonUtility.FromJson<T>(jsonData);
            }
            var data = new T();
            SaveFileJson<T>(filename, data);
            return data;
        }

        public static T ReadData<T>(string data) where T : class, new()
        {
            return JsonUtility.FromJson<T>(data);
        }
        
        public static IEnumerator DownloadFileJson(Action<string> callback, string filename) 
        {
            Debug.Log($"Start Downloading {filename}");
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
                yield break;
            }
        } 
        public static IEnumerator UploadFileJson<T>(string filename, T data) where T : class
        {
            Debug.Log($"Start Uploading");
            var json = JsonUtility.ToJson(data);
            var path = Path.Combine(Application.streamingAssetsPath, filename + ".json");
            var www = UnityWebRequest.Put(path, json);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log($"Upload {filename}");
            }
        }
        public static void SaveFileJson<T>(string filename, T data) where T : class
        {
            var filePath = Path.Combine(Application.persistentDataPath, filename + ".json"); 
            
            var jsonData = JsonUtility.ToJson(data);
            
            File.WriteAllText(filePath, jsonData);
        }

        public static void SaveNewFileJson<T>(string filename, T data) where T : class
        {
            var filePath = Path.Combine("Assets/StreamingAssets", filename + ".json");

            var jsonData = JsonUtility.ToJson(data);
            
            File.WriteAllText(filePath, jsonData);
        }
    }
}