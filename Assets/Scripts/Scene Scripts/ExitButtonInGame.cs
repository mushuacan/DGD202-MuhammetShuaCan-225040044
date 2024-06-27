using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//This code created with ChatGPT
//Also I changed some part of it and added if/else
public class ExitButtonInGame : MonoBehaviour
{
    public GameObject inGameMenu; // Çýkýþ yazýsý için referans
    public EndCondution endCondution;
    public ChangeMusic changeMusic;

    private bool isShowing = false;

    private void Awake()
    {
        MenuSetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeSinceLevelLoad + 0.5f < endCondution.gameTime)
            {
                //Debug.Log("Oyun baþýndan beri geçen zaman -> " + Time.timeSinceLevelLoad);
                //Debug.Log("Oyunun durma vakti -> " + endCondution.gameTime);
                InGameMenu();
                
            }
        }
    }

    public void InGameMenu()
    {
        isShowing = !isShowing;
        MenuSetActive(isShowing);

        if (isShowing)
        {//true
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            changeMusic.PauseGame();
        }
        else
        {//false
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false;
            changeMusic.ResumeGame();
        }
    }

    private void MenuSetActive(bool activation)
    {
        inGameMenu.gameObject.SetActive(activation);
    }

    public void OnExitButtonPressed()
    {
        Application.Quit(); // Uygulamayý kapat
    }

}