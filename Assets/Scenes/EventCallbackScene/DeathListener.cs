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
            EventSystem.Current.RegisterListener<UnitDeathEventInfo>(OnUnitDied);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnUnitDied(UnitDeathEventInfo unitDeathInfo)
        {
            Debug.Log("Alerted about unit death: " + unitDeathInfo.UnitGO.name);
        }
    }
}