using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeGrowUp : MonoBehaviour
{
    public GameObject bodyPrefab; // Gövde segmenti prefab
    public GameObject tailPrefab; // Kuyruk segmenti prefab
    public PlayerTakeItem player;
    public int initialSize = 2; // Baþlangýç boyutu
    [SerializeField] private List<GameObject> segments = new List<GameObject>(); // Segment listesi

    void Start()
    {
        // Baþlangýçta yýlaný oluþtur
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
            Debug.LogError("Yeni segmentte SnakeBodyFollow veya SnakeBody bileþeni bulunamadý!");
            return;
        }

        Transform targetTransform = null;

        if (segments.Count > 0)
        {
            // Önceki segmenti hedef olarak ayarla
            targetTransform = segments[segments.Count - 1].transform.Find("Target");
            if (targetTransform == null)
            {
                Debug.LogError("Önceki segmentte 'Target' adlý bir alt nesne bulunamadý!");
                return;
            }
        }
        else
        {
            // Ýlk segment için yýlanýn baþýný hedef olarak ayarla
            targetTransform = transform.Find("Target");
            if (targetTransform == null)
            {
                Debug.LogError("Yýlanýn baþýnda 'Target' adlý bir alt nesne bulunamadý!");
                return;
            }
        }

        // Yeni segmenti hedef konumuna yerleþtir
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
            // Son segmenti kaldýr ve yerine bir gövde + kuyruk ekle
            GameObject oldTail = segments[segments.Count - 1];
            segments.RemoveAt(segments.Count - 1);
            Destroy(oldTail);

            // Yeni gövde segmenti ekle
            GameObject newBodySegment = Instantiate(bodyPrefab);
            SnakeBodyFollow bodyFollow = newBodySegment.GetComponent<SnakeBodyFollow>();
            SnakeBody characterMovement = newBodySegment.GetComponent<SnakeBody>();

            if (bodyFollow == null || characterMovement == null)
            {
                Debug.LogError("Yeni gövde segmentinde SnakeBodyFollow veya SnakeBody bileþeni bulunamadý!");
                return;
            }

            Transform targetTransform = null;

            if (segments.Count > 0)
            {
                targetTransform = segments[segments.Count - 1].transform.Find("Target");
                if (targetTransform == null)
                {
                    Debug.LogError("Önceki segmentte 'Target' adlý bir alt nesne bulunamadý!");
                    return;
                }
            }
            else
            {
                targetTransform = transform.Find("Target");
                if (targetTransform == null)
                {
                    Debug.LogError("Yýlanýn baþýnda 'Target' adlý bir alt nesne bulunamadý!");
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
                Debug.LogError("Yeni kuyruk segmentinde SnakeBodyFollow bileþeni bulunamadý!");
                return;
            }

            Transform newBodyTargetTransform = newBodySegment.transform.Find("Target");
            if (newBodyTargetTransform == null)
            {
                Debug.LogError("Yeni gövde segmentinde 'Target' adlý bir alt nesne bulunamadý!");
                return;
            }

            tailBodyFollow.target = newBodyTargetTransform;
            newTailSegment.transform.position = newBodyTargetTransform.position;

            SnakeBody tailCharacterMovement = newTailSegment.GetComponent<SnakeBody>();
            if (tailCharacterMovement == null)
            {
                Debug.LogError("Yeni kuyruk segmentinde SnakeBody bileþeni bulunamadý!");
                return;
            }

            tailCharacterMovement.SetPosition(segments.Count + 1);

            segments.Add(newTailSegment);
        }
    }




    public void Grow()
    {
        // Yýlaný büyüt
        //AddSegment();
        UpdateTail();
    }

    public void Collect()
    {
        // Nesne toplandýðýnda çaðrýlacak
        
        if (segments.Count - initialSize + 1 < (player.collectedCount / 10))
        {
            Grow();
        }
    }

}
