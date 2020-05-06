using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// wip
public class StandOnSwitch : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        float mass = other.GetComponent<Rigidbody>().mass;

        if (mass > 20)
        {
            //Do stuff
        }
    }

}
