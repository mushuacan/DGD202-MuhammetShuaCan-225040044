using UnityEngine;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Pool;

public class ItemScript : MonoBehaviour
{
    public float colorChangeTime1 = 3f; // Rengin d�n���m s�resi
    public float colorChangeTime2 = 5f; // Solukla�ma s�resi
    public float colorChangeTime3 = 2f; // Solukla�ma s�resi
    public float lifeTime = 5; //LifeTime di�er change colorlar� y�zdelik hale d�n��t�r�r. �rne�in 100 olsa lifeTime. Di�erleri 30 + 50 + 20 = 100 olur

    public Color BaseColor;
    public Color targetColor1;
    public Color targetColor2;
    public Color targetColor3;
    private Tween colorTween;
    private Tween delayedTween;
    public Renderer rend;
    public GameObject childObject;

    public ParticleSystem particleSystemNotTaken;
    public ParticleSystem particleSystemTaken;
    public bool etkilendiMi = false;

    public AudioClip[] sounds; // �al�nacak ses dosyalar� (dizi halinde)
    private AudioSource audioSource; // AudioSource bile�eni

    private ObjectPooler objectPooler;
    private Material originalMaterial;


    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

        Renderer childRenderer = childObject.GetComponent<Renderer>();
        Material childMaterial = childRenderer.material;
        originalMaterial = childMaterial;

    }

    private void OnEnable()
    {
        rend.material.shader = Shader.Find("Standard"); // Standard shader kullan�ld���ndan emin ol
        etkilendiMi = false;
        if (rend != null) {rend.enabled = true;}
        if (objectPooler == null) {objectPooler = FindObjectOfType<ObjectPooler>();}
        //rend.material = null; // �nce mevcut materyali temizle
        //rend.material = originalMaterial; // Sonra �zg�n materyali geri y�kle
        StartColorTransition();
    }

    private void StartColorTransition()
    {
        rend.material.color = targetColor1;
        // Sararmas�
        colorTween = rend.material.DOBlendableColor(targetColor1, (lifeTime/10) * colorChangeTime1).OnComplete(() =>
        {
            if (rend != null)
            {
                // Ya�armas�
                colorTween = rend.material.DOBlendableColor(targetColor2, (lifeTime / 10) * colorChangeTime2).OnComplete(() =>
                {
                    if (rend != null)
                    {
                        //Kararmas�
                        colorTween = rend.material.DOBlendableColor(targetColor3, (lifeTime / 10) * colorChangeTime3).OnComplete(() =>
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

        PlaySound();
        
        DestroyYourSelf();
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

        DestroyYourSelf();
    }

    private void DestroyYourSelf()
    {
        delayedTween = DOVirtual.DelayedCall(0.04f, HideSkin);

        delayedTween = DOVirtual.DelayedCall(1f, () =>
        {
            objectPooler.ReturnToPool(gameObject);
        }).SetUpdate(false);
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
    private void OnDisable()
    {
        particleSystemNotTaken.DOPause();
        particleSystemTaken.DOPause();

        // DelayedCall'� durdurma
        delayedTween.Kill(); 
        colorTween.Kill();
    }
}