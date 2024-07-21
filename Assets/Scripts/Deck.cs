using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameObject card1;    
    public GameObject card2;    
    public GameObject card3;  
    public GameObject card4;
    public GameObject card5;    
    public GameObject card6;       

    List<GameObject> cards = new List<GameObject>();

    public GameObject playerArea;
    public GameObject Canvas;

    void Awake()
    {
        cards.Add(card1);
        cards.Add(card2);
        cards.Add(card3);
        cards.Add(card4);
        cards.Add(card5);
        cards.Add(card6);
        Canvas = GameObject.Find("Main Canvas");
    }

    public void DrawCard(int num)
    {
        Transform parentTransform = playerArea.transform;
        // 親要素の子要素の数を取得
        int childCount = parentTransform.childCount;
        if(childCount <= 10)
        {
            for (int i = 0; i < num; i++)
            {
                GameObject playerCard = Instantiate(cards[Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
                playerCard.transform.SetParent(parentTransform, false);
            }
        }
    }
}
