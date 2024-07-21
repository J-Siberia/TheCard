using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunShower : AttackCard
{
    public void CardAwake()
    {
        bDiceList = new List<(int, int)>();
        yDiceList = new List<(int, int)>();

        bDiceList.Add((1, -6));
        bDiceList.Add((2, 0));

        yDiceList.Add((1, 0));
        yDiceList.Add((2, 0));
        yDiceList.Add((4, 0));
        yDiceList.Add((4, -7));
    }

    public void OnHit(Chara player, Chara enemy, GameObject someObject, bool isBousou, bool isMatchWin = false)
    {
        if (isBousou == false && isMatchWin == true)
        {
            enemy.CalculateMental(10);
        }
    }

    public void OnUse(Chara player, Chara enemy, bool isBousou)
    {
        if (isBousou == true)
        {
            player.CalculateMental(-4);
        }
    }
}