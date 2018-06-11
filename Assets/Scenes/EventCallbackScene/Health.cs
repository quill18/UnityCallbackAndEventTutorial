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

            UnitDeathEventInfo udei = new UnitDeathEventInfo();
            udei.EventDescription = "Unit "+ gameObject.name +" has died.";
            udei.UnitGO = gameObject;

            EventSystem.FireEvent(udei);

            Destroy(gameObject);
        }
    }
}