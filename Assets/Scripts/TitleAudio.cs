using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAudio : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioClip bgm1;

    void Start()
    {
        // ������Ԃ�BGM1���Đ�
        bgmSource.clip = bgm1;
        bgmSource.Play();
    }
}
