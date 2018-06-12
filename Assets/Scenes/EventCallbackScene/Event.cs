using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public abstract class Event
    {
        /*
         * The base Event,
         * might have some generic text
         * for doing Debug.Log?
         */

        public string EventDescription;
    }

    public class DebugEvent : Event
    {
        public int VerbosityLevel;
    }

    public class UnitDeathEvent : Event
    {
        public GameObject UnitGO;
        /*

        Info about cause of death, our killer, etc...

        Could be a struct, readonly, etc...

        */
    }
}