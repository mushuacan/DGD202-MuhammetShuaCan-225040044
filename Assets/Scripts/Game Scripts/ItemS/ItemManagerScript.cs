using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

public class ItemManagerScript : MonoBehaviour
{
    public GameObject prefab; // Spawnlanacak prefab
    public float spawnInterval = 2f; // Objelerin spawnlanma aral��� (saniye cinsinden)
    public Vector2 spawnRange = new Vector2(10f, 10f); // Objelerin spawnlanaca�� mesafe aral��� (x-z ekseninde)
    public ObjectPool objectPool; // Object pool referans�
    public Transform parentTransform; // Parent objesi
    public float objectLifetime = 5f; // Objelerin aktif kalma s�resi

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
            float randomX = Random.Range(-spawnRange.x, spawnRange.x); // Random.Range(min, max) s�ralamas� d�zeltilmeli
            float randomZ = Random.Range(-spawnRange.y, spawnRange.y); // Random.Range(min, max) s�ralamas� d�zeltilmeli
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y, randomZ);

            // Pool'dan bir obje al ve aktif et
            GameObject obj = objectPool.GetPooledObject();
            obj.transform.position = spawnPosition;
            obj.transform.SetParent(parentTransform); // Parent'� ayarla

            // Ba�lang�� durumuna geri d�nd�rmek i�in gerekli ayarlar� yap
            ResetObject(obj);

            obj.SetActive(true);
            // Objeyi belirli bir s�re sonra pool'a geri d�nd�r
            StartCoroutine(ReturnObjectToPool(obj, objectLifetime));
        }
    }

    private IEnumerator ReturnObjectToPool(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        objectPool.ReturnToPool(obj);
    }

    private void ResetObject(GameObject obj)
    {
        // Objeyi ba�lang�� durumuna geri d�nd�r
        ItemScript itemScript = obj.GetComponent<ItemScript>();
        if (itemScript != null)
        {
            //itemScript.ResetItem();
        }
    }
}
