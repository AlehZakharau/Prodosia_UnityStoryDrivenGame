using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    public abstract class Event : ScriptableObject
    {
        [FormerlySerializedAs("HasScriptDependence")] public bool hasScriptDependence;

        public bool HasExecuted { get; set; }
        public virtual void DoAction(bool checkScript = false)
        {
            if(!checkScript && !hasScriptDependence || HasExecuted) return;
        }
    }
}