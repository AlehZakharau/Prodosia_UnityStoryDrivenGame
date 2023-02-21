using System;
using System.Collections.Generic;
using UnityEngine;

namespace ReactiveSystem
{
    public interface IEventBus
    {
        public void Fire<T>(T signal) where T : struct;
        public void Subscribe<T>(Action<T> callback) where T : struct;
        public void UnSubscribe<T>(Action<T> callback) where T : struct;
    }
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, object> delegates = new();
        public void Fire<T>(T signal) where T : struct
        {
#if ENABLE_LOG
            Debug.Log(signal);
#endif
            if (delegates.TryGetValue(typeof(T), out var subscribers))
            {
                var list = (List<Action<T>>)subscribers;
                var copy = list.ToArray();
                foreach (var action in copy)
                {
                    action(signal);
                }
            }
        }

        public void Subscribe<T>(Action<T> callback) where T : struct
        {
            if (!delegates.TryGetValue(typeof(T), out var subscribers))
            {
                subscribers = new List<Action<T>>();
                delegates[typeof(T)] = subscribers;
            }
#if ENABLE_LOG
                Debug.Log($"Add to {typeof(T)} callback {callback.Method}| {delegates.Count} ");
#endif
            var list = (List<Action<T>>)subscribers;
            list.Add(callback);
        }

        public void UnSubscribe<T>(Action<T> callback) where T : struct
        {
            if (delegates.ContainsKey(typeof(T)))
            {
#if ENABLE_LOG
                    Debug.Log($"Remove from {typeof(T)} callback {callback.Method}");
#endif
                var callbacks = (List<Action<T>>)delegates[typeof(T)];{}
                callbacks.Remove(callback);
            }
        }
    }
}