using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class SceneDecoration : MonoBehaviour
    {
        [Header("Scenes")]
        public GameObject IntroUI;
        public GameObject startPlatform;
        public GameObject[] Scene;
        public GameObject middlePlatform;
        public GameObject[] GameOver;
        public GameObject EpilogueUI;
        public GameObject EpiloguePlatfrom;

        private readonly List<GameObject> activeDecoration = new();

        public UnityEvent OnEpilogue;
        public UnityEvent OnGame;
        public UnityEvent OnGameOver;
        
        public void ShowDecoration(Decorations decorations)
        {
            foreach (var dec in activeDecoration) dec.gameObject.SetActive(false);
            activeDecoration.Clear();
            switch (decorations)
            {
                case Decorations.Epilogue:
                    OnEpilogue?.Invoke();
                    EpiloguePlatfrom.SetActive(true);
                    IntroUI.gameObject.SetActive(true);
                    activeDecoration.Add(IntroUI);
                    activeDecoration.Add(EpiloguePlatfrom);
                    break;
                case Decorations.GameOver:
                    OnGameOver?.Invoke();
                    IntroUI.gameObject.SetActive(true);
                    activeDecoration.Add(IntroUI);
                    break;
                case Decorations.Intro:
                    IntroUI.gameObject.SetActive(true);
                    activeDecoration.Add(IntroUI);
                    break;
                case Decorations.Scene:
                    OnGame?.Invoke();
                    startPlatform.SetActive(true);
                    activeDecoration.Add(startPlatform);
                    foreach (var dec in Scene)
                    {
                        activeDecoration.Add(dec);
                        dec.gameObject.SetActive(true);
                    }       
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(decorations), decorations, null);
            }
        }

        public void TurnOnMiddlePlatform()
        {
            startPlatform.SetActive(false);
            middlePlatform.SetActive(true);
        }
    }
}