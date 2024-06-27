using UnityEngine;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;

public class ItemScript : MonoBehaviour
{
    public float colorChangeTime1 = 3f; // Rengin d�n���m s�resi
    public float colorChangeTime2 = 5f; // Solukla�ma s�resi
    public float colorChangeTime3 = 2f; // Solukla�ma s�resi
    public float lifeTime = 5;
    public Color targetColor1 = new Color(0.0f, 0.5f, 0.0f); // Koyu ye�il
    public Color targetColor2 = Color.black; // Siyah
    public Color targetColor3 = Color.black; // Siyah
    private Tween colorTween;
    public Renderer rend;

    public GameObject particleEffectPrefab; // Particle effect prefab'i bu de�i�kene atay�n

    public AudioClip[] sounds; // �al�nacak ses dosyalar� (dizi halinde)
    private AudioSource audioSource; // AudioSource bile�eni

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        rend.material.shader = Shader.Find("Standard"); // Standard shader kullan�ld���ndan emin ol
        rend.material = new Material(rend.material); // Orijinal materyalin bir kopyas�n� olu�tur
        StartColorTransition();
    }

    void StartColorTransition()
    {
        // Sararmas�
        colorTween = rend.material.DOBlendableColor(targetColor1, (lifeTime/10) * colorChangeTime1).OnComplete(() =>
        {
            if (rend != null)
            {
                // Ya�armas�
                rend.material.DOBlendableColor(targetColor2, (lifeTime / 10) * colorChangeTime2).OnComplete(() =>
                {
                    if (rend != null)
                    {
                        //Kararmas�
                        rend.material.DOBlendableColor(targetColor3, (lifeTime / 10) * colorChangeTime3).OnComplete(() =>
                        {
                            if (rend != null)
                            {
                                ItemNotTaken_Destroy();
                                // 3 saniye bekleyip kendini imha et
                                DOVirtual.DelayedCall(3f, () =>
                                {
                                    Destroy(gameObject);
                                });
                            }
                        });
                    }
                });
            }
        });
    }

    void ItemNotTaken_Destroy()
    {
        // Particle effect olu�tur
        if (particleEffectPrefab != null)
        {
            Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        }

        // Objeyi a�a�� ta��
        transform.position = new Vector3(transform.position.x, transform.position.y - 4, transform.position.z);

        // Ses �al
        PlaySound();
    }

    private void PlaySound()
    {
        int randomIndex = Random.Range(0, sounds.Length);
        audioSource.PlayOneShot(sounds[randomIndex]);
    }
    void OnDestroy()
    {
        // Tween'leri objeyi yok etmeden �nce durdur
        if (colorTween != null)
        {
            colorTween.Kill();
        }
    }
}