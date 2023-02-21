using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class NarratorDecoration : MonoBehaviour
    {
        public TMP_Text subtitle;


        public void ShowSubtitle(string text)
        {
            subtitle.gameObject.SetActive(true);
            subtitle.text = text;
        }

        public void HideText()
        {
            subtitle.gameObject.SetActive(false);
        }
    }
}