using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class Spawner : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        public GameObject UnitPrefab;

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
        }
    }
}