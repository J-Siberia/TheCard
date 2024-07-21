using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmotionPanic : MonoBehaviour
{
    public Image image; // Image�R���|�[�l���g
    public TextMeshProUGUI textMeshPro; // TextMeshPro�R���|�[�l���g
    public float blinkSpeed = 1.0f; // �_�ő��x
    public float minAlpha = 0.0f; // �ŏ������x
    public float maxAlpha = 1.0f; // �ő哧���x

    private bool isIncreasingAlpha = true; // �����x�����������ǂ���
    private Color imageColor;
    private Color textMeshProColor;

    private void Start()
    {
        // �����F��ݒ�
        imageColor = image.color;
        textMeshProColor = textMeshPro.color;
    }

    private void Update()
    {
        // �����x�����炩�ɕύX
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
