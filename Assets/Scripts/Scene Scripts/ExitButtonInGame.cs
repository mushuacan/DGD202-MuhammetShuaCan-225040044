using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//This code created with ChatGPT
//Also I changed some part of it and added if/else
public class ExitButtonInGame : MonoBehaviour
{
    public GameObject inGameMenu; // ��k�� yaz�s� i�in referans

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
            Cursor.lockState = CursorLockMode.Locked; // �mleci kilitle
            Cursor.visible = false; // �mleci gizle
        }
        else
        {//true
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined; // �mleci A�
            Cursor.visible = true; // �mleci gizle
            Debug.Log("Esc men�s� a��ld�");
        }
        Debug.Log("Esc'ye bas�ld�. isShowing -> " + isShowing);
    }

    private void MenuSetActive(bool activation)
    {
        inGameMenu.SetActive(activation);
    }

    public void OnExitButtonPressed()
    {
        Application.Quit(); // Uygulamay� kapat
    }

}