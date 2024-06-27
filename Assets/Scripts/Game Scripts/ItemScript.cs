using UnityEngine;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;

public class ItemScript : MonoBehaviour
{
    public float colorChangeTime1 = 3f; // Rengin dönüþüm süresi
    public float colorChangeTime2 = 5f; // Soluklaþma süresi
    public float colorChangeTime3 = 2f; // Soluklaþma süresi
    public float lifeTime = 5;
    public Color targetColor1 = new Color(0.0f, 0.5f, 0.0f); // Koyu yeþil
    public Color targetColor2 = Color.black; // Siyah
    public Color targetColor3 = Color.black; // Siyah
    private Tween colorTween;
    public Renderer rend;

    public GameObject particleEffectPrefab; // Particle effect prefab'i bu deðiþkene atayýn

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
        colorTween = rend.material.DOBlendableColor(targetColor1, (lifeTime/10) * colorChangeTime1).OnComplete(() =>
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
        // Particle effect oluþtur
        if (particleEffectPrefab != null)
        {
            Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        }

        // Objeyi aþaðý taþý
        transform.position = new Vector3(transform.position.x, transform.position.y - 4, transform.position.z);

        // Ses çal
        PlaySound();
    }

    private void PlaySound()
    {
        int randomIndex = Random.Range(0, sounds.Length);
        audioSource.PlayOneShot(sounds[randomIndex]);
    }
    void OnDestroy()
    {
        // Tween'leri objeyi yok etmeden önce durdur
        if (colorTween != null)
        {
            colorTween.Kill();
        }
    }
}