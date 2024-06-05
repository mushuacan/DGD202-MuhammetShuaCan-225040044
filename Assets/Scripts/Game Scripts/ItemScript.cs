using UnityEngine;
using System.Collections;
using System.Threading;

public class ItemScript : MonoBehaviour
{
    public float transitionDuration = 5f; // Rengin d�n���m s�resi
    public Color targetColor = Color.green; // Hedef renk
    public float fadeOutDuration = 1f; // Solukla�ma s�resi

    private Renderer rend;
    private Color originalColor;
    private bool isFading = false;

    public GameObject particleEffectPrefab; // Particle effect prefab'i bu de�i�kene atay�n

    public AudioClip[] sounds; // �al�nacak ses dosyalar� (dizi halinde)
    private AudioSource audioSource; // AudioSource bile�eni

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        StartCoroutine(ChangeColorOverTime());
    }

    IEnumerator ChangeColorOverTime()
    {
        yield return new WaitForSeconds(transitionDuration);

        isFading = true;
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
        transform.position = new Vector3(transform.position.x ,transform.position.y - 2, transform.position.z);
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
