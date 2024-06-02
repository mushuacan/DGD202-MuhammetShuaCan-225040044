using UnityEngine;

//This code created with ChatGPT
public class CreditsButton : MonoBehaviour
{
    public GameObject creditsPanel; // Credits paneli veya yazýsý

    private bool isShowing = false;

    private void Awake()
    {
        creditsPanel.SetActive(isShowing); // Paneli aktif/pasif yap
    }

    public void OnCreditsButtonPressed()
    {
        isShowing = !isShowing; // Gösterme durumunu deðiþtir
        creditsPanel.SetActive(isShowing); // Paneli aktif/pasif yap
    }
}
