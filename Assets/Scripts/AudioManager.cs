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
        // 初期状態でBGM1を再生
        bgmSource.clip = bgm1;
        bgmSource.Play();
    }

    public void ToggleBGM()
    {
        if (isBgm1Playing)
        {
            // BGM1再生中の場合、BGM2に切り替え
            bgmSource.Stop();
            bgmSource.clip = bgm2;
            bgmSource.Play();
        }
        else
        {
            // BGM2再生中の場合、BGM1に切り替え
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
