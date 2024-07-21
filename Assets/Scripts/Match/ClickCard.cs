using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickCard : MonoBehaviour
{
    public float blinkInterval = 0.15f; // 点滅の間隔（秒）
    public bool isBlinking = false; // 点滅中かどうかを制御
    private float timer = 0f;
    private bool isVisible = true;
    private Graphic uiElement; // UI要素のためのGraphicコンポーネント
    public GameObject matchMaker;
    private MatchMaker matchMakerComponent;

    void Awake()
    {
        // このスクリプトがアタッチされたUI要素のGraphicコンポーネントを取得
        uiElement = GetComponent<Graphic>();
        if (uiElement == null)
        {
            Debug.LogError("このスクリプトはGraphicコンポーネントを持つUI要素にアタッチされる必要があります。");
        }

        matchMaker = GameObject.Find("MatchManager");
        matchMakerComponent = matchMaker.GetComponent<MatchMaker>();
    }

    void Update()
    {
        if (isBlinking)
        {
            timer += Time.deltaTime;

            if (timer >= blinkInterval)
            {
                timer = 0f;
                isVisible = !isVisible;

                // UI要素の透明度を変更して点滅を制御
                uiElement.color = new Color(uiElement.color.r, uiElement.color.g, uiElement.color.b, isVisible ? 1f : 0f);
            }
        }
        else
        {
            uiElement.color = new Color(uiElement.color.r, uiElement.color.g, uiElement.color.b, 1f);
        }
    }

    // カードをクリックした際の処理
    public void OnCardClick()
    {
        if (matchMakerComponent.isCardSelect)
        {
            matchMakerComponent.selectedCard = null;

            Vector3 newPosition = transform.position - new Vector3(0, 50.0f, 0);
            transform.position = newPosition;

            matchMakerComponent.isCardSelect = false;
            isBlinking = false;
        }
        else
        {
            matchMakerComponent.selectedCard = gameObject;

            Vector3 newPosition = transform.position + new Vector3(0, 50.0f, 0);
            transform.position = newPosition;

            matchMakerComponent.isCardSelect = true;
            //isBlinking = true;
        }
    }
}
