using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;  // Audio Mixer referansý
    public Slider musicSlider;     // Müzik ses seviyesini kontrol eden slider
    public Slider sfxSlider;       // SFX ses seviyesini kontrol eden slider

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Music");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX");

        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMusicVolume()
    {
        float valume = musicSlider.value;
        PlayerPrefs.SetFloat("Music", valume);
        audioMixer.SetFloat("Music", Mathf.Log10(valume) *20);
        Debug.Log("music value is: " + valume);
    }

    public void SetSFXVolume()
    {
        float valume = sfxSlider.value;
        PlayerPrefs.SetFloat("SFX", valume);
        audioMixer.SetFloat("SFX", Mathf.Log10(valume) * 20);
        Debug.Log("sfx value is: " + valume);
    }
}

