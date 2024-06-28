using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTakeItem : MonoBehaviour
{
    public TextMeshProUGUI collectedText; // UI Text eleman�
    public int collectedCount = 0; // Toplanan nesne sayac�

    public PlayerController playerController;
    public SnakeGrowUp snakeBody;

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
        // Saya� de�erini art�r
        collectedCount += howMuch;
        UpdateUICollectedItem();
        snakeBody.Collect();

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
}
