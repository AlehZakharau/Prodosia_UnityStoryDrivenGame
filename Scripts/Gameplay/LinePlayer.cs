using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class LinePlayer : MonoBehaviour
    {
        private Coroutine currentLine;
        public void StartPlayLine(float delay, float eventTime, Action callback, Action eventCallback)
        {
            currentLine = StartCoroutine(PlayLine(delay, eventTime, callback, eventCallback));
        }
        public void StartPlayLine(float delay, Action callback)
        {
            currentLine = StartCoroutine(PlayLine(delay, callback));
        }

        public void StopPlayLine()
        {
            if(currentLine != null)
                StopCoroutine(currentLine);
        }

        private IEnumerator PlayLine(float delay, float eventTime, Action callback, Action eventCallback)
        {
            delay -= eventTime;
            yield return new WaitForSeconds(eventTime);
            eventCallback.Invoke();
            yield return new WaitForSeconds(delay);
            callback.Invoke();
        }
        private IEnumerator PlayLine(float delay, Action callback)
        {
            yield return new WaitForSeconds(delay);
            callback.Invoke();
        }
    }
}