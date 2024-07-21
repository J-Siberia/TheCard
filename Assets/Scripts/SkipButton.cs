using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour
{
    private GameObject TurnManager;
    private TurnManager turnManager;

    void Awake()
    {
        TurnManager = GameObject.Find("TurnManager");
        turnManager = TurnManager.GetComponent<TurnManager>();
    }

    public void OnClick()
    {
        turnManager.isSkipButton = false; // ボタンが押されたことをフラグで示す
    }
}
