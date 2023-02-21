using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.SimpleLocalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CreditsView : View
    {
        public Button backBt;
        public TMP_Text text;

        public float spawnTime = 9f;
        public float pauseTime = 9f;
        public float speed = 10f;
        public int[] intervals;

        private bool isDone;
        private List<TMP_Text> texts = new();

        private void OnEnable()
        {
            StartCoroutine(ShowCredits());
        }

        private void OnDisable()
        {
            StopCoroutine(ShowCredits());
            foreach (var tmpText in texts) Destroy(tmpText.gameObject);
            texts.Clear();
        }


        private IEnumerator ShowCredits()
        {
            float timer = spawnTime; ;
            int creditsIndex = 1;
            while (true)
            {
                timer += Time.deltaTime;
                if (timer >= spawnTime && creditsIndex <= 17 && !CheckInterval(creditsIndex) ||
                    timer >= pauseTime && CheckInterval(creditsIndex))
                {
                    var t = Instantiate(text, transform);
                    t.text = Localization.Localize($"UI.Credits{creditsIndex++}");
                    texts.Add(t);
                    timer = 0;
                }

                foreach (var tmpText in texts)
                {
                    tmpText.rectTransform.localPosition += Vector3.up * speed * Time.deltaTime;
                }
                yield return null;
            }
        }

        private bool CheckInterval(int index)
        {
            return intervals.Any(interval => index == interval);
        }
    }
}