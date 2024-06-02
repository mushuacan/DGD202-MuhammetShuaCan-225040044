using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//This code created with ChatGPT
//Also I changed some part of it and added if/else
public class ExitButtonInGame : MonoBehaviour
{
    public GameObject inGameMenu; // Çýkýþ yazýsý için referans

    private bool isShowing = false;

    private void Start()
    {
        inGameMenu.SetActive(isShowing);
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
        inGameMenu.SetActive(isShowing);
        if (!isShowing)
        {
            Cursor.lockState = CursorLockMode.Locked; // Ýmleci kilitle
            Cursor.visible = false; // Ýmleci gizle
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined; // Ýmleci kilitle
            Cursor.visible = true; // Ýmleci gizle
        }
        Debug.Log("Esc'ye basýldý. isShowing -> " + isShowing);
    }

    public void OnExitButtonPressed()
    {
        Application.Quit(); // Uygulamayý kapat
    }

}
