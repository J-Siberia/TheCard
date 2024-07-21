using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prepared : AttackCard
{

    public void CardAwake()
    {
        bDiceList = new List<(int, int)>();
        yDiceList = new List<(int, int)>();

        bDiceList.Add((10, 5));
        bDiceList.Add((10, -12));
        bDiceList.Add((5, -8));

        yDiceList.Add((-2, -7));
    }

    public void OnUse(Chara player, Chara enemy, bool isBousou)
    {
        if (isBousou == false)
        {
            deck.DrawCard(1);
        }
    }

}
