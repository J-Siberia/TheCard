using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instinct : AttackCard
{

    public void CardAwake()
    {
        bDiceList = new List<(int, int)>();
        yDiceList = new List<(int, int)>();

        bDiceList.Add((6, 0));
        bDiceList.Add((2, -2));
        bDiceList.Add((2, -2));

        yDiceList.Add((0, -5));
    }

    public void OnUse(Chara player, Chara enemy, bool isBousou)
    {
        if (isBousou == false)
        {
            enemy.CalculateMental(8);
        }
    }
}
