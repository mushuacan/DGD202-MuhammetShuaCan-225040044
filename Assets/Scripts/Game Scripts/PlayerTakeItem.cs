using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTakeItem : MonoBehaviour
{
    public TextMeshProUGUI collectedText; // UI Text eleman�
    private int collectedCount = 0; // Toplanan nesne sayac�

    public PlayerController playerController;

    public GameObject particleEffectPrefab; // Particle effect prefab'i bu de�i�kene atay�n

    public AudioClip[] sounds; // �al�nacak ses dosyalar� (dizi halinde)
    private AudioSource audioSource; // AudioSource bile�eni

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
        // Ba�lang��ta toplama sayac�n� g�ncelle
        UpdateUICollectedItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("interactable"))
        {
            // Toplanan nesneyi yok et
            Destroy(other.gameObject);

            playerController.moveSpeedBonus = 5f;
            PlaySound();
            ActivateParticleEffect(other);

            // Saya� de�erini art�r
            collectedCount++;

            // UI'y� g�ncelle
            UpdateUICollectedItem();
        }
    }

    private void UpdateUICollectedItem()
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
