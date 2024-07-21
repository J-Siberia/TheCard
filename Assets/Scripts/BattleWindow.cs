using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleWindow : MonoBehaviour
{
    public GameObject battleWindow;
    //public List<MatchData> dataList;
    //public float delay = 0.75f;

    private GameObject playerObject;
    private GameObject enemyObject;
    private Chara player;
    private Chara enemy;
    private string damage;
    private Vector3 initPosition;

    public GameObject textMeshProPrefab; // TextMeshProのプレハブ
    public Transform Phase;

    void Awake()
    {
        Phase = transform.Find("Phase");
    }

    void Update()
    {

    }

    public void DisplayResult(MatchData data, List<int> pDiceIntList, List<int> eDiceIntList, int pSum, int eSum)
    {
        foreach (Transform child in Phase)
        {
            Destroy(child.gameObject);
        }

        if (data.playerSpeedDice != null)
        {
            playerObject = data.playerSpeedDice.transform.parent.gameObject;
            player = playerObject.GetComponent<Chara>();
        }
        else
        {
            playerObject = null;
        }
        
        if (data.enemySpeedDice != null)
        {
            enemyObject = data.enemySpeedDice.transform.parent.gameObject;
            enemy = enemyObject.GetComponent<Chara>();
        }
        else
        {
            enemyObject = null;
        }
        
        if (data.matchStatus == 0)
        {
            GameObject playerCard = Instantiate(data.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard, new Vector3(-800, 55, 0), Quaternion.identity);
            playerCard.transform.SetParent(Phase, false);
            playerCard.GetComponent<RectTransform>().sizeDelta = new Vector2(210, 300);
            playerCard.GetComponent<RectTransform>().localScale = new Vector3(2.2f, 2.2f, 1);
            GameObject enemyCard = Instantiate(data.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard, new Vector3(800, 55, 0), Quaternion.identity);
            enemyCard.transform.SetParent(Phase, false);
            enemyCard.GetComponent<RectTransform>().localScale = new Vector3(2.2f, 2.2f, 1);

            for (int i = 0; i < pDiceIntList.Count; i++)
            {
                GameObject newText = Instantiate(textMeshProPrefab, Phase);    // TextMeshProのプレハブをインスタンス化 
                TextMeshProUGUI textComponent = newText.GetComponent<TextMeshProUGUI>();    // テキストの内容を設定
                textComponent.text = pDiceIntList[i].ToString();
                newText.GetComponent<RectTransform>().anchoredPosition = new Vector3(-500, 250 - 100 * (i - 1), 0);
                newText.GetComponent<RectTransform>().localScale = new Vector3(1.4f, 1.4f, 1);        // テキストの位置とスケールを設定
            }
        
            for (int j = 0; j < eDiceIntList.Count; j++)
            {
                GameObject newText = Instantiate(textMeshProPrefab, Phase);    // TextMeshProのプレハブをインスタンス化 
                TextMeshProUGUI textComponent = newText.GetComponent<TextMeshProUGUI>();    // テキストの内容を設定
                textComponent.text = eDiceIntList[j].ToString();
                newText.GetComponent<RectTransform>().anchoredPosition = new Vector3(280, 250 - 100 * (j - 1), 0);
                newText.GetComponent<RectTransform>().localScale = new Vector3(1.4f, 1.4f, 1);        // テキストの位置とスケールを設定
            }

            GameObject pSumText = Instantiate(textMeshProPrefab, Phase);    // TextMeshProのプレハブをインスタンス化 
            TextMeshProUGUI pSumTextComponent = pSumText.GetComponent<TextMeshProUGUI>();    // テキストの内容を設定
            pSumTextComponent.text = pSum.ToString();
            pSumText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-570, -180);
            pSumText.GetComponent<RectTransform>().localScale = new Vector3(2.5f, 2.5f, 1);        // テキストの位置とスケールを設定

            GameObject eSumText = Instantiate(textMeshProPrefab, Phase);    // TextMeshProのプレハブをインスタンス化 
            TextMeshProUGUI eSumTextComponent = eSumText.GetComponent<TextMeshProUGUI>();    // テキストの内容を設定
            eSumTextComponent.text = eSum.ToString();
            eSumText.GetComponent<RectTransform>().anchoredPosition = new Vector2(130, -180);
            eSumText.GetComponent<RectTransform>().localScale = new Vector3(2.5f, 2.5f, 1);        // テキストの位置とスケールを設定

        }
        else if(data.matchStatus == 1)
        {
            GameObject playerCard = Instantiate(data.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard, new Vector3(-800, 55, 0), Quaternion.identity);
            playerCard.transform.SetParent(Phase, false);
            playerCard.GetComponent<RectTransform>().sizeDelta = new Vector2(210, 300);
            playerCard.GetComponent<RectTransform>().localScale = new Vector3(2.2f, 2.2f, 1);

            for (int i = 0; i < pDiceIntList.Count; i++)
            {
                GameObject newText = Instantiate(textMeshProPrefab, Phase);    // TextMeshProのプレハブをインスタンス化 
                TextMeshProUGUI textComponent = newText.GetComponent<TextMeshProUGUI>();    // テキストの内容を設定
                textComponent.text = pDiceIntList[i].ToString();
                newText.GetComponent<RectTransform>().anchoredPosition = new Vector3(-500, 250 - 100 * (i - 1), 0);
                newText.GetComponent<RectTransform>().localScale = new Vector3(1.4f, 1.4f, 1);        // テキストの位置とスケールを設定
            }

            GameObject pSumText = Instantiate(textMeshProPrefab, Phase);    // TextMeshProのプレハブをインスタンス化 
            TextMeshProUGUI pSumTextComponent = pSumText.GetComponent<TextMeshProUGUI>();    // テキストの内容を設定
            pSumTextComponent.text = pSum.ToString();
            pSumText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-570, -180);
            pSumText.GetComponent<RectTransform>().localScale = new Vector3(2.5f, 2.5f, 1);        // テキストの位置とスケールを設定
        }
        else
        {
            GameObject enemyCard = Instantiate(data.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard, new Vector3(800, 55, 0), Quaternion.identity);
            enemyCard.transform.SetParent(Phase, false);
            enemyCard.GetComponent<RectTransform>().localScale = new Vector3(2.2f, 2.2f, 1);

            for (int j = 0; j < eDiceIntList.Count; j++)
            {
                GameObject newText = Instantiate(textMeshProPrefab, Phase);    // TextMeshProのプレハブをインスタンス化 
                TextMeshProUGUI textComponent = newText.GetComponent<TextMeshProUGUI>();    // テキストの内容を設定
                textComponent.text = eDiceIntList[j].ToString();
                newText.GetComponent<RectTransform>().anchoredPosition = new Vector3(280, 250 - 100 * (j - 1), 0);
                newText.GetComponent<RectTransform>().localScale = new Vector3(1.4f, 1.4f, 1);        // テキストの位置とスケールを設定
            }

            GameObject eSumText = Instantiate(textMeshProPrefab, Phase);    // TextMeshProのプレハブをインスタンス化 
            TextMeshProUGUI eSumTextComponent = eSumText.GetComponent<TextMeshProUGUI>();    // テキストの内容を設定
            eSumTextComponent.text = eSum.ToString();
            eSumText.GetComponent<RectTransform>().anchoredPosition = new Vector2(130, -180);
            eSumText.GetComponent<RectTransform>().localScale = new Vector3(2.5f, 2.5f, 1);        // テキストの位置とスケールを設定
        }
    }
}
