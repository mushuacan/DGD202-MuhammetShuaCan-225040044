using UnityEngine;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Pool;

public class ItemScript : MonoBehaviour
{
    public float colorChangeTime1 = 3f; // Rengin dönüþüm süresi
    public float colorChangeTime2 = 5f; // Soluklaþma süresi
    public float colorChangeTime3 = 2f; // Soluklaþma süresi
    public float lifeTime = 5; //LifeTime diðer change colorlarý yüzdelik hale dönüþtürür. Örneðin 100 olsa lifeTime. Diðerleri 30 + 50 + 20 = 100 olur

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

    public AudioClip[] sounds; // Çalýnacak ses dosyalarý (dizi halinde)
    private AudioSource audioSource; // AudioSource bileþeni

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
        rend.material.shader = Shader.Find("Standard"); // Standard shader kullanýldýðýndan emin ol
        etkilendiMi = false;
        if (rend != null) {rend.enabled = true;}
        if (objectPooler == null) {objectPooler = FindObjectOfType<ObjectPooler>();}
        //rend.material = null; // Önce mevcut materyali temizle
        //rend.material = originalMaterial; // Sonra özgün materyali geri yükle
        StartColorTransition();
    }

    private void StartColorTransition()
    {
        rend.material.color = targetColor1;
        // Sararmasý
        colorTween = rend.material.DOBlendableColor(targetColor1, (lifeTime/10) * colorChangeTime1).OnComplete(() =>
        {
            if (rend != null)
            {
                // Yaþarmasý
                colorTween = rend.material.DOBlendableColor(targetColor2, (lifeTime / 10) * colorChangeTime2).OnComplete(() =>
                {
                    if (rend != null)
                    {
                        //Kararmasý
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

        // Particle effect oluþtur
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

        // Particle effect oluþtur
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
        //Görüntüyü kapat
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

        // DelayedCall'ý durdurma
        delayedTween.Kill(); 
        colorTween.Kill();
    }
}