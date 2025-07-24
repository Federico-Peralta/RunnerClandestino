using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabsToSpawn; // Array de prefabs para instanciar aleatoriamente
    [SerializeField] private float spawnRadius = 0.5f; // Radio máximo de dispersión

    private void Start()
    {
        StartCoroutine(SpawnPrefabRoutine());
    }

    private IEnumerator SpawnPrefabRoutine()
    {
        while (true)
        {
            SpawnRandomPrefab();
            float randomInterval = Random.Range(3f, 5f);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    public void SpawnRandomPrefab()
    {
        if (prefabsToSpawn != null && prefabsToSpawn.Length > 0)
        {
            int randomIndex = Random.Range(0, prefabsToSpawn.Length);
            GameObject prefab = prefabsToSpawn[randomIndex];
            if (prefab != null)
            {
                // Genera un punto aleatorio dentro de un círculo (en XZ)
                Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
                Vector3 spawnPos = transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
                Instantiate(prefab, spawnPos, transform.rotation);
            }
            else
            {
                Debug.LogWarning("Uno de los prefabs en el array es null.");
            }
        }
        else
        {
            Debug.LogWarning("No se ha asignado ningún prefab para instanciar.");
        }
    }
}
