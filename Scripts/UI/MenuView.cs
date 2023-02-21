using UnityEngine.UI;

namespace UI
{
    public class MenuView : View
    {
        public Button startGameBt;
        public Button settingsBt;
        public Button creditsBt;
        public Button exitBt;
        public Button helpBt;
        public Button continueBt;

        private void Awake()
        {
#if UNITY_WEBGL
            exitBt.gameObject.SetActive(false);
#endif
        }
        
        public void ShowingContinueBt(bool isActive) => continueBt.gameObject.SetActive(isActive);

    }
}