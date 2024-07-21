using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider hpSlider;

    public GameObject Chara;
    private Chara chara;

    void Awake()
    {
        chara = Chara.GetComponent<Chara>();
        hpSlider.maxValue = chara.hitPoint;
    }

    void Update()
    {
        hpSlider.value = chara.hitPoint;
    }
}