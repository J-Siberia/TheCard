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
            case 10000:             // ‘Ò‚¿‚í‚Ñ‚é
                return "Waiting";
            case 10001:             // —r“ª‹ç“÷
                return "MatonHead";
            case 10002:             // –{”\
                return "Instinct";
            case 10003:             // Œ¢‚Ì‹¤H‚¢
                return "DogEatDog";
            case 10004:             // • ‚ğ‚­‚­‚é
                return "Prepared";
            case 10005:             // ‚Ü‚Ç‚ë‚Ş
                return "Sleeping";
            case 20000:             // •‘“¥
                return "Dance";
            case 20001:             // ‹z‚¢Šñ‚¹‚ç‚ê‚é
                return "BePulled";
            case 20002:             // ŒÏ‰J
                return "SunShower";
            case 30000:             // Ù‚¢–Í•í
                return "Mimic1";
            case 30001:             // Š®‘S‚È–Í•í
                return "Mimic2";
            case 30002:             // ‰Ê‚Ä‚È‚¢œpœj
                return "Slime1";
            case 30003:             // lŠÔ‚Ì–¡
                return "Slime2";
            default:
                return null;
        }
    }
}
