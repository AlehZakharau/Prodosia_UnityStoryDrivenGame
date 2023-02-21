using System;
using Core;
using Data;
using Data.Events;
using ReactiveSystem;
using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class LineScriptController
    {
        private readonly Script script;
        private readonly IEventBus eventBus;
        private readonly GameStateModel model;
        private readonly AudioSource voicePlayer;
        private readonly LinePlayer linePlayer;

        private LineData[] linesData;
        private int currentClip;
        private Stages currentStage;
        public bool IsSkippable { get; set; }
        public bool IsSkippableComment { get; set; }

        public int ScriptLength => script.Stages.Length;

        public LineScriptController(Script script, IAudioPlayer audioPlayer,
            IEventBus eventBus, GameStateModel model)
        {
            this.script = script;
            this.eventBus = eventBus;
            this.model = model;
            voicePlayer = audioPlayer.VoicePlayer;

            linePlayer = CreateLinePLayer();
            eventBus.Subscribe<ShowWinEffectSignal>(WinEffect);
            
        }

        private void WinEffect(ShowWinEffectSignal obj) => IsSkippable = false;

        public void PlayStage(Stages stageName)
        {
            linesData = GetLineData(stageName);
            DebugStage(stageName);
            
            currentClip = 0;
            model.line = currentClip;
            PlayLine(linesData[0]);
        }

        public void PlayStage(Stages stageName, int clip)
        {
            linesData = GetLineData(stageName);
            DebugStage(stageName);
            for (int i = 0; i < clip; i++)
            {
                if(linesData[i].Event != null)
                    linesData[i].Event.DoAction(true);
            }
            if(!model.firstMessageChoose)
                PlayLine(linesData[clip]);
        }
        
        

        public void Skip()
        {
            PlayNext();
        }

        public void StopPlayLine()
        {
            DoAction();
            linePlayer.StopPlayLine();
            voicePlayer.Stop();
            IsSkippable = false;
            IsSkippableComment = false;
        }

        public void PlayLine(CommentLine commentData, Action afterComment)
        {
            eventBus.Fire(new StartCommentSignal(commentData.Character, commentData.id));
            IsSkippableComment = true;
            voicePlayer.clip = commentData.Clip;
            voicePlayer.Play();
            linePlayer.StartPlayLine(commentData.Clip.length + commentData.DelayAfter,  afterComment);
        }

        private LineData[] GetLineData(Stages stageName)
        {
            var stage = script.GetStage(stageName);
            currentStage = stage.name;
            model.stage = currentStage;
            
            return stage.LinesData;
        }

        private void PlayLine(LineData lineData)
        {
            eventBus.Fire(new StartLineSignal(lineData.Character, lineData.id));
            IsSkippable = true;
            voicePlayer.clip = lineData.Clip;
            voicePlayer.Play();
            linePlayer.StartPlayLine(lineData.Clip.length + lineData.DelayAfter, lineData.EventTime, 
                PlayNext, DoAction);
        }

        private void PlayNext()
        {
            currentClip++;
            model.line = currentClip;
            eventBus.Fire(new EndLineSignal());
            voicePlayer.Stop();
            
            if(CheckEndStage())
                return;
            PlayLine(linesData[currentClip]);
        }

        private void DoAction()
        {
            if(currentClip >= linesData.Length) return;
            if(linesData[currentClip].Event == null) return;
            if(linesData[currentClip].Event.HasExecuted) return;
            linesData[currentClip].Event.DoAction();
        }

        private bool CheckEndStage()
        {
            if (currentClip < linesData.Length) return false;
            eventBus.Fire(new EndStageSignal(currentStage));
            return true;
        }

        private LinePlayer CreateLinePLayer()
        {
            var newLinePlayer = new GameObject("LinePlayer");
            return newLinePlayer.AddComponent<LinePlayer>();
        }

        private void DebugStage(Stages stageName)
        {
#if UNITY_EDITOR
            Debug.Log($"<color={C.Highlight}>Stage: {currentStage}</color>");
            if(linesData.Length < 1)
                Debug.Log($"<color={C.Error}>Stage : {stageName} is empty</color>");
#endif
        }
    }
}