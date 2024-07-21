using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPText : MonoBehaviour
{
    public Text textComponent; // テキストを表示するUI Textコンポーネント
    public GameObject Chara;
    private Chara chara;

    void Awake()
    {
        chara = Chara.GetComponent<Chara>();
    }

    void Update()
    {
        string hpText = chara.hitPoint.ToString();
        //textComponent.text = $"{chara.hitPoint}"; // 新しいテキストに更新する値を設定
    }
}