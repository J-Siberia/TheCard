using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chara : MonoBehaviour
{
    public int hitPoint;
    public int mentalPoint;
    public string charaName;
    public GameObject playerSpeedDice;
    public GameObject enemySpeedDice;

    public GameObject TurnManager;
    public TurnManager turnManager;

    void Start()
    {
        TurnManager = GameObject.Find("TurnManager");
        turnManager = TurnManager.GetComponent<TurnManager>();
    }

    public void Init()
    {

    }

    public void CalculateMental(int varMental)
    {
        mentalPoint = Mathf.Clamp(mentalPoint + varMental, -50, 50);
    }

    public void CalculateHitPoint(int varhitPoint)
    {
        TurnManager = GameObject.Find("TurnManager");
        turnManager = TurnManager.GetComponent<TurnManager>();

        hitPoint -= varhitPoint;
        if(hitPoint < 0)
        {
            if (turnManager.players.Contains(gameObject))
            {
                turnManager.players.Remove(gameObject);
                if (turnManager.players.Count <= 0)
                {
                    turnManager.GameEnd(false);
                }
            }
            else if (turnManager.enemies.Contains(gameObject))
            {
                turnManager.enemies.Remove(gameObject);
                if (turnManager.enemies.Count <= 0)
                {
                    turnManager.GameEnd(true);
                }
            }
            Destroy(gameObject);
        }
    }

    public void GenerateDice(GameObject character, bool isPlayer, int numDice)
    {
        Transform parentTransform = character.transform;
        // �e�v�f�̎q�v�f�̐����擾
        int childCount = parentTransform.childCount;
        foreach (Transform child in parentTransform)
        {
            Destroy(child.gameObject); // �q�v�f���폜
        }
        int initSpeedDicePosition = -2 * (numDice - 1);
        for (int i = 0; i < numDice; i++)
        {
            if (isPlayer)
            {
                GameObject newSpeedDice = Instantiate(playerSpeedDice, new Vector3(initSpeedDicePosition + 4 * i, 7, 0), Quaternion.identity);
                newSpeedDice.transform.SetParent(character.transform, false);

                ClickDice diceComponent = newSpeedDice.GetComponent<ClickDice>();
                diceComponent.originalPosition = newSpeedDice.transform.position;

                PlayerSpeedDice myScriptComponent = newSpeedDice.GetComponent<PlayerSpeedDice>();
                if (myScriptComponent != null)
                {
                    // �Ώۂ̃��\�b�h���Ăяo��
                    myScriptComponent.TurnStart();
                }
            }
            else
            {
                GameObject newSpeedDice = Instantiate(enemySpeedDice, new Vector3(initSpeedDicePosition + 4 * i, 6.75f, 0), Quaternion.identity);
                newSpeedDice.transform.SetParent(character.transform, false);

                ClickDice diceComponent = newSpeedDice.GetComponent<ClickDice>();
                diceComponent.originalPosition = newSpeedDice.transform.position;

                EnemySpeedDice myScriptComponent = newSpeedDice.GetComponent<EnemySpeedDice>();
                if (myScriptComponent != null)
                {
                    // �Ώۂ̃��\�b�h���Ăяo��
                    myScriptComponent.TurnStart();
                }
            }
            
        }
    }
}
