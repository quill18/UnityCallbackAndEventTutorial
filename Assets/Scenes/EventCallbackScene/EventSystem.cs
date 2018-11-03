using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EventCallbacks
{
    public class EventSystem : MonoBehaviour
    {
        // this class will hold the listener action 
        // and an unique key for controlling the event
        class GameListener
        {
            public EventListener listener;
            public System.Guid guid;

            public GameListener(EventListener listener)
            {
                this.listener = listener;
                this.guid = Guid.NewGuid();
            }
        }

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
                if (__Current == null)
                {
                    __Current = GameObject.FindObjectOfType<EventSystem>();
                }

                return __Current;
            }
        }

        delegate void EventListener(EventInfo ei);
        Dictionary<System.Type, List<GameListener>> eventListeners;

        public void RegisterListener<T>(System.Action<T> listener, ref Guid guid) where T : EventInfo
        {
            System.Type eventType = typeof(T);
            if (eventListeners == null)
            {
                eventListeners = new Dictionary<System.Type, List<GameListener>>();
            }

            if (eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
            {
                eventListeners[eventType] = new List<GameListener>();
            }

            // Wrap a type converstion around the event listener
            // I'm betting someone better at C# generic syntax
            // can find a way around this.
            EventListener wrapper = (ei) => { listener((T)ei); };

            GameListener gameListener = new GameListener(wrapper);

            eventListeners[eventType].Add(gameListener);

            guid = gameListener.guid;
        }

        public void UnregisterListener<T>(Guid guid) where T : EventInfo
        {
            Debug.Log("Unregister: " + guid);

            System.Type eventType = typeof(T);
            if (eventListeners == null)
            {
                eventListeners = new Dictionary<System.Type, List<GameListener>>();
            }
            if (eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
            {
                eventListeners[eventType] = new List<GameListener>();
            }

            // Find gameListenter in the dictionary to remove it
            GameListener gl = eventListeners[eventType].Where(x => x.guid == guid).FirstOrDefault();

            if (gl == null) //no gamelistenter with that Guid, just leave..
                return;

            eventListeners[eventType].Remove(gl);
            Debug.Log(guid + " unregistered");
        }

        // Second option to unregister, I don't think this is the most performatic way, however we can access it with less code.
        public void UnregisterListener(Guid guid)
        {
            Debug.Log("Unregister: " + guid);

            if (eventListeners == null)
            {
                return;
            }

            KeyValuePair<Type, List<GameListener>> listener = eventListeners.Where(x => x.Value.Any(y => y.guid == guid)).FirstOrDefault();

            GameListener gameListener = listener.Value.Where(x => x != null && x.guid == guid).FirstOrDefault();
            if (gameListener == null)//no gamelistenter with that Guid, just leave..
                return;

            Debug.Log(guid + " unregistered");
            eventListeners[listener.Key].Remove(gameListener);
        }

        public void FireEvent(EventInfo eventInfo)
        {
            System.Type trueEventInfoClass = eventInfo.GetType();
            if (eventListeners == null || eventListeners[trueEventInfoClass] == null)
            {
                // No one is listening, we are done.
                return;
            }

            foreach (GameListener el in eventListeners[trueEventInfoClass])
            {
                el.listener(eventInfo);
            }
        }

    }
}