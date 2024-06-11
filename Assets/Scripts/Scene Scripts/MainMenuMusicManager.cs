using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuMusicManager : MonoBehaviour
{
    public AudioMixer audioMixer;  // Audio Mixer referansý
    // Start is called before the first frame update
    void Start()
    {
        float volume = PlayerPrefs.GetFloat("Music", 0.34f);
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
