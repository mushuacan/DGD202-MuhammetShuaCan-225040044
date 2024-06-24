using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab; // Pool'da kullan�lacak prefab
    public int poolSize = 10; // Pool'daki nesne say�s�
    public Transform parentTransform; // Parent objesi

    private List<GameObject> pool;

    private void Awake()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, parentTransform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        // E�er t�m objeler kullan�l�yorsa yeni bir obje olu�turup pool'a ekleyebiliriz (iste�e ba�l�)
        GameObject newObj = Instantiate(prefab, parentTransform);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(parentTransform); // Parent'� tekrar ayarla
    }
}
