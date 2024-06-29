using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScaleAndCameraPosition : MonoBehaviour
{
    public GameObject targetObject; // Boyutu artan objenin referansý, Unity Inspector'dan atayabileceðiniz bir GameObject

    float scaleFactor = 2.0f; // Ölçek faktörü, obje büyüdükçe kameranýn ne kadar geriye gideceðini kontrol eder

    void Update()
    {
        float objectScale = targetObject.transform.localScale.x - 0.8f;

        Vector3 newPosition = new Vector3(0f, objectScale * scaleFactor, -objectScale * scaleFactor);

        transform.localPosition = newPosition;
    }

}
