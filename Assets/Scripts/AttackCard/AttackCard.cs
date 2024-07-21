using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : MonoBehaviour
{
    // max ~ min
    public List<(int, int)> bDiceList = new List<(int, int)>();
    public List<(int, int)> yDiceList = new List<(int, int)>();

    public int cardID;
    public int cardPower;
    public string cardName;
    public int cardMental;

    public GameObject Deck;
    public Deck deck;

    void Awake()
    {
        Deck = GameObject.Find("Deck");
        deck = Deck.GetComponent<Deck>();
    }

    public List<int> RollDice(bool isBousou = true)
    {
        List<int> resultList = new List<int>();
        resultList.Add(cardPower);
        if (isBousou) { Debug.Log("�\��"); } else { Debug.Log("�J�T"); }
        //Debug.Log("��b�l: " + cardPower);
        List<(int, int)> diceList = isBousou ? bDiceList : yDiceList;
        //Debug.Log("�_�C�X���X�g: " + bDiceList.Count);

        foreach ((int, int) dice in diceList)
        {
            int randDice = Random.Range(dice.Item2, dice.Item1 + 1);
            //Debug.Log("�o��: " + randDice);
            resultList.Add(randDice);
        }
        return resultList;
    }

}
