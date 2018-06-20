using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class Handler
    {
        public object Target { get; set; }
        public System.Reflection.MethodInfo Method { get; set; }
    }

    public class EventSystem : MonoBehaviour
    {
        // Use this for initialization
        void OnEnable()
        {
            __Current = this;
        }

        static private EventSystem __Current;
        static public EventSystem Current
        {
            get
            {
                if(__Current == null)
                {
                    __Current = GameObject.FindObjectOfType<EventSystem>();
                }

                return __Current;
            }
        }

        delegate void EventListener(EventInfo ei);
        Dictionary<Type, List<Handler>> eventListeners;

        public void RegisterListener<T>(Action<T> listener) where T : EventInfo
        {
            var target = listener.Target;
            var method = listener.Method;

            Type eventType = typeof(T);
            if (eventListeners == null)
            {
                eventListeners = new Dictionary<Type, List<Handler>>();
            }

            if (eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
            {
                eventListeners[eventType] = new List<Handler>();
            }

            eventListeners[eventType].Add(new Handler { Target = target, Method = method });
        }

        public void UnregisterListener<T>(Action<T> listener) where T : EventInfo
        {
            var eventType = typeof(T);
            eventListeners[eventType].RemoveAll(l => l.Target == listener.Target && l.Method == listener.Method);
        }

        public void FireEvent(EventInfo eventInfo)
        {
            Type trueEventInfoClass = eventInfo.GetType();
            if (eventListeners == null || eventListeners[trueEventInfoClass] == null)
            {
                // No one is listening, we are done.
                return;
            }

            foreach (Handler el in eventListeners[trueEventInfoClass])
            {
                el.Method.Invoke(el.Target, new[] { eventInfo });
            }
        }
    }
}
