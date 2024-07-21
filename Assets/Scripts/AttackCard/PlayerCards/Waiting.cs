using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiting : AttackCard
{

    public void CardAwake()
    {
        bDiceList = new List<(int, int)>();
        yDiceList = new List<(int, int)>();
        bDiceList.Add((9, -5));
        bDiceList.Add((9, -5));
        bDiceList.Add((9, -5));

        yDiceList.Add((2, 2));
    }

    public void OnHit(Chara player, Chara enemy, GameObject someObject, bool isBousou, bool isMatchWin = false)
    {
        if(isBousou == false)
        {
            deck.DrawCard(2);
        }
    }

    public void OnUse(Chara player, Chara enemy, bool isBousou)
    {
        if (isBousou == true)
        {
            player.CalculateMental(-10);
        }
    }

}
