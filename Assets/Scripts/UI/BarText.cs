using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPText : MonoBehaviour
{
    public Text textComponent; // �e�L�X�g��\������UI Text�R���|�[�l���g
    public GameObject Chara;
    private Chara chara;

    void Awake()
    {
        chara = Chara.GetComponent<Chara>();
    }

    void Update()
    {
        string hpText = chara.hitPoint.ToString();
        //textComponent.text = $"{chara.hitPoint}"; // �V�����e�L�X�g�ɍX�V����l��ݒ�
    }
}