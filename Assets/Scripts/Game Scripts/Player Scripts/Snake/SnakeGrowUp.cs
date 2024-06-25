using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeGrowUp : MonoBehaviour
{
    public GameObject bodyPrefab; // G�vde segmenti prefab
    public GameObject tailPrefab; // Kuyruk segmenti prefab
    public PlayerTakeItem player;
    public int initialSize = 2; // Ba�lang�� boyutu
    [SerializeField] private List<GameObject> segments = new List<GameObject>(); // Segment listesi

    void Start()
    {
        // Ba�lang��ta y�lan� olu�tur
        for (int i = 0; i < initialSize; i++)
        {
            AddSegment();
        }
        UpdateTail();
    }


    void AddSegment()
    {
        // Yeni bir segment ekle
        GameObject newSegment = Instantiate(bodyPrefab);
        SnakeBodyFollow bodyFollow = newSegment.GetComponent<SnakeBodyFollow>();
        SnakeBody characterMovement = newSegment.GetComponent<SnakeBody>();

        if (bodyFollow == null || characterMovement == null)
        {
            Debug.LogError("Yeni segmentte SnakeBodyFollow veya SnakeBody bile�eni bulunamad�!");
            return;
        }

        Transform targetTransform = null;

        if (segments.Count > 0)
        {
            // �nceki segmenti hedef olarak ayarla
            targetTransform = segments[segments.Count - 1].transform.Find("Target");
            if (targetTransform == null)
            {
                Debug.LogError("�nceki segmentte 'Target' adl� bir alt nesne bulunamad�!");
                return;
            }
        }
        else
        {
            // �lk segment i�in y�lan�n ba��n� hedef olarak ayarla
            targetTransform = transform.Find("Target");
            if (targetTransform == null)
            {
                Debug.LogError("Y�lan�n ba��nda 'Target' adl� bir alt nesne bulunamad�!");
                return;
            }
        }

        // Yeni segmenti hedef konumuna yerle�tir
        newSegment.transform.position = targetTransform.position;
        bodyFollow.target = targetTransform;

        // Segmentin pozisyonunu ayarla
        characterMovement.SetPosition(segments.Count + 1);

        segments.Add(newSegment);
    }



    void UpdateTail()
    {
        if (segments.Count > 0)
        {
            // Son segmenti kald�r ve yerine bir g�vde + kuyruk ekle
            GameObject oldTail = segments[segments.Count - 1];
            segments.RemoveAt(segments.Count - 1);
            Destroy(oldTail);

            // Yeni g�vde segmenti ekle
            GameObject newBodySegment = Instantiate(bodyPrefab);
            SnakeBodyFollow bodyFollow = newBodySegment.GetComponent<SnakeBodyFollow>();
            SnakeBody characterMovement = newBodySegment.GetComponent<SnakeBody>();

            if (bodyFollow == null || characterMovement == null)
            {
                Debug.LogError("Yeni g�vde segmentinde SnakeBodyFollow veya SnakeBody bile�eni bulunamad�!");
                return;
            }

            Transform targetTransform = null;

            if (segments.Count > 0)
            {
                targetTransform = segments[segments.Count - 1].transform.Find("Target");
                if (targetTransform == null)
                {
                    Debug.LogError("�nceki segmentte 'Target' adl� bir alt nesne bulunamad�!");
                    return;
                }
            }
            else
            {
                targetTransform = transform.Find("Target");
                if (targetTransform == null)
                {
                    Debug.LogError("Y�lan�n ba��nda 'Target' adl� bir alt nesne bulunamad�!");
                    return;
                }
            }

            newBodySegment.transform.position = targetTransform.position;
            bodyFollow.target = targetTransform;

            characterMovement.SetPosition(segments.Count + 1);
            segments.Add(newBodySegment);

            // Yeni kuyruk segmenti ekle
            GameObject newTailSegment = Instantiate(tailPrefab);
            SnakeBodyFollow tailBodyFollow = newTailSegment.GetComponent<SnakeBodyFollow>();

            if (tailBodyFollow == null)
            {
                Debug.LogError("Yeni kuyruk segmentinde SnakeBodyFollow bile�eni bulunamad�!");
                return;
            }

            Transform newBodyTargetTransform = newBodySegment.transform.Find("Target");
            if (newBodyTargetTransform == null)
            {
                Debug.LogError("Yeni g�vde segmentinde 'Target' adl� bir alt nesne bulunamad�!");
                return;
            }

            tailBodyFollow.target = newBodyTargetTransform;
            newTailSegment.transform.position = newBodyTargetTransform.position;

            SnakeBody tailCharacterMovement = newTailSegment.GetComponent<SnakeBody>();
            if (tailCharacterMovement == null)
            {
                Debug.LogError("Yeni kuyruk segmentinde SnakeBody bile�eni bulunamad�!");
                return;
            }

            tailCharacterMovement.SetPosition(segments.Count + 1);

            segments.Add(newTailSegment);
        }
    }




    public void Grow()
    {
        // Y�lan� b�y�t
        //AddSegment();
        UpdateTail();
    }

    public void Collect()
    {
        // Nesne topland���nda �a�r�lacak
        
        if (segments.Count - initialSize + 1 < (player.collectedCount / 10))
        {
            Grow();
        }
    }

}
