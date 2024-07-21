using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAudio : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioClip bgm1;

    void Start()
    {
        // ‰Šúó‘Ô‚ÅBGM1‚ğÄ¶
        bgmSource.clip = bgm1;
        bgmSource.Play();
    }
}
