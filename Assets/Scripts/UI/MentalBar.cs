using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MentalBar : MonoBehaviour
{
    public Slider mSlider;

    public GameObject Chara;
    private Chara chara;

    void Awake()
    {
        chara = Chara.GetComponent<Chara>();
    }

    void Update()
    {
        mSlider.value = chara.mentalPoint;
    }
}