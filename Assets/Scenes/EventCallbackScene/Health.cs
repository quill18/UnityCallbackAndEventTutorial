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

            UnitDeathEvent udei = new UnitDeathEvent();
            udei.Description = "Unit "+ gameObject.name +" has died.";
            udei.UnitGO = gameObject;
            udei.FireEvent();

            Destroy(gameObject);
        }
    }
}