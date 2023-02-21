using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Gameplay
{
    public class WinEffectDecoration : MonoBehaviour
    {
        public Animator motherIdle;
        public Animator[] decorationAnims;
        public SpriteRenderer[] decorationMother;
        public Animator spotLightAnim;
        public Animator pointLightAnim;
        private static readonly int Active = Animator.StringToHash("Active");

        private void Awake()
        {
            foreach (var decorationAnim in decorationAnims)
            {
                decorationAnim.enabled = false;
            }
        }


        public void StartWinEffect(Action callback)
        {
            StartCoroutine(WinEffect(callback));
            TurnOffLight();
        }

        public void Default()
        {
            foreach (var decorationAnim in decorationMother)
            {
                decorationAnim.color = new Color(164,164,164);
            }
        }

        private void TurnOffLight()
        {
            foreach (var decorationAnim in decorationAnims)
            {
                decorationAnim.enabled = true;
                decorationAnim.SetBool(Active, false);
            }
        }

        private IEnumerator WinEffect(Action callback)
        {
            var time = 0f;
            var pointTime = 0f;
            var turnOnTime = 0f;
            spotLightAnim.gameObject.SetActive(false);
            while (time < 16f)
            {
                time += Time.deltaTime;
                pointTime += Time.deltaTime;
                turnOnTime += Time.deltaTime;

                if (pointTime >= 3.5f)
                {
                    pointLightAnim.gameObject.SetActive(true);
                    pointLightAnim.enabled = true;
                }

                if (turnOnTime >= 13.9f)
                {
                    spotLightAnim.gameObject.SetActive(true);
                    spotLightAnim.enabled = true;
                    pointLightAnim.enabled = false;
                    pointLightAnim.gameObject.SetActive(false);
                    motherIdle.gameObject.SetActive(true);
                    motherIdle.enabled = true;
                    foreach (var decorationAnim in decorationAnims)
                    {
                        decorationAnim.gameObject.SetActive(false);
                    }
                }
                yield return null;
            }
            spotLightAnim.enabled = false;
            callback.Invoke();
        }
    }
}