using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitButton : MonoBehaviour
{
    public GameObject exitText; // Çýkýþ yazýsý için referans

    public void OnExitButtonPressed()
    {
        StartCoroutine(ShowExitTextAndQuit());
    }

    private IEnumerator ShowExitTextAndQuit()
    {
        exitText.SetActive(true); // Çýkýþ yazýsýný göster
        yield return new WaitForSeconds(2); // 2 saniye bekle
        Application.Quit(); // Uygulamayý kapat
    }
}
