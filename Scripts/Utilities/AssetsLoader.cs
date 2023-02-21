using Data;
using UnityEditor;

namespace Utilities
{
    #if UNITY_EDITOR
    public static class AssetsLoader
    {
        public static EditorConfig LoadConfig()
        {
            return AssetDatabase.LoadAssetAtPath<EditorConfig>("Assets/Data/Configs/EditorConfig.asset");
        }
    }
    #endif
}