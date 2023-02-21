using System;
using UnityEngine;

namespace Gameplay
{
    public class MotherDecoration : MonoBehaviour
    {
        [Header("Mother")]
        public MotherSprites[] motherSpritesArray;
        public GameObject motherIdle;
        
        private GameObject activeMotherPose;
        private int currentMotherStage = -1;
        private void Awake()
        {
            activeMotherPose = motherIdle;
        }
        public void ShowNewPose(int stage, int life)
        {
            var pose = ConvertLife(life);
            activeMotherPose.gameObject.SetActive(false);
            currentMotherStage = stage;
            activeMotherPose = motherSpritesArray[stage].mother[pose];
            activeMotherPose.gameObject.SetActive(true);
        }
        public void ChangeMotherStatus(int life)
        {
            var pose = ConvertLife(life);
            if(currentMotherStage == -1) return;
            activeMotherPose.gameObject.SetActive(false);
            activeMotherPose = motherSpritesArray[currentMotherStage].mother[pose];
            activeMotherPose.gameObject.SetActive(true);
        }
        public void MotherIdle(bool value)
        {
            activeMotherPose.gameObject.SetActive(false);
            motherIdle.gameObject.SetActive(value);
        }

        private int ConvertLife(int life)
        {
            if (life < 4) return 2;
            return life == 4 ? 1 : 0;
        }
    }
}

[Serializable]
public class MotherSprites
{
    public GameObject[] mother;
}