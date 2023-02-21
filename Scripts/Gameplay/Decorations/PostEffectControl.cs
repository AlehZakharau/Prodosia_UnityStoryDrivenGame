using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Gameplay
{
    public class PostEffectControl : MonoBehaviour
    {
        public PostProcessVolume PostProcessVolume;

        private void Awake()
        {
#if UNITY_WEBGL
            PostProcessVolume.enabled = false;
#else
            PostProcessVolume.enabled = true;
#endif
            
        }
    }
}