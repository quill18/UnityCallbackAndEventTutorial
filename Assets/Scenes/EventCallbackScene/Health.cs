using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class Health : MonoBehaviour
    {

        Guid DebugEventGuid;

        // Use this for initialization
        void Start()
        {
            EventSystem.Current.RegisterListener<DebugEventInfo>(DebugEvent, ref DebugEventGuid);
        }

        void DebugEvent(DebugEventInfo debugEvent)
        {
            Debug.Log(debugEvent.EventDescription);
        }
        void OnDestroy()
        {
            //Using first option
            //EventSystem.Current.UnregisterListener<DebugEventInfo>(DebugEventGuid);


            //Using second option
            EventSystem.Current.UnregisterListener(DebugEventGuid);

        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Die();
            }
        }

        void Die()
        {
            // I am dying for some reason.

            UnitDeathEventInfo udei = new UnitDeathEventInfo();
            udei.EventDescription = "Unit " + gameObject.name + " has died.";
            udei.UnitGO = gameObject;

            EventSystem.Current.FireEvent(
                udei
                );

            Destroy(gameObject);
        }
    }
}