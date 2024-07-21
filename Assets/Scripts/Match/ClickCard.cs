using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickCard : MonoBehaviour
{
    public float blinkInterval = 0.15f; // �_�ł̊Ԋu�i�b�j
    public bool isBlinking = false; // �_�Œ����ǂ����𐧌�
    private float timer = 0f;
    private bool isVisible = true;
    private Graphic uiElement; // UI�v�f�̂��߂�Graphic�R���|�[�l���g
    public GameObject matchMaker;
    private MatchMaker matchMakerComponent;

    void Awake()
    {
        // ���̃X�N���v�g���A�^�b�`���ꂽUI�v�f��Graphic�R���|�[�l���g���擾
        uiElement = GetComponent<Graphic>();
        if (uiElement == null)
        {
            Debug.LogError("���̃X�N���v�g��Graphic�R���|�[�l���g������UI�v�f�ɃA�^�b�`�����K�v������܂��B");
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

                // UI�v�f�̓����x��ύX���ē_�ł𐧌�
                uiElement.color = new Color(uiElement.color.r, uiElement.color.g, uiElement.color.b, isVisible ? 1f : 0f);
            }
        }
        else
        {
            uiElement.color = new Color(uiElement.color.r, uiElement.color.g, uiElement.color.b, 1f);
        }
    }

    // �J�[�h���N���b�N�����ۂ̏���
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
