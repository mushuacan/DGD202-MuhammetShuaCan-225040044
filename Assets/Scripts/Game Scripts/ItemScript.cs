using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour
{
    public float transitionDuration = 5f; // Rengin dönüþüm süresi
    public Color targetColor = Color.green; // Hedef renk
    public Color targetColor2 = Color.red; // Hedef renk
    public float fadeOutDuration = 1f; // Soluklaþma süresi

    public Renderer rend;
    private Color originalColor;

    public GameObject particleEffectPrefab; // Particle effect prefab'i bu deðiþkene atayýn

    public AudioClip[] sounds; // Çalýnacak ses dosyalarý (dizi halinde)
    private AudioSource audioSource; // AudioSource bileþeni

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        originalColor = rend.material.color;
        StartCoroutine(ChangeColorOverTime());
    }

    IEnumerator ChangeColorOverTime()
    {
        yield return new WaitForSeconds(transitionDuration);

        float elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            float t = elapsedTime / fadeOutDuration;
            rend.material.color = Color.Lerp(originalColor, targetColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (particleEffectPrefab != null)
        {
            Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y - 4, transform.position.z);
        PlaySound();
        yield return new WaitForSeconds(3f);
        Destroy(gameObject); // Kendini imha et
    }
    private void PlaySound()
    {
        int randomIndex = Random.Range(0, sounds.Length);
        audioSource.PlayOneShot(sounds[randomIndex]);
    }
}