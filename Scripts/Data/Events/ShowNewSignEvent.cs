using ReactiveSystem;
using UnityEngine;

namespace Data.Events
{
    public struct ShowNewSignSignal{}

    [CreateAssetMenu(fileName = "ShowNewSignEvent", menuName = "Event/ShowNewSign", order = 0)]
    public class ShowNewSignEvent : Event
    {
        public override void DoAction(bool checkScript = false)
        {
            base.DoAction();
            MonoEventBus.Fire(new ShowNewSignSignal());
            HasExecuted = true;
        }
    }
    
}