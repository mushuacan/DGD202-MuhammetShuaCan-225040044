using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip[] musics;

    void Start()
    {
        // Ba�lang��ta m�zi�i �al
        musicSource.clip = musics[0];
        musicSource.Play();
    }

    public void EndGame()
    {
        // Oyun bitti�inde m�zi�i durdur
        musicSource.Stop();

        // Yeni m�zi�i ba�lat
        if (musics != null)
        {
            musicSource.Play();
        }
    }

    public void ChangeMusicClip(bool won)
    {
        if (won)
        {
            musicSource.clip = musics[1];
        }
        else
        {
             musicSource.clip= musics[2];
        }
    }
}
