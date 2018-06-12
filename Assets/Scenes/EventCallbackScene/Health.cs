using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class Health : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

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

            UnitDeathEvent ude = new UnitDeathEvent();
            ude.EventDescription = "Unit "+ gameObject.name +" has died.";
            ude.UnitGO = gameObject;

            // fire the event like so:
            EventSystem<UnitDeathEvent>.FireEvent(ude);

            Destroy(gameObject);
        }
    }
}