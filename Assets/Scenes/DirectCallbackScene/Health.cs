using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DirectCallbacks
{
    public class Health : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Define a "template" or signature for a function
        public delegate void OnDeathCallbackDelegate(UnitDeathInfo unitDeathInfo);
        static public event OnDeathCallbackDelegate OnDeathListeners;

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

            // We COULD do these manually here, but what happens if
            // we add, remove, or change how these systems work?
            // Play Death Sound
            // Spawn Explosion
            // Increase Score
            // Drop Loot

            // Instead, we want to allow other systems to "listen" for our
            // death, and be alerted when that happens.

            // Let all our listeners know that we have died!
            if(OnDeathListeners != null)
            {
                UnitDeathInfo udi = new UnitDeathInfo();
                udi.DeadUnitGO = gameObject;
                OnDeathListeners(udi);
            }

            Destroy(gameObject);
        }
    }
}