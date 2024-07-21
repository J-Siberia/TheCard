using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpeedDice : SpeedDice
{
    public GameObject card1;    
    public GameObject card2;   
    public GameObject card3;    
    public GameObject card4;
    public GameObject card5;

    public bool isSlime = false;

    void Awake()
    {
        base.Init();
        cards.Add(card1);
        //cards.Add(card2);
        cards.Add(card3);
        cards.Add(card4);

        if (isSlime)
        {
            for (int i = 0; i < turnManager.slimeMimic; i++)
            {
                cards.Add(card5);
            }
        }
        
    }

    public void AddCard(GameObject card)
    {
        cards.Add(card);
    }

    public void TurnStart()
    {
        textMeshProText = GetComponentInChildren<TextMeshPro>();
        mySpeed = Random.Range(0, 5 + 1);
        if (textMeshProText != null)
        {
            // テキストを変更
            textMeshProText.text = mySpeed.ToString();
        }
        else
        {
            Debug.LogWarning("TextMeshProテキストが見つかりませんでした。");
        }

        if (base.IsBousou())
        {
            spriteRenderer.color = spriteRenderer.color * colorBousou;
        }
        else
        {
            spriteRenderer.color = spriteRenderer.color * colorYuutsu;
        }
        useCard = cards[Random.Range(0, cards.Count)];
    }

    public void OnHoverEnter()
    {
        if (useCard != null)
        {
            zoomCard = Instantiate(useCard, new Vector2(Input.mousePosition.x - 1300, Input.mousePosition.y - 440), Quaternion.identity);
            zoomCard.transform.SetParent(Canvas.transform, false);
            zoomCard.layer = LayerMask.NameToLayer("Zoom");

            RectTransform rect = zoomCard.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(350, 500);
            if (zoomCard != null)
            {
                Vector3 objectPosition = zoomCard.transform.position;
            }
        }
    }
}
