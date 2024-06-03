using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTakeItem : MonoBehaviour
{
    public TextMeshProUGUI collectedText; // UI Text elemaný
    private int collectedCount = 0; // Toplanan nesne sayacý

    public GameObject particleEffectPrefab; // Particle effect prefab'i bu deðiþkene atayýn


    public AudioClip[] sounds; // Çalýnacak ses dosyalarý (dizi halinde)
    private AudioSource audioSource; // AudioSource bileþeni

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        // Baþlangýçta toplama sayacýný güncelle
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

            // Sayaç deðerini artýr
            collectedCount++;

            // UI'yý güncelle
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        // UI Text elemanýndaki metni güncelle
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
