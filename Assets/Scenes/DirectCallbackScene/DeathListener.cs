using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirectCallbacks
{
    public class DeathListener : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            //Spawner spawner = GameObject.FindObjectOfType<Spawner>();
            //spawner.OnUnitSpawnedListeners += OnUnitSpawned;

            Health.OnDeathListeners += OnUnitDied;
        }

        // Update is called once per frame
        void Update()
        {

        }

        //void OnUnitSpawned(Health health)
        //{
        //    Debug.Log("OnUnitSpawned -- Notified!");
        //    health.OnDeathListeners += OnUnitDied;
        //}

        void OnUnitDied(UnitDeathInfo unitDeathInfo)
        {
            // Do we need to check if unitDeathInfo.DeadUnitGO is a unit we care to
            // listen to?  For example, only the player?

            Debug.Log( "Alerted about unit death: " + unitDeathInfo.DeadUnitGO.name );
        }
    }
}