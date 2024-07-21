using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogEatDog : AttackCard
{
    public void CardAwake()
    {
        bDiceList = new List<(int, int)>();
        yDiceList = new List<(int, int)>();

        bDiceList.Add((6, 0));
        bDiceList.Add((6, 0));
        bDiceList.Add((2, -4));
        bDiceList.Add((6, -6));

        yDiceList.Add((0,-5));
        yDiceList.Add((11,-10));
    } 
}
