//Last Edited : 30/05

using UnityEngine;

/// <summary>
/// The enemies spawner. Appears whenever a tool is left on the ground for too long, instanciates enemies from their prefab
/// </summary>
public class Spawn : MonoBehaviour
{
    [SerializeField] 
    [Tooltip("The enemy prefab")]
    private GameObject m_prefab;


    private void SpawnEnemies()
    {
        var nb_enemies = 3;
        int randNumbEnemies = Random.Range(1, nb_enemies);

        for (int i = 1; i < randNumbEnemies; i++)
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
