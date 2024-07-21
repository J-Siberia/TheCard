using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roam : AttackCard
{
    public void CardAwake()
    {
        bDiceList = new List<(int, int)>();
        yDiceList = new List<(int, int)>();

        bDiceList.Add((3, -4));
        bDiceList.Add((-1, -2));

        yDiceList.Add((2, 0));
        yDiceList.Add((1, -1));
    }

    public void OnHit(Chara player, Chara enemy, GameObject someObject, bool isBousou, bool isMatchWin = false)
    {
        if (isBousou == false)
        {
            enemy.CalculateMental(5);
        }
    }
}