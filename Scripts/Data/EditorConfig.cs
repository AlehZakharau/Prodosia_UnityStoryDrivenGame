using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "EditorConfig", menuName = "Configs/EditorConfig", order = 0)]
    public class EditorConfig : ScriptableObject
    {
        public bool viewLog;
    }
}