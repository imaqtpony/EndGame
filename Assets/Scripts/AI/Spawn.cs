using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject m_prefab;


    private void SpawnEnemies()
    {
        var nb_enemies = 3;

        for (int i = 1; i < nb_enemies; i++)
        {
            Instantiate(m_prefab, RandomizeSpawn(), transform.rotation);
        }
    }

    private void OnEnable()
    {
        SpawnEnemies();
    }

    private Vector3 RandomizeSpawn()
    {
        return new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y, transform.position.z + Random.Range(-3f, 3f));
    }
}
