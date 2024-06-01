using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitButtonInGame : MonoBehaviour
{
    public GameObject inGameMenu; // Çýkýþ yazýsý için referans

    private bool isShowing = true;

    private void Start()
    {
        inGameMenu.SetActive(isShowing);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isShowing = !isShowing;
            inGameMenu.SetActive(isShowing);
            if (!isShowing)
            {
                Cursor.lockState = CursorLockMode.Locked; // Ýmleci kilitle
                Cursor.visible = false; // Ýmleci gizle
            }else
            {
                Cursor.lockState = CursorLockMode.Confined; // Ýmleci kilitle
                Cursor.visible = true; // Ýmleci gizle
            }
            Debug.Log("Esc'ye basýldý. isShowing -> " + isShowing);
        }
    }

    public void OnExitButtonPressed()
    {
        Application.Quit(); // Uygulamayý kapat
    }

}
