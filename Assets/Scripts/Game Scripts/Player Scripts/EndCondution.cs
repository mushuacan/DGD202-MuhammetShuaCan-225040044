using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndCondution : MonoBehaviour
{
    public float gameTime = 60f; // Oyun s�resi (saniye cinsinden)
    public float winCond = 60f;
    public TextMeshProUGUI resultText; // Sonu� metni i�in UI Text
    public TextMeshProUGUI conditionText; // Sonu� metni i�in UI Text
    public PlayerTakeItem playerTakeItem;
    public GameObject EndMenu;
    public TextMeshProUGUI timerText;
    public ChangeMusic changeMusic;

    private float timer = 0f;
    private bool gameEnded = false;


    private void Start()
    {
        EndMenu.SetActive(false);
    }

    void Update()
    {
        if (!gameEnded)
        {
            timer += Time.deltaTime;
            if (timer >= gameTime)
            {
                if (playerTakeItem.collectedCount >= winCond - 1f)
                {
                    EndGame(true); // Oyun s�resi doldu�unda oyunu bitir
                }
                else
                {
                    EndGame(false);
                }
                EndMenu.SetActive(true);
                changeMusic.EndGame();
                Cursor.lockState = CursorLockMode.Confined; 
                Cursor.visible = true; 
                Time.timeScale = 0f;
            }
        }
        timerText.text = "Time: " + Mathf.Round((gameTime - timer) * 10f) / 10f;
    }
    void EndGame(bool won)
    {
        changeMusic.ChangeMusicClip(won);
        gameEnded = true;
        if (won)
        {
            resultText.text = "You Won";
            conditionText.text = "You collected more than " + winCond ; 
        }
        else
        {
            resultText.text = "Lost";
            conditionText.text = "You couldn't collect more than " + winCond;
        }
    }
}
