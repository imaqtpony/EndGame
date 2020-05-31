using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// at the barrrel quest, if the barrel that we have to move hits the barrel in the dirty water
/// it disables the navmesh obstacle and the player can pass
/// </summary>
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
