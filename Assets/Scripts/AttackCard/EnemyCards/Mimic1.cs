using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic1 : AttackCard
{
    public GameObject perfectMimic;
    public GameObject TurnManager;
    protected TurnManager turnManager;

    public void CardAwake()
    {
        bDiceList = new List<(int, int)>();
        yDiceList = new List<(int, int)>();

        bDiceList.Add((10, 10));
        bDiceList.Add((-10, -10));
        bDiceList.Add((-12, -13));

        yDiceList.Add((-11, -13));
    }

    public void OnHit(Chara player, Chara enemy, GameObject someObject, bool isBousou, bool isMatchWin = false)
    {
        TurnManager = GameObject.Find("TurnManager");
        turnManager = TurnManager.GetComponent<TurnManager>();
        turnManager.slimeMimic++;
    }
}