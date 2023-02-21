using ReactiveSystem;
using UnityEngine;

namespace Data.Events
{
    public struct ShowSunSignal{}
    [CreateAssetMenu(fileName = "SunEvent", menuName = "Event/Sun", order = 2)]
    public class SunEvent : Event
    {
        public override void DoAction(bool checkScript)
        {
            base.DoAction();
            MonoEventBus.Fire(new ShowSunSignal());
            HasExecuted = true;
        }
    }
}