using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatonHead : AttackCard
{

    public void CardAwake()
    {
        bDiceList = new List<(int, int)>();
        yDiceList = new List<(int, int)>();

        bDiceList.Add((0, -1));
        bDiceList.Add((1, -1));
        bDiceList.Add((3, 0));

        yDiceList.Add((7, -5));
        yDiceList.Add((7, -5));
    }

    public void OnHit(Chara player, Chara enemy, GameObject someObject, bool isBousou, bool isMatchWin = false)
    {
        if (isBousou == false && isMatchWin == true)
        {
            deck.DrawCard(1);
        }
    }

    public void OnUse(Chara player, Chara enemy, bool isBousou)
    {
        if(isBousou == true)
        {
            deck.DrawCard(1);
        }
    }

}
