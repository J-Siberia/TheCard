/*
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class DragDropDice : MatchMaker
{
    public GameObject chara;
    private bool isDragging = false;
    private bool isOverOpponentDice = false;
    public GameObject playerDice;
    public GameObject opponentDice;
    private GameObject selectedCard;
    private int opponentSpeed;
    private int playerSpeed;

    private GameObject startParent;
    private Vector2 startPosition;

    private Vector3 offset;
    private Transform parentTransform; // êeóvëfÇÃTransform

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
            transform.position = newPosition + offset;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collidedObjectTag = collision.gameObject.tag;
        if (collidedObjectTag == "EnemyDice")
        {
            isOverOpponentDice = true;
            opponentDice = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isOverOpponentDice)
        {
            isOverOpponentDice = false;
            opponentDice = null;
        }

    }

    public void StartDrag()
    {
        if (base.isSelect)
        {
            startParent = this.transform.parent.gameObject;
            startPosition = transform.position;
            // ç≈è„à Ç…à⁄ìÆ
            this.transform.SetParent(transform.root.gameObject.transform, true);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
            isDragging = true;
        }
    }

    public void EndDrag()
    {
        isDragging = false;
        if (isOverOpponentDice)
        {
            //transform.SetParent(opponentDice.transform, false);
            Debug.Log("Matched!");
            transform.position = startPosition;
            this.transform.SetParent(startParent.transform, true);
        }
        else
        {
            transform.position = startPosition;
            this.transform.SetParent(startParent.transform, true);
        }
    }
}
*/