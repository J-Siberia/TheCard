using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAudio : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioClip bgm1;

    void Start()
    {
        // 初期状態でBGM1を再生
        bgmSource.clip = bgm1;
        bgmSource.Play();
    }
}
