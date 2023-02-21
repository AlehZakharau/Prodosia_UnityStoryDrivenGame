    using ReactiveSystem;
using UnityEngine;

namespace Data.Events
{
    public struct ShowWinEffectSignal{}
    [CreateAssetMenu(fileName = "WinEffectEvent", menuName = "Event/WinEffect", order = 0)]
    public class ShowWinEffectEvent : Event
    {
        public override void DoAction(bool checkScript = false)
        {
            base.DoAction();
            MonoEventBus.Fire(new ShowWinEffectSignal());
            HasExecuted = true;
        }
    }
}