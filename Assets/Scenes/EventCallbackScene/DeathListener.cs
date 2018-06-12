using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class DeathListener : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            // subscribe fully generic to an event
            EventSystem<UnitDeathEvent>.EventTriggered += OnUnitDied;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void OnUnitDied(UnitDeathEvent unitDeathInfo)
        {
            Debug.Log("Alerted about unit death: " + unitDeathInfo.UnitGO.name);
        }
    }
}