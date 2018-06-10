using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
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
        Dictionary<System.Type, List<EventListener>> eventListeners;

        public void RegisterListener<T>(System.Action<T> listener) where T : EventInfo
        {
            System.Type eventType = typeof(T);
            if (eventListeners == null)
            {
                eventListeners = new Dictionary<System.Type, List<EventListener>>();
            }

            if(eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
            {
                eventListeners[eventType] = new List<EventListener>();
            }

            // Wrap a type converstion around the event listener
            // I'm betting someone better at C# generic syntax
            // can find a way around this.
            EventListener wrapper = (ei) => { listener((T)ei); };

            eventListeners[eventType].Add(wrapper);
        }

        public void UnregisterListener<T>(System.Action<T> listener) where T : EventInfo
        {
            // TODO
        }

        public void FireEvent(EventInfo eventInfo)
        {
            System.Type trueEventInfoClass = eventInfo.GetType();
            if (eventListeners == null || eventListeners[trueEventInfoClass] == null)
            {
                // No one is listening, we are done.
                return;
            }

            foreach(EventListener el in eventListeners[trueEventInfoClass])
            {
                el( eventInfo );
            }
        }

    }
}