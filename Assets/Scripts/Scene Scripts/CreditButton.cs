using UnityEngine;

//This code created with ChatGPT
public class CreditsButton : MonoBehaviour
{
    public GameObject creditsPanel; // Credits paneli veya yaz�s�

    private bool isShowing = false;

    private void Awake()
    {
        creditsPanel.SetActive(isShowing); // Paneli aktif/pasif yap
    }

    public void OnCreditsButtonPressed()
    {
        isShowing = !isShowing; // G�sterme durumunu de�i�tir
        creditsPanel.SetActive(isShowing); // Paneli aktif/pasif yap
    }
}
