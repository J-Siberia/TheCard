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
            case 10000:             // 待ちわびる
                return "Waiting";
            case 10001:             // 羊頭狗肉
                return "MatonHead";
            case 10002:             // 本能
                return "Instinct";
            case 10003:             // 犬の共食い
                return "DogEatDog";
            case 10004:             // 腹をくくる
                return "Prepared";
            case 10005:             // まどろむ
                return "Sleeping";
            case 20000:             // 舞踏
                return "Dance";
            case 20001:             // 吸い寄せられる
                return "BePulled";
            case 20002:             // 狐雨
                return "SunShower";
            case 30000:             // 拙い模倣
                return "Mimic1";
            case 30001:             // 完全な模倣
                return "Mimic2";
            case 30002:             // 果てない徘徊
                return "Slime1";
            case 30003:             // 人間の味
                return "Slime2";
            default:
                return null;
        }
    }
}
