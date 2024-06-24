using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

public class ItemManagerScript : MonoBehaviour
{
    public GameObject prefab; // Spawnlanacak prefab
    public float spawnInterval = 2f; // Objelerin spawnlanma aralýðý (saniye cinsinden)
    public Vector2 spawnRange = new Vector2(10f, 10f); // Objelerin spawnlanacaðý mesafe aralýðý (x-z ekseninde)
    public ObjectPool objectPool; // Object pool referansý
    public Transform parentTransform; // Parent objesi
    public float objectLifetime = 5f; // Objelerin aktif kalma süresi

    private void Start()
    {
        // Coroutine baþlatýlýr
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Belirtilen aralýkla bekle
            yield return new WaitForSeconds(spawnInterval);

            // Rastgele bir pozisyon oluþtur
            float randomX = Random.Range(-spawnRange.x, spawnRange.x); // Random.Range(min, max) sýralamasý düzeltilmeli
            float randomZ = Random.Range(-spawnRange.y, spawnRange.y); // Random.Range(min, max) sýralamasý düzeltilmeli
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y, randomZ);

            // Pool'dan bir obje al ve aktif et
            GameObject obj = objectPool.GetPooledObject();
            obj.transform.position = spawnPosition;
            obj.transform.SetParent(parentTransform); // Parent'ý ayarla

            // Baþlangýç durumuna geri döndürmek için gerekli ayarlarý yap
            ResetObject(obj);

            obj.SetActive(true);
            // Objeyi belirli bir süre sonra pool'a geri döndür
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
        // Objeyi baþlangýç durumuna geri döndür
        ItemScript itemScript = obj.GetComponent<ItemScript>();
        if (itemScript != null)
        {
            //itemScript.ResetItem();
        }
    }
}
