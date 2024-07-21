using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic2 : AttackCard
{
    public void CardAwake()
    {
        bDiceList = new List<(int, int)>();
        yDiceList = new List<(int, int)>();

        bDiceList.Add((20, 15));
        bDiceList.Add((10, -8));
        bDiceList.Add((4, 2));
        bDiceList.Add((-3, -6));

        yDiceList.Add((1, 1));
        yDiceList.Add((1, -1));
        yDiceList.Add((2, -2));
    }

    public void OnHit(Chara player, Chara enemy, GameObject someObject, bool isBousou, bool isMatchWin = false)
    {
        if(isBousou == true)
        {
            enemy.CalculateMental(-5);
        }
    }
}