using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarMentalText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // TextMeshPro�R���|�[�l���g�ւ̎Q��
    public GameObject Chara;
    private Chara chara;

    void Awake()
    {
        chara = Chara.GetComponent<Chara>();
        textMeshPro.text = chara.mentalPoint.ToString();
    }

    void Update()
    {
        string mentalText = chara.mentalPoint.ToString();
        textMeshPro.text = mentalText;
    }
}
