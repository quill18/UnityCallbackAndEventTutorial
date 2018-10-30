using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirectCallbacks
{
    public class Spawner : MonoBehaviour
    {
        public GameObject unitPrefab;

        public delegate void OnUnitSpawnedDelegate(Health health);
        public event OnUnitSpawnedDelegate OnUnitSpawnedListeners;

        void Awake()
        {
            unitPrefab = (GameObject)Instantiate(Resources.Load("Prefab/Unit_Direct"));
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SpawnUnit();
            }
        }

        void SpawnUnit()
        {
            if(OnUnitSpawnedListeners != null)
            {
                GameObject go = Instantiate(unitPrefab);
                OnUnitSpawnedListeners(go.GetComponent<Health>());
            }
        }
    }
}