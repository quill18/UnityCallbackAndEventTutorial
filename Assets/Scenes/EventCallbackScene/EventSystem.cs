using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class EventSystem
    {
        private static Dictionary<Type, EventSystem> _systems = new Dictionary<Type, EventSystem>();

        public static void Register<T>(Action<T> listener) where T : EventInfo
        {
            Type eventType = typeof(T);
            if (_systems == null)
            {
                _systems = new Dictionary<Type, EventSystem>();
            }

            if (!_systems.ContainsKey(eventType) || _systems[eventType] == null)
            {
                _systems[eventType] = new EventSystem<T>();
            }

          ((EventSystem<T>)_systems[eventType]).RegisterListener(listener);
        }

        public static void FireEvent<T>(T eventInfo) where T : EventInfo
        {
            Type eventType = typeof(T);
            if (_systems?[eventType] == null)
            {
                // No one is listening, we are done.
                return;
            }

          ((EventSystem<T>)_systems[eventType]).FireEvent(eventInfo);
        }
    }

    public class EventSystem<T> : EventSystem where T : EventInfo
    {
        private Dictionary<Type, Action<T>> _eventListeners;

        public void RegisterListener(Action<T> listener)
        {
            // Can't ever be null as it's initialised with class
            if (_eventListeners == null)
            {
                _eventListeners = new Dictionary<Type, Action<T>>();
            }

            Type eventType = typeof(T);

            if (!_eventListeners.ContainsKey(eventType))
                _eventListeners[eventType] = listener;
            else
                _eventListeners[eventType] += listener;
        }

        public void UnregisterListener(Action<T> listener)
        {
            // TODO
        }

        public void FireEvent(T eventInfo)
        {
            Type trueEventInfoClass = typeof(T);
            if (_eventListeners?[trueEventInfoClass] == null)
            {
                // No one is listening, we are done.
                return;
            }

            _eventListeners[trueEventInfoClass]?.Invoke(eventInfo);
        }
    }
}