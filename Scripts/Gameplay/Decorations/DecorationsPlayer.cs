using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay
{
    public class DecorationsPlayer : MonoBehaviour
    {
        public SceneDecoration sceneDecoration;
        public EffectDecoration effectDecoration;
        public WinEffectDecoration winEffectDecoration; 
        public MotherDecoration motherDecoration;
        public CameraDecoration cameraDecoration;
        public NarratorDecoration introEpilogueNarratorDecoration;
        public NarratorDecoration gameOverNarratorDecoration;
        public AudioPlayer audioPlayer;
    }
}