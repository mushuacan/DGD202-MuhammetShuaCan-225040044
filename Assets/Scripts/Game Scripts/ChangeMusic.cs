using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip[] musics;
    private List<int> musicIndices;
    private int currentMusicIndex;

    void Start()
    {
        // Belirli tarihte özel müzik çalma kontrolü
        DateTime today = DateTime.Today;
        if (today.Month == 6 && today.Day == 28)
        {
            // Belirli tarihte çalacak özel müzik 
            musicSource.clip = musics[0];
            musicSource.Play();
            return;
        }

        // Ýlk defa mý çalýþtýrýlýyor kontrolü
        if (PlayerPrefs.GetInt("FirstRun", 1) == 1)
        {
            // Ýlk defa çalýþtýrýlýyorsa özel müziði çal
            musicSource.clip = musics[1];
            musicSource.Play();

            // Ýlk çalýþtýrma iþaretini kaldýr
            PlayerPrefs.SetInt("FirstRun", 0);
            PlayerPrefs.Save();
            return;
        }

        // Müzik indeks listesini oluþtur ve karýþtýr
        InitializeMusicIndices();

        // Ýlk müziði çal
        PlayNextMusic();
    }

    void InitializeMusicIndices()
    {
        musicIndices = new List<int>();

        // 1'den 12'ye kadar indeksleri ekle (13 dahil deðil)
        for (int i = 1; i < 12; i++)
        {
            musicIndices.Add(i);
        }

        // Listede indeksleri karýþtýr
        for (int i = 0; i < musicIndices.Count; i++)
        {
            int temp = musicIndices[i];
            int randomIndex = UnityEngine.Random.Range(0, musicIndices.Count);
            musicIndices[i] = musicIndices[randomIndex];
            musicIndices[randomIndex] = temp;
        }

        currentMusicIndex = 0;
    }

    void PlayNextMusic()
    {
        if (currentMusicIndex < musicIndices.Count)
        {
            musicSource.clip = musics[musicIndices[currentMusicIndex]];
            musicSource.Play();
            currentMusicIndex++;
        }
        else
        {
            // Bütün müzikler çalýndý, tekrar baþa dön ve karýþtýr
            InitializeMusicIndices();
            PlayNextMusic();
        }
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
            musicSource.clip = musics[13];
        }
        else
        {
            musicSource.clip = musics[14];
        }
    }
}
