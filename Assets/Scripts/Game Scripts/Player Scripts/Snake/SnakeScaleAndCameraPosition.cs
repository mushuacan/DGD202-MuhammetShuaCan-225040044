using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScaleAndCameraPosition : MonoBehaviour
{
    public GameObject targetObject; // Boyutu artan objenin referans�, Unity Inspector'dan atayabilece�iniz bir GameObject

    float scaleFactor = 2.0f; // �l�ek fakt�r�, obje b�y�d�k�e kameran�n ne kadar geriye gidece�ini kontrol eder

    void Update()
    {
        float objectScale = targetObject.transform.localScale.x - 0.8f;

        Vector3 newPosition = new Vector3(0f, objectScale * scaleFactor, -objectScale * scaleFactor);

        transform.localPosition = newPosition;
    }

}
