using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public abstract class EventInfo
    {
        /*
         * The base EventInfo,
         * might have some generic text
         * for doing Debug.Log?
         */

        public string EventDescription;
    }

    public class DebugEventInfo : EventInfo
    {
        public int VerbosityLevel;
    }

    public class UnitDeathEventInfo : EventInfo
    {
        public GameObject UnitGO;
        /*

        Info about cause of death, our killer, etc...

        Could be a struct, readonly, etc...

        */
    }
}