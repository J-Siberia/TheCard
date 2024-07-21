using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedDice : MonoBehaviour
{
    public int mySpeed;
    public GameObject useCard;
    public GameObject parentObject;

    public GameObject Canvas;
    public GameObject TurnManager;
    public GameObject zoomCard;

    public List<GameObject> cards;

    protected TextMeshPro textMeshProText; // TextMeshPro�̃e�L�X�g�R���|�[�l���g���w��
    protected BoxCollider2D boxCollider;
    protected TurnManager turnManager;

    public SpriteRenderer spriteRenderer;
    public Color colorBousou = new Color(1.0f, 0.2f, 0.2f, 1.0f); // ��Z����F(�\��)
    public Color colorYuutsu = new Color(0.2f, 0.2f, 1.0f, 1.0f); // ��Z����F(�J�T)
    public bool isMatch = false;
    public bool isBousou = false;

    private float charaMental;

    protected void Init()
    {
        cards = new List<GameObject>();
        Canvas = GameObject.Find("Main Canvas");
        TurnManager = GameObject.Find("TurnManager");
        turnManager = TurnManager.GetComponent<TurnManager>();
    }

    protected bool IsBousou()
    {
        float random = Random.value; // 0.0����1.0�̃����_���Ȓl�𐶐�

        Transform parentTransform = transform.parent;
        if (parentTransform != null)
        {
            // �e�I�u�W�F�N�g����e�X�N���v�g���擾
            Chara parentScript = parentTransform.GetComponent<Chara>();
            if (parentScript != null)
            {
                charaMental = (parentScript.mentalPoint + 50) * 0.01f;
            }
        }


        if (random < charaMental)
        {
            isBousou = true;
            return true;
        }
        else
        {
            isBousou = false;
            return false;
        }
    }

    public void OnHoverExit()
    {
        if (zoomCard)
        {
            Destroy(zoomCard);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        parentObject = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (turnManager)
        {
            if (turnManager.isMatchPhase)
            {
                boxCollider.enabled = true;
            }
            else
            {
                boxCollider.enabled = false;
            }
        }  
    }

    void Awake()
    {
        cards = new List<GameObject>();
    }
}
