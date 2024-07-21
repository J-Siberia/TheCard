using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioClip bgm1;
    public AudioClip bgm2;
    public AudioClip bgmLose;
    public AudioClip bgmWin;

    private bool isBgm1Playing = true;

    void Start()
    {
        // ‰Šúó‘Ô‚ÅBGM1‚ğÄ¶
        bgmSource.clip = bgm1;
        bgmSource.Play();
    }

    public void ToggleBGM()
    {
        if (isBgm1Playing)
        {
            // BGM1Ä¶’†‚Ìê‡ABGM2‚ÉØ‚è‘Ö‚¦
            bgmSource.Stop();
            bgmSource.clip = bgm2;
            bgmSource.Play();
        }
        else
        {
            // BGM2Ä¶’†‚Ìê‡ABGM1‚ÉØ‚è‘Ö‚¦
            bgmSource.Stop();
            bgmSource.clip = bgm1;
            bgmSource.Play();
        }

        isBgm1Playing = !isBgm1Playing;
    }

    public void GameEnd(bool isWin)
    {
        if (isWin)
        {
            bgmSource.Stop();
            bgmSource.clip = bgmWin;
            bgmSource.Play();
        }
        else
        {
            bgmSource.Stop();
            bgmSource.clip = bgmLose;
            bgmSource.Play();
        }
    }
}
