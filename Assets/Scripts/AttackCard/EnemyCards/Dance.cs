using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dance : AttackCard
{
    public void CardAwake()
    {
        bDiceList = new List<(int, int)>();
        yDiceList = new List<(int, int)>();

        bDiceList.Add((0, -6));
        bDiceList.Add((4, -7));

        yDiceList.Add((1, 0));
    }

    public void OnHit(Chara player, Chara enemy, GameObject someObject, bool isBousou, bool isMatchWin = false)
    {
        if (isBousou == false)
        {
            player.CalculateMental(20);
        }
        else
        {
            enemy.CalculateMental(6);
        }
    }
}