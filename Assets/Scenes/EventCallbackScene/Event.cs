using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public abstract class Event<T> where T : Event<T> {
        /*
         * The base Event,
         * might have some generic text
         * for doing Debug.Log?
         */

        public string Description;
        
        public delegate void EventListener(T info);
        private static event EventListener events;
        
        public static void RegisterListener(EventListener listener) {
            events += listener;
        }

        public static void UnregisterListener(EventListener listener) {
            events -= listener;
        }

        public void FireEvent() {
            if (events != null) {
                events(this as T);
            }
        }
    }

    public class DebugEvent : Event<DebugEvent>
    {
        public int VerbosityLevel;
    }

    public class UnitDeathEvent : Event<UnitDeathEvent>
    {
        public GameObject UnitGO;
        /*

        Info about cause of death, our killer, etc...

        Could be a struct, readonly, etc...

        */
    }
}