using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmotionPanic : MonoBehaviour
{
    public Image image; // Imageコンポーネント
    public TextMeshProUGUI textMeshPro; // TextMeshProコンポーネント
    public float blinkSpeed = 1.0f; // 点滅速度
    public float minAlpha = 0.0f; // 最小透明度
    public float maxAlpha = 1.0f; // 最大透明度

    private bool isIncreasingAlpha = true; // 透明度が増加中かどうか
    private Color imageColor;
    private Color textMeshProColor;

    private void Start()
    {
        // 初期色を設定
        imageColor = image.color;
        textMeshProColor = textMeshPro.color;
    }

    private void Update()
    {
        // 透明度を滑らかに変更
        float lerpTime = Mathf.PingPong(Time.time * blinkSpeed, 1);
        float lerpedAlpha = Mathf.Lerp(minAlpha, maxAlpha, lerpTime);

        if (isIncreasingAlpha)
        {
            imageColor.a = lerpedAlpha;
            textMeshProColor.a = lerpedAlpha;
        }
        else
        {
            imageColor.a = 1 - lerpedAlpha;
            textMeshProColor.a = 1 - lerpedAlpha;
        }

        image.color = imageColor;
        textMeshPro.color = textMeshProColor;
    }
}
