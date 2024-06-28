using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
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
    public AudioManager audioManager;

    private float timer = 0f;
    public bool gameEnded = false;
    public bool endMusicStarted = false;

    public AudioSource audioSource;
    public AudioClip endMusicClip;


    private void Start()
    {
        EndMenu.SetActive(false);
    }

    void Update()
    {
        if (!gameEnded)
        {
            timer += Time.deltaTime;

            CheckForWin(timer, gameTime);

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
        audioManager.SetMusicVolume();
        changeMusic.ChangeMusicClipAtEndofGame(won);
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
    void CheckForWin(float timer, float timerToEnd)
    {
        int item = playerTakeItem.collectedCount;
        if ((timer + 13.4f) >= timerToEnd && 
            timer + 13 <= timerToEnd && 
            !endMusicStarted && 
            ((timerToEnd / winCond) >= (timer / item)))
        {
            audioSource.clip = endMusicClip;
            audioSource.Play();
            endMusicStarted = !endMusicStarted;
        }

        if (endMusicStarted)
        {
            float hesap = (timerToEnd - timer) / 5;
            if (hesap <= 1)
            {
                audioManager.SetMusicVolumeForEndOfGame(hesap);
                Debug.Log("Hesap -> " + hesap);
            }
        }
    }
}
