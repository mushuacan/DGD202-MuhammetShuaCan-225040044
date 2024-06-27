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
    private AudioClip previousMusic;
    private float musicOffset;

    void Start()
    {
        // Belirli tarihte �zel m�zik �alma kontrol�
        DateTime today = DateTime.Today;
        if (today.Month == 6 && today.Day == 28)
        {
            // Belirli tarihte �alacak �zel m�zik 
            musicSource.clip = musics[0];
            musicSource.Play();
            return;
        }

        // �lk defa m� �al��t�r�l�yor kontrol�
        if (PlayerPrefs.GetInt("FirstRun", 1) == 1)
        {
            // �lk defa �al��t�r�l�yorsa �zel m�zi�i �al
            musicSource.clip = musics[1];
            musicSource.Play();

            // �lk �al��t�rma i�aretini kald�r
            PlayerPrefs.SetInt("FirstRun", 0);
            PlayerPrefs.Save();
            return;
        }

        // M�zik indeks listesini olu�tur ve kar��t�r
        InitializeMusicIndices();

        // �lk m�zi�i �al
        PlayNextMusic();

    }

    void InitializeMusicIndices()
    {
        musicIndices = new List<int>();

        // 1'den 12'ye kadar indeksleri ekle (13 dahil de�il)
        for (int i = 1; i < 12; i++)
        {
            musicIndices.Add(i);
        }

        // Listede indeksleri kar��t�r
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
            Debug.Log("Now playing: " + musicSource.clip.name + ", Length: " + musicSource.clip.length + " seconds");

        }
        else
        {
            // B�t�n m�zikler �al�nd�, tekrar ba�a d�n ve kar��t�r
            InitializeMusicIndices();
            PlayNextMusic();
        }
    }

    public void PauseGame()
    {
        musicOffset = musicSource.time;

        // �alan m�zi�i durdur
        previousMusic = musicSource.clip;
        musicSource.Stop();

        // Men� m�zi�ini �al
        musicSource.clip = musics[14]; //Men� m�zi�i eklenecek
        musicSource.time = 1f;
        musicSource.Play();
        Debug.Log("Durdurulan m�zik ve saniyesi: " + previousMusic + ", " + musicOffset);
    }
    public void ResumeGame()
    {
        musicSource.clip = previousMusic;
        musicSource.time = musicOffset; // Kaydedilmi� offset de�eri ile m�zi�i ba�lat
        Debug.Log("Ba�lat�lacak m�zik ve saniyesi " + previousMusic + ", " + musicOffset);
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
            musicSource.clip = musics[13];
        }
        else
        {
            musicSource.clip = musics[14];
        }
    }
}
