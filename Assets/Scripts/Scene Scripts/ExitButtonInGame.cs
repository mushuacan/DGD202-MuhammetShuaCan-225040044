using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitButtonInGame : MonoBehaviour
{
    public GameObject inGameMenu; // ��k�� yaz�s� i�in referans

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
                Cursor.lockState = CursorLockMode.Locked; // �mleci kilitle
                Cursor.visible = false; // �mleci gizle
            }else
            {
                Cursor.lockState = CursorLockMode.Confined; // �mleci kilitle
                Cursor.visible = true; // �mleci gizle
            }
            Debug.Log("Esc'ye bas�ld�. isShowing -> " + isShowing);
        }
    }

    public void OnExitButtonPressed()
    {
        Application.Quit(); // Uygulamay� kapat
    }

}
