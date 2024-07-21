using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart : MonoBehaviour
{
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public GameObject playerArea;
    public GameObject enemy01;
    public GameObject enemy02;
    public GameObject enemySpeedDice;
    public TurnManager turnManager;

    List<GameObject> cards = new List<GameObject>();
    List<GameObject> enemies = new List<GameObject>();

    List<GameObject> dices = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        cards.Add(card1);
        cards.Add(card2);
        cards.Add(card3);
        enemies.Add(enemy01);
        enemies.Add(enemy02);
    }

    public void OnClick()
    {
        turnManager.StartBattle();
    }

}
