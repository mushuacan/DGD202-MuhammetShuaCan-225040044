using UnityEngine;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;

public class ItemScript : MonoBehaviour
{
    public float colorChangeTime1 = 3f; // Rengin dönüþüm süresi
    public float colorChangeTime2 = 5f; // Soluklaþma süresi
    public float colorChangeTime3 = 2f; // Soluklaþma süresi
    public float lifeTime = 5; //LifeTime diðer change colorlarý yüzdelik hale dönüþtürür. Örneðin 100 olsa lifeTime. Diðerleri 30 + 50 + 20 = 100 olur
    public Color targetColor1 = new Color(0.0f, 0.5f, 0.0f); // Koyu yeþil
    public Color targetColor2 = Color.black; // Siyah
    public Color targetColor3 = Color.black; // Siyah
    private Tween delayedTween;
    public Renderer rend;
    public bool etkilendiMi = false;

    public ParticleSystem particleSystemNotTaken;
    public ParticleSystem particleSystemTaken;

    public AudioClip[] sounds; // Çalýnacak ses dosyalarý (dizi halinde)
    private AudioSource audioSource; // AudioSource bileþeni

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        rend.material.shader = Shader.Find("Standard"); // Standard shader kullanýldýðýndan emin ol
        rend.material = new Material(rend.material); // Orijinal materyalin bir kopyasýný oluþtur
        StartColorTransition();
    }

    void StartColorTransition()
    {
        // Sararmasý
        rend.material.DOBlendableColor(targetColor1, (lifeTime/10) * colorChangeTime1).OnComplete(() =>
        {
            if (rend != null)
            {
                // Yaþarmasý
                rend.material.DOBlendableColor(targetColor2, (lifeTime / 10) * colorChangeTime2).OnComplete(() =>
                {
                    if (rend != null)
                    {
                        //Kararmasý
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

        // Particle effect oluþtur
        if (particleSystemNotTaken != null)
        {
            particleSystemNotTaken.Play();
        }

        delayedTween = DOVirtual.DelayedCall(0.2f, HideSkin);

        // Ses çal
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

        // Particle effect oluþtur
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
        //Görüntüyü kapat
        rend.enabled = !rend.enabled;
    }

    private void PlaySound()
    {
        int randomIndex = Random.Range(0, sounds.Length);
        audioSource.PlayOneShot(sounds[randomIndex]);
    }

    void OnDestroy()
    {
        // DelayedCall'ý durdurma
        delayedTween.Kill();
    }
}