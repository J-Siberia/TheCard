using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSpeedDice : SpeedDice
{
    public bool isSetCard = false;

    void Awake()
    {
        base.Init();
    }

    void Update()
    {
        if (!turnManager.isPanic && turnManager.isMatchPhase)
        {
            boxCollider.enabled = true;
        }
        else
        {
            boxCollider.enabled = false;
        }
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

        useCard = null;

    }

    public void OnHoverEnter()
    {
        if (useCard != null)
        {
            zoomCard = Instantiate(useCard, new Vector2(Input.mousePosition.x, Input.mousePosition.y - 1100), Quaternion.identity);
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
