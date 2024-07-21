using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanTaste : AttackCard
{
    public void CardAwake()
    {
        bDiceList = new List<(int, int)>();
        yDiceList = new List<(int, int)>();

        bDiceList.Add((2, 2));
        bDiceList.Add((5, 0));

        yDiceList.Add((10, 0));
        yDiceList.Add((5, 3));
    }

    public void OnHit(Chara player, Chara enemy, GameObject someObject, bool isBousou, bool isMatchWin = false)
    {
        if (isBousou == true && isMatchWin == true)
        {
            enemy.CalculateMental(3);
        }
    }
}