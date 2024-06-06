using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//This code created with ChatGPT
//Also I changed some part of it and added if/else
public class ExitButtonInGame : MonoBehaviour
{
    public GameObject inGameMenu; // Çýkýþ yazýsý için referans

    private bool isShowing = false;

    private void Awake()
    {
        MenuSetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InGameMenu();
        }
    }

    private void InGameMenu()
    {
        isShowing = !isShowing;
        MenuSetActive(isShowing);
        if (!isShowing)
        {//false
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked; // Ýmleci kilitle
            Cursor.visible = false; // Ýmleci gizle
        }
        else
        {//true
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined; // Ýmleci Aç
            Cursor.visible = true; // Ýmleci gizle
            Debug.Log("Esc menüsü açýldý");
        }
        Debug.Log("Esc'ye basýldý. isShowing -> " + isShowing);
    }

    private void MenuSetActive(bool activation)
    {
        inGameMenu.SetActive(activation);
    }

    public void OnExitButtonPressed()
    {
        Application.Quit(); // Uygulamayý kapat
    }

}