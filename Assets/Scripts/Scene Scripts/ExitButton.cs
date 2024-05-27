using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitButton : MonoBehaviour
{
    public GameObject exitText; // ��k�� yaz�s� i�in referans

    public void OnExitButtonPressed()
    {
        StartCoroutine(ShowExitTextAndQuit());
    }

    private IEnumerator ShowExitTextAndQuit()
    {
        exitText.SetActive(true); // ��k�� yaz�s�n� g�ster
        yield return new WaitForSeconds(2); // 2 saniye bekle
        Application.Quit(); // Uygulamay� kapat
    }
}
