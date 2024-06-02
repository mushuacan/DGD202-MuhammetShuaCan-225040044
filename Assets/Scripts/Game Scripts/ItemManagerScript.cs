using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Created with ChatGPT and arranged by Mushu
public class ItemManagerScript : MonoBehaviour
{
    public GameObject prefab; // Spawnlanacak prefab
    public float spawnInterval = 2f; // Objelerin spawnlanma aral��� (saniye cinsinden)
    public Vector2 spawnRange = new Vector2(10f, 10f); // Objelerin spawnlanaca�� mesafe aral��� (x-z ekseninde)


    private void Start()
    {
        // Coroutine ba�lat�l�r
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Belirtilen aral�kla bekle
            yield return new WaitForSeconds(spawnInterval);

            // Rastgele bir pozisyon olu�tur
            float randomX = Random.Range(spawnRange.x, -spawnRange.x);
            float randomZ = Random.Range(spawnRange.y, -spawnRange.y);
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y, randomZ);

            // Prefab� spawnla
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }
}
