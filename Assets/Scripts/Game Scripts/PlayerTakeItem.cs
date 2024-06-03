using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTakeItem : MonoBehaviour
{
    public TextMeshProUGUI collectedText; // UI Text eleman�
    private int collectedCount = 0; // Toplanan nesne sayac�

    public GameObject particleEffectPrefab; // Particle effect prefab'i bu de�i�kene atay�n


    public AudioClip[] sounds; // �al�nacak ses dosyalar� (dizi halinde)
    private AudioSource audioSource; // AudioSource bile�eni

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        // Ba�lang��ta toplama sayac�n� g�ncelle
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("interactable"))
        {
            // Toplanan nesneyi yok et
            Destroy(other.gameObject);

            PlaySound();
            ActivateParticleEffect(other);

            // Saya� de�erini art�r
            collectedCount++;

            // UI'y� g�ncelle
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        // UI Text eleman�ndaki metni g�ncelle
        collectedText.text = "Collected: " + collectedCount;
    }
    private void PlaySound()
    {
        int randomIndex = Random.Range(0, sounds.Length);
        audioSource.PlayOneShot(sounds[randomIndex]);
    }
    private void ActivateParticleEffect(Collider other)
    {
        if (particleEffectPrefab != null)
        {
            Instantiate(particleEffectPrefab, other.transform.position, Quaternion.identity);
        }
    }
}
