using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTakeItem : MonoBehaviour
{
    public TextMeshProUGUI collectedText; // UI Text elemaný
    public int collectedCount = 0; // Toplanan nesne sayacý

    public PlayerController playerController;
    public SnakeGrowUp snakeBody;

    public AudioClip[] sounds; // Çalýnacak ses dosyalarý (dizi halinde)
    private AudioSource audioSource; // AudioSource bileþeni

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
        // Baþlangýçta toplama sayacýný güncelle
        UpdateUICollectedItem();
    }

    private void Update()
    {
        //Hile kodu
        if (Input.GetKeyDown(KeyCode.F4))
        {
            CollectedCountIncrease(4);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("interactable"))
        {
            if (!other.GetComponent<ItemScript>().etkilendiMi)
            {
                playerController.moveSpeedBonus = 5f;

                CollectedCountIncrease();

                other.GetComponent<ItemScript>().PlayerTakedItem();
                PlaySound();
            }
        }
    }

    public void CollectedCountIncrease(int howMuch = 1)
    {
        // Sayaç deðerini artýr
        collectedCount += howMuch;
        UpdateUICollectedItem();
        snakeBody.Collect();

    }

    private void UpdateUICollectedItem()
    {
        // UI Text elemanýndaki metni güncelle
        collectedText.text = "Collected: " + collectedCount;
    }
    private void PlaySound()
    {
        int randomIndex = Random.Range(0, sounds.Length);
        audioSource.PlayOneShot(sounds[randomIndex]);
    }
}
