using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Gameplay
{
    public class CameraDecoration : MonoBehaviour
    {
        [Header("Camera")] 
        public CinemachineVirtualCamera Camera;
        public float shakeLength = 2f;
        public NoiseSettings normal;
        public float normalAmplitude = 0.2f;
        public float normalFrequency = 0.15f;
        public NoiseSettings shake;
        public float shakeAmplitude = 1.5f;
        public float shakeFrequency = 1f;

        private bool isShaking;
        public void Shake()
        {
            isShaking = true;
            var perlin = Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            StartCoroutine(ShakeCamera(perlin));
        }

        public void StopShake()
        {
            if(!isShaking) return; 
            isShaking = false;
            StopAllCoroutines();
            var perlin = Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            TurnNormalShake(perlin);
        }

        private IEnumerator ShakeCamera(CinemachineBasicMultiChannelPerlin perlin)
        {
            perlin.m_NoiseProfile = shake;
            perlin.m_AmplitudeGain = shakeAmplitude;
            perlin.m_FrequencyGain = shakeFrequency;
            yield return new WaitForSeconds(shakeLength);
            TurnNormalShake(perlin);
        }

        private void TurnNormalShake(CinemachineBasicMultiChannelPerlin perlin)
        {
            perlin.m_NoiseProfile = normal;
            perlin.m_AmplitudeGain = normalAmplitude;
            perlin.m_FrequencyGain = normalFrequency;
        }
    }
}