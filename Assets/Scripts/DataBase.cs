using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetComponentName(int cID)
    {
        switch (cID)
        {
            case 10000:             // �҂���т�
                return "Waiting";
            case 10001:             // �r�����
                return "MatonHead";
            case 10002:             // �{�\
                return "Instinct";
            case 10003:             // ���̋��H��
                return "DogEatDog";
            case 10004:             // ����������
                return "Prepared";
            case 10005:             // �܂ǂ��
                return "Sleeping";
            case 20000:             // ����
                return "Dance";
            case 20001:             // �z���񂹂���
                return "BePulled";
            case 20002:             // �ωJ
                return "SunShower";
            case 30000:             // �ق��͕�
                return "Mimic1";
            case 30001:             // ���S�Ȗ͕�
                return "Mimic2";
            case 30002:             // �ʂĂȂ��p�j
                return "Slime1";
            case 30003:             // �l�Ԃ̖�
                return "Slime2";
            default:
                return null;
        }
    }
}
