using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MatchMaker : MonoBehaviour
{
    public bool isCardSelect = false;
    public bool isPlayerDiceSelect = false;
    public bool isEnemyDiceSelect = false;
    public GameObject selectedCard;
    public GameObject selectedPlayerDice;
    public GameObject selectedEnemyDice;

    public GameObject TurnManager;
    private TurnManager turnManager;
    private MatchManager matchManager;

    public GameObject Canvas;

    void Awake()
    {
        TurnManager = GameObject.Find("TurnManager");
        turnManager = TurnManager.GetComponent<TurnManager>();
        matchManager = GetComponent<MatchManager>();
        Canvas = GameObject.Find("Main Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (!turnManager.isMatchPhase)
        {
            isCardSelect = false;
            isPlayerDiceSelect = false;
            isEnemyDiceSelect = false;
        }
        else
        {
            if (isCardSelect && isPlayerDiceSelect && isEnemyDiceSelect)
            {
                PlayerSpeedDice pDice = selectedPlayerDice.GetComponent<PlayerSpeedDice>();
                GameObject useCard = Instantiate(selectedCard, new Vector2(-1000, -1000), Quaternion.identity);

                RectTransform cardRectTransform = useCard.GetComponent<RectTransform>();
                //cardRectTransform.sizeDelta = selectedCard.GetComponent<RectTransform>().sizeDelta;
                //useCard.transform.parent = Canvas.transform;
                cardRectTransform.SetParent(Canvas.transform, false);
                pDice.useCard = useCard;
                pDice.isSetCard = true;
                Destroy(selectedCard);
                isCardSelect = false;
                isPlayerDiceSelect = false;
                isEnemyDiceSelect = false;
                matchManager.manageMatch(selectedPlayerDice, selectedEnemyDice);
            }
        }
    }
}
