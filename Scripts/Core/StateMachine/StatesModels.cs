using System;
using Data;
using UnityEngine.Serialization;

namespace Core
{
    [Serializable]
    public class GameStateModel : StateModel
    {
         public bool isNewGame = true;
         public int life = 5;
         public int motherStage;
         public bool motherIdle = true;

        public bool tutorialChoose;
        public bool firstMessageChoose;
        public bool winEffectChoose;
        public Stages stage = Stages.Intro;
        public int line;
        public Decorations decoration = Decorations.Intro;

        public bool hadFail;
        public bool hadCriticalFail;

        public bool messageChoose;
        public int messageGroupIndex;
        public int messageIndex;
        public Message[] messagesGroup;


        public void Default()
        {
            isNewGame = true;
            life = 5;
            motherStage = 0;
            motherIdle = true;
            tutorialChoose = false;
            firstMessageChoose = false;
            stage = Stages.Intro;
            line = 0;
            decoration = Decorations.Intro;
            messageChoose = false;
            messageGroupIndex = 0;
            messageIndex = 0;
            messagesGroup = null;
            hadFail = false;
            hadCriticalFail = false;
        }
    }

    [Serializable]
    public class LoadGameStateModel : StateModel
    {
        
    }
}