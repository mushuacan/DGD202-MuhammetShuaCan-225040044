using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip[] musics;

    void Start()
    {
        // Baþlangýçta müziði çal
        musicSource.clip = musics[0];
        musicSource.Play();
    }

    public void EndGame()
    {
        // Oyun bittiðinde müziði durdur
        musicSource.Stop();

        // Yeni müziði baþlat
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
