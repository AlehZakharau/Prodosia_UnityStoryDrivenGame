using ReactiveSystem;
using UnityEngine;

namespace Data.Events
{
    public struct TurnButtonsSignal{}
    [CreateAssetMenu(fileName = "TurnButtons", menuName = "Event/TurnButtons", order = 3)]
    public class TurnButtonsEvent : Event
    {
        public override void DoAction(bool checkScript = false)
        {
            base.DoAction(checkScript);
            MonoEventBus.Fire(new TurnButtonsSignal());
            HasExecuted = true;
        }
    }
}