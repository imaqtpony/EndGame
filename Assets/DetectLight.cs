using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLight : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "LightCone")
        {
            Debug.Log("marche stp");
            Destroy(gameObject);
        }
    }
}
