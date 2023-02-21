using Assets.SimpleLocalization;
using Data;
using ReactiveSystem;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class SubtitlesPlayer : MonoBehaviour
    {
        public TMP_Text efficientText;
        public TMP_Text mentorText;
        public TMP_Text pacificText;
        public TMP_Text tantrumText;
        public TMP_Text warriorText;

        private TMP_Text active;
        private void Start()
        {
            MonoEventBus.Subscribe<StartLineSignal>(StartLine);
            MonoEventBus.Subscribe<StartCommentSignal>(StartComment);
            MonoEventBus.Subscribe<EndLineSignal>(EndLine);

            efficientText.gameObject.SetActive(false);
            mentorText.gameObject.SetActive(false);
            pacificText.gameObject.SetActive(false);
            tantrumText.gameObject.SetActive(false);
            warriorText.gameObject.SetActive(false);
        }

        private void EndLine(EndLineSignal obj)
        {
            if(active == null) return;
            active.gameObject.SetActive(false);
        }

        private void StartComment(StartCommentSignal obj) => ShowText(obj.Character, obj.Id);

        private void StartLine(StartLineSignal obj) => ShowText(obj.Character, obj.Id);

        private void ShowText(Character character, string id)
        {
            var text = GetTextObject(character);
            if(text == null) return;
            text.text = Localization.Localize(id);
            text.gameObject.SetActive(true);
            active = text;
        }

        private TMP_Text GetTextObject(Character character)
        {
            return active = character switch
            {
                Character.None => null,
                Character.Narrator => null,
                Character.Efficient => efficientText,
                Character.Mentor => mentorText,
                Character.Pacific => pacificText,
                Character.Tantrum => tantrumText,
                Character.Warrior => warriorText,
                _ => active
            };
        }
    }
}