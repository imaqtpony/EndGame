using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarreQuest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            Destroy(GetComponent<NavMeshObstacle>());
        }
    }
}
