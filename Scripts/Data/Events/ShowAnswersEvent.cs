using ReactiveSystem;
using UnityEngine;

namespace Data.Events
{
    public struct ShowFirstAnswersSignal{}
    [CreateAssetMenu(fileName = "ShowAnswersEvent", menuName = "Event/ShowAnswers", order = 1)]
    public class ShowAnswersEvent : Event
    {
        public override void DoAction(bool checkScript)
        {
            base.DoAction();
            MonoEventBus.Fire(new ShowFirstAnswersSignal());
            HasExecuted = true;
        }
    }
}