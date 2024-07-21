using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleeping : AttackCard
{
    public void CardAwake()
    {
        bDiceList = new List<(int, int)>();
        yDiceList = new List<(int, int)>();

        bDiceList.Add((-1, -2));

        yDiceList.Add((2, 1));
    }

    public void OnUse(Chara player, Chara enemy, bool isBousou)
    {
        if (isBousou == true)
        {
            player.CalculateHitPoint(-2);
        }
        else
        {
            player.CalculateHitPoint(-12);
        }
    }
}
