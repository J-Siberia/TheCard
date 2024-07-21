using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BePulled : AttackCard
{
    public void CardAwake()
    {
        bDiceList = new List<(int, int)>();
        yDiceList = new List<(int, int)>();

        bDiceList.Add((4, -4));
        bDiceList.Add((2, 0));
        bDiceList.Add((2, 0));

        yDiceList.Add((4, 0));
    }

    public void OnHit(Chara player, Chara enemy, GameObject someObject, bool isBousou, bool isMatchWin = false)
    {
        if (isBousou == false)
        {
            enemy.CalculateMental(6);
        }
        else if(isBousou == true && isMatchWin == true)
        {
            enemy.CalculateMental(2);
        }
    }
}