using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirectCallbacks
{
    public class Spawner : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        public GameObject UnitPrefab;

        public delegate void OnUnitSpawnedDelegate(Health health);
        public event OnUnitSpawnedDelegate OnUnitSpawnedListeners;

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
            GameObject go = Instantiate(UnitPrefab);
            if(OnUnitSpawnedListeners != null)
            {
                OnUnitSpawnedListeners(go.GetComponent<Health>());
            }
        }
    }
}