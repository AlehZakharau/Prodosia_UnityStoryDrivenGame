using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "Configs/CameraConfig", order = 0)]
    public class CameraConfig : ScriptableObject
    {
        [Header("Speed")]
        public float CameraSpeedX;
        public float CameraSpeedY;
        public float CameraSpeedZ;

        [Header("Boarder")]
        public float forwardFace;
        public float backFace;
        public float rightFace;
        public float leftFace;
        public float upFace;
        public float downFace;
    }
}