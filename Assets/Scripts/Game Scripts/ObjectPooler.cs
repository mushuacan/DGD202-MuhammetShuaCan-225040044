using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject objectToPool;
    public int amountToPool;
    private List<GameObject> pooledObjects;
    public Transform parentTransform; // Parent olarak kullanmak istediðiniz Transform

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool, parentTransform); // Parent olarak belirliyoruz
            obj.name = $"{objectToPool.name}_{i}";
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        DebugActiveInactiveCounts();

        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                Debug.Log($"Returning pooled object: {obj.name}");
                return obj;
            }
        }

        GameObject newObj = Instantiate(objectToPool, parentTransform); // Yeni bir nesne oluþtururken de parent belirliyoruz
        newObj.SetActive(false);
        pooledObjects.Add(newObj);
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pooledObjects.Remove(obj);
        pooledObjects.Add(obj);
    }


    private void DebugActiveInactiveCounts()
    {
        int activeCount = 0;
        int inactiveCount = 0;

        foreach (GameObject obj in pooledObjects)
        {
            if (obj.activeInHierarchy)
            {
                activeCount++;
            }
            else
            {
                inactiveCount++;
            }
        }

        Debug.Log($"Active Objects: {activeCount}, Inactive Objects: {inactiveCount}");
    }
}
