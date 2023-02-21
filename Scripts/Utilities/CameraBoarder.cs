using Data;
using UnityEngine;

namespace Core.Editor
{
    [ExecuteInEditMode]
    public class CameraBoarder : MonoBehaviour
    {
        [SerializeField] private CameraConfig config;
        private void OnDrawGizmos()
        {
            var width = config.rightFace - config.leftFace;
            var height = config.upFace - config.downFace;
            var length = config.forwardFace - config.backFace;


            var center = new Vector3(config.rightFace - width / 2, config.upFace - height / 2, config.forwardFace -  length / 2);
            var size = new Vector3(width, height, length); 
            
            Gizmos.color = new Color(0.83f, 0.59f, 0.43f);
            Gizmos.DrawWireCube(center, size);
        }
    }
}