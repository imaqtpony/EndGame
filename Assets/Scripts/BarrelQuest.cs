using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarrelQuest : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Barrel")
        {
            Destroy(GetComponent<NavMeshObstacle>());
        }
    }
}
