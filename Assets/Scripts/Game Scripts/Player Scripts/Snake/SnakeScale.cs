using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScale : MonoBehaviour
{
    public void ArrangeScale(int eatenCount = -1, int position = 3)
    {
        eatenCount = CheckEatenCount(eatenCount);
        
        float scale = 0.8f + (eatenCount - position * 5) / 300f;
        transform.localScale = new Vector3(scale, scale, scale);
    }


    private int CheckEatenCount(int eatenCount)
    {
        if (eatenCount == -1)
        {
            GameObject playerObject = GameObject.Find("Player"); // "Player" ismini GameObject'inizdeki isimle deðiþtirin
            if (playerObject != null)
            {
                PlayerTakeItem playerTakeItem = playerObject.GetComponent<PlayerTakeItem>();
                if (playerTakeItem != null)
                {
                    eatenCount = playerTakeItem.collectedCount;
                }
                else
                {
                    Debug.LogError("PlayerTakeItem component'i bulunamadý.");
                }
            }
        }
        return eatenCount;
    }
}
