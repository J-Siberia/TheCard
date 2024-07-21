using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarHPText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // TextMeshPro�R���|�[�l���g�ւ̎Q��
    public GameObject Chara;
    private Chara chara;

    void Awake()
    {
        chara = Chara.GetComponent<Chara>();
        textMeshPro.text = chara.hitPoint.ToString();
    }

    void Update()
    {
        string hpText = chara.hitPoint.ToString();
        textMeshPro.text = hpText;
    }
}
