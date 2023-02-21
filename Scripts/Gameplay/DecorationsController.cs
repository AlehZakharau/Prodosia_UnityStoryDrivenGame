using System;
using Assets.SimpleLocalization;
using Core;
using Data;
using Data.Events;
using ReactiveSystem;
using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class DecorationsController
    {
        private readonly DecorationsPlayer decPlayer;
        private readonly IEventBus eventBus;
        private readonly GameStateModel model;

        public DecorationsController(DecorationsPlayer decPlayer, IEventBus eventBus, GameStateModel model)
        {
            this.decPlayer = decPlayer;
            this.eventBus = eventBus;
            this.model = model;

            eventBus.Subscribe<ShowWinEffectSignal>(ShowWinEffect);
        }

        public void ShowWinEffect(ShowWinEffectSignal obj)
        {
            decPlayer.winEffectDecoration.StartWinEffect(OnEffectEnd);
            model.winEffectChoose = true;
        }

        private void OnEffectEnd()
        {
            model.winEffectChoose = false;
        }

        public void ShowDecoration(Decorations decoration)
        {
            decPlayer.sceneDecoration.ShowDecoration(decoration);
            Debug.Log($"Show Decoration: <color={C.Additional}>{decoration}</color>");
            ShowNarratorSubtitle(decoration);
        }

        public void ChangePlatform()
        {
            decPlayer.sceneDecoration.TurnOnMiddlePlatform();
        }

        public void ChangeMotherStatus(int life)
        {
            decPlayer.motherDecoration.ChangeMotherStatus(life);
        }

        public void ShowNewMotherPose(int stage, int life)
        {
            decPlayer.motherDecoration.ShowNewPose(stage, life);
        }

        public void ShowSymbol(Sprite sprite)
        {
            decPlayer.effectDecoration.ShowSymbol(sprite);
        }

        public void HideSymbol()
        {
            decPlayer.effectDecoration.HideSymbol();
            decPlayer.winEffectDecoration.Default();
        }

        public void MotherIdle(bool value)
        {
            decPlayer.motherDecoration.MotherIdle(value);
        }

        public void AnswerEffect(bool isRight)
        {
            decPlayer.effectDecoration.AnswerEffect(isRight);
            if(!isRight)
                ShakeCamera();
        }

        public void ShakeCamera()
        {
            decPlayer.cameraDecoration.Shake();
        }

        public void StopShake()
        {
            decPlayer.cameraDecoration.StopShake();
        }

        public void PlayEpilogueMusic()
        {
            decPlayer.audioPlayer.music.StopMusic();
            decPlayer.audioPlayer.epilogueMusic.Play();
        }
        public void PlayGameOverMusic()
        {
            decPlayer.audioPlayer.music.StopMusic();
            decPlayer.audioPlayer.gameOverMusic.Play();
        }

        public void PlayDangerEnvironment()
        {
            decPlayer.audioPlayer.calmEnvironment.StopMusic();
            decPlayer.audioPlayer.dangerEnvironment.PlayRandom();
        }

        public void PlayAggressiveMother()
        {
            decPlayer.audioPlayer.calmMother.StopMusic();
            decPlayer.audioPlayer.aggressiveMother.PlayRandom();
        }

        public void PlayGameMusic()
        {
            decPlayer.audioPlayer.music.PlayByIndex(0);
            decPlayer.audioPlayer.epilogueMusic.Stop();
            decPlayer.audioPlayer.gameOverMusic.Stop();
            decPlayer.audioPlayer.interSound.Play();
            decPlayer.audioPlayer.calmEnvironment.PlayRandom();
            decPlayer.audioPlayer.calmMother.PlayRandom();
            decPlayer.audioPlayer.dangerEnvironment.StopMusic();
            decPlayer.audioPlayer.aggressiveMother.StopMusic();
        }

        private void ShowNarratorSubtitle(Decorations stage)
        {
            switch (stage)
            {
                case Decorations.Intro:
                    decPlayer.introEpilogueNarratorDecoration.ShowSubtitle(Localization.Localize("Intro._1"));
                    break;
                case Decorations.Epilogue:
                    decPlayer.introEpilogueNarratorDecoration.ShowSubtitle(Localization.Localize("Epilogue._1"));
                    break;
                case Decorations.GameOver:
                    decPlayer.introEpilogueNarratorDecoration.ShowSubtitle(Localization.Localize("GameOver._1"));
                    break;
                case Decorations.Scene:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stage), stage, null);
            }
        }

        public void StopWinEffect()
        {
            decPlayer.winEffectDecoration.StopAllCoroutines();
        }
    }
}