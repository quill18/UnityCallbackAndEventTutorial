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
            UnitDeathEvent.RegisterListener(OnUnitDied);
        }

        void OnDestroy() {
            UnitDeathEvent.UnregisterListener(OnUnitDied);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnUnitDied(UnitDeathEvent unitDeath)
        {
            Debug.Log("Alerted about unit death: " + unitDeath.UnitGO.name);
        }
    }
}