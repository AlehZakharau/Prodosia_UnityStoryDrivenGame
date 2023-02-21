using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class EffectDecoration : MonoBehaviour
    {
        [Header("Effects")] 
        public Material particleMaterial;
        public Color wrongColor;
        public Color rightColor;
        public ParticleSystem symbols;
        public ParticleSystem[] staticSymbols;
        public Animator spotLightAnim;
        public Animator pointLightAnim;
        

        private int symbolIndex;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");


        private void Awake()
        {
            spotLightAnim.enabled = false;
            pointLightAnim.enabled = false;
            pointLightAnim.gameObject.SetActive(false);
        }

        public void ShowSymbol(Sprite sprite)
        {
            //foreach (var symbol in staticSymbols) symbol.gameObject.SetActive(false);
            symbols.gameObject.SetActive(true);
            var sheet = symbols.textureSheetAnimation;
            sheet.SetSprite(0 ,sprite);
            symbols.Play();
        }

        public void HideSymbol()
        {
            //foreach (var symbol in staticSymbols) symbol.gameObject.SetActive(true);
            symbols.gameObject.SetActive(false);
            symbols.Stop();
        }

        public void AnswerEffect(bool isRight)
        {
            StartCoroutine(isRight ? ShowAnswerEffect(rightColor, true) : ShowAnswerEffect(wrongColor, false));
        }
        
        private IEnumerator ShowAnswerEffect(Color color, bool isRight)
        {
            var timer = 0f;
            while (timer < 2f)
            {
                timer += Time.deltaTime;
                if (!isRight)
                {
                    spotLightAnim.enabled = true;
                    pointLightAnim.gameObject.SetActive(true);
                    pointLightAnim.enabled = true;
                }
                particleMaterial.color = Color.Lerp(Color.white, color, timer);
                particleMaterial.SetColor(EmissionColor, Color.Lerp(Color.white, color, timer));
                yield return null;
            }
            while (particleMaterial.color != Color.white)
            {
                spotLightAnim.enabled = false;
                pointLightAnim.gameObject.SetActive(false);
                pointLightAnim.enabled = false;
                particleMaterial.color = Color.Lerp(color, Color.white, timer);
                particleMaterial.SetColor(EmissionColor, Color.Lerp(color, Color.white, timer));
                yield return null;
            }
        }
    }
}