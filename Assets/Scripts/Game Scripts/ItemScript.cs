using UnityEngine;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;

public class ItemScript : MonoBehaviour
{
    public float colorChangeTime1 = 3f; // Rengin d�n���m s�resi
    public float colorChangeTime2 = 5f; // Solukla�ma s�resi
    public float colorChangeTime3 = 2f; // Solukla�ma s�resi
    public float lifeTime = 5; //LifeTime di�er change colorlar� y�zdelik hale d�n��t�r�r. �rne�in 100 olsa lifeTime. Di�erleri 30 + 50 + 20 = 100 olur
    public Color targetColor1 = new Color(0.0f, 0.5f, 0.0f); // Koyu ye�il
    public Color targetColor2 = Color.black; // Siyah
    public Color targetColor3 = Color.black; // Siyah
    private Tween delayedTween;
    public Renderer rend;
    public bool etkilendiMi = false;

    public ParticleSystem particleSystemNotTaken;
    public ParticleSystem particleSystemTaken;

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
        rend.material.DOBlendableColor(targetColor1, (lifeTime/10) * colorChangeTime1).OnComplete(() =>
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
                            }
                        });
                    }
                });
            }
        });
    }

    void ItemNotTaken_Destroy()
    {
        if (etkilendiMi)
        {
            return;
        }
        etkilendiMi = true;

        // Particle effect olu�tur
        if (particleSystemNotTaken != null)
        {
            particleSystemNotTaken.Play();
        }

        delayedTween = DOVirtual.DelayedCall(0.2f, HideSkin);

        // Ses �al
        PlaySound();

        // 3 saniye bekleyip kendini imha et
        delayedTween = DOVirtual.DelayedCall(1f, () =>
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        });
    }

    public void PlayerTakedItem()
    {
        if (etkilendiMi)
        {
            return;
        }
        etkilendiMi = true;

        // Particle effect olu�tur
        if (particleSystemTaken != null)
        {
            particleSystemTaken.Play();
        }

        delayedTween = DOVirtual.DelayedCall(0.2f, HideSkin);

        // 3 saniye bekleyip kendini imha et
        delayedTween = DOVirtual.DelayedCall(1f, () =>
        {
            Destroy(gameObject);
        });
    }

    private void HideSkin()
    {
        //G�r�nt�y� kapat
        rend.enabled = !rend.enabled;
    }

    private void PlaySound()
    {
        int randomIndex = Random.Range(0, sounds.Length);
        audioSource.PlayOneShot(sounds[randomIndex]);
    }

    void OnDestroy()
    {
        // DelayedCall'� durdurma
        delayedTween.Kill();
    }
}