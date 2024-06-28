using Newtonsoft.Json.Linq;
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
        musicSlider.value = PlayerPrefs.GetFloat("Music", 0.34f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 0.34f);

        SetMusicVolume();
        SetSFXVolume();
        audioMixer.SetFloat("Music for End", -80);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        PlayerPrefs.SetFloat("Music", volume);
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        Debug.Log("music value is: " + volume);
    }
    public void SetMusicVolumeForEndOfGame(float yüzdelik)
    {
        float volume = PlayerPrefs.GetFloat("Music");
        audioMixer.SetFloat("Music", Mathf.Log10(volume * (yüzdelik)) * 20 );
        audioMixer.SetFloat("Music for End", (Mathf.Log10(volume * (1 - yüzdelik)) * 20) - 7f);

    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        PlayerPrefs.SetFloat("SFX", volume);
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        Debug.Log("sfx value is: " + volume);
    }
}

