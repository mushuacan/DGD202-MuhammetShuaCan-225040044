using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EndCondution : MonoBehaviour
{
    public float gameTime = 60f; // Oyun süresi (saniye cinsinden)
    public float winCond = 60f;
    public Image endGameMenuImage;
    public Color winColor;
    public Color loseColor;
    public TextMeshProUGUI resultText; // Sonuç metni için UI Text
    public TextMeshProUGUI conditionText; // Sonuç metni için UI Text
    public PlayerTakeItem playerTakeItem;
    public GameObject EndMenu;
    public TextMeshProUGUI timerText;
    public ChangeMusic changeMusic;

    private float timer = 0f;
    public bool gameEnded = false;


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
                    EndGame(true); // Oyun süresi dolduðunda oyunu bitir
                }
                else
                {
                    EndGame(false);
                }
                EndMenu.SetActive(true);
                changeMusic.EndGame();
                Cursor.lockState = CursorLockMode.Confined; 
                Cursor.visible = true; 
                Camera.main.GetComponent<EndedGameCameraScript>().EndGame();
                Time.timeScale = 0; // Zamaný durdur
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
            endGameMenuImage.color = winColor;
            conditionText.text = "You collected more than " + winCond + " apples"; 
        }
        else
        {
            resultText.text = "Lost";
            endGameMenuImage.color = loseColor;
            conditionText.text = "You couldn't collect more than " + winCond + " apples";
        }
    }
}
