using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDice : MonoBehaviour
{
    public GameObject targetObject; // 点滅させたいオブジェクト
    public float blinkInterval = 0.05f; // 点滅の間隔（秒）
    private bool isBlinking = false;
    private float timer = 0f;
    public GameObject matchMaker;
    private MatchMaker matchMakerComponent;

    public Vector3 originalPosition;

    void Awake()
    {
        matchMaker = GameObject.Find("MatchManager");
        matchMakerComponent = matchMaker.GetComponent<MatchMaker>();
    }

    private void Update()
    {
        if (isBlinking)
        {
            timer += Time.deltaTime;

            if (timer >= blinkInterval)
            {
                timer = 0f;
                ToggleObjectVisibility();
            }
        }
    }

    public void StartBlinking()
    {
        isBlinking = true;
    }

    public void StopBlinking()
    {
        isBlinking = false;
        // オブジェクトの表示を元に戻す
        targetObject.SetActive(true);
    }

    private void ToggleObjectVisibility()
    {
        // オブジェクトの表示を切り替える
        targetObject.SetActive(!targetObject.activeSelf);
    }

    // ダイスをクリックした際の処理
    public void OnEnemyDiceClick()
    {

        if (matchMakerComponent.isCardSelect)
        {
            if (transform.position == originalPosition)
            {
                //matchMakerComponent.selectedEnemyDice = null;
                //
                //Vector3 newPosition = transform.position - new Vector3(0, 5.0f, 0);
                //transform.position = newPosition;
                //
                //matchMakerComponent.isEnemyDiceSelect = false;

                matchMakerComponent.selectedEnemyDice = gameObject;

                Vector3 newPosition = transform.position + new Vector3(0, 5.0f, 0);
                transform.position = newPosition;

                matchMakerComponent.isEnemyDiceSelect = true;

            }
            else
            {
                matchMakerComponent.selectedEnemyDice = gameObject;
                //
                //Vector3 newPosition = transform.position + new Vector3(0, 5.0f, 0);
                //transform.position = newPosition;
                //
                matchMakerComponent.isEnemyDiceSelect = true;
            }
        }
        else
        {
            Debug.Log("先にカードを選択してください");
        }
        
    }
    // ダイスをクリックした際の処理
    public void OnPlayerDiceClick()
    {
        if (matchMakerComponent.isCardSelect)
        {
            if (transform.position == originalPosition)
            {
                //matchMakerComponent.selectedPlayerDice = null;
                //
                //Vector3 newPosition = transform.position - new Vector3(0, 5.0f, 0);
                ////transform.position = Vector3.Lerp(transform.position, newPosition, 10.0f * Time.deltaTime);
                //transform.position = newPosition;
                //
                //matchMakerComponent.isPlayerDiceSelect = false;

                matchMakerComponent.selectedPlayerDice = gameObject;

                Vector3 newPosition = transform.position + new Vector3(0, 5.0f, 0);
                //transform.position = Vector3.Lerp(transform.position, newPosition, 10.0f * Time.deltaTime);
                transform.position = newPosition;

                matchMakerComponent.isPlayerDiceSelect = true;
            }
            else
            {
                matchMakerComponent.selectedPlayerDice = gameObject;
                //
                //Vector3 newPosition = transform.position + new Vector3(0, 5.0f, 0);
                ////transform.position = Vector3.Lerp(transform.position, newPosition, 10.0f * Time.deltaTime);
                //transform.position = newPosition;
                //
                matchMakerComponent.isPlayerDiceSelect = true;
            }   
        }
        else
        {
            Debug.Log("先にカードを選択してください");
        }
        
    }

}
