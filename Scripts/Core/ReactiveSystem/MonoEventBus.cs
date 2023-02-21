using System;
using UnityEngine;

namespace ReactiveSystem
{
    public class MonoEventBus : MonoBehaviour
    {
        private static IEventBus eventBus;
        
        public void Init(IEventBus newObserver)
        {
            eventBus = newObserver;
        }
        
        public static void Fire<T>(T signal) where T : struct
        {
            eventBus.Fire(signal);
        }

        public static void Subscribe<T>(Action<T> signal) where T : struct
        {
            eventBus.Subscribe(signal);
        }

        public static void UnSubscribe<T>(Action<T> signal) where T : struct
        {
            eventBus.UnSubscribe(signal);
        }
    }
}