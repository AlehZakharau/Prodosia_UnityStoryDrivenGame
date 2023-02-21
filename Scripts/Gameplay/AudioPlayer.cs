using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Gameplay
{
    public interface IAudioPlayer
    {
        public AudioSource VoicePlayer { get; set; }
    }
    public class AudioPlayer : MonoBehaviour, IAudioPlayer
    {
        public AudioMixer mixer;
        [field: SerializeField] public AudioSource VoicePlayer { get; set; }
        public AudioSource epilogueMusic;
        public AudioSource gameOverMusic;
        public AudioSource interSound;
        public SoundList music;
        public SoundList calmEnvironment;
        public SoundList dangerEnvironment;
        public SoundList calmMother;
        public SoundList aggressiveMother;
    }
}