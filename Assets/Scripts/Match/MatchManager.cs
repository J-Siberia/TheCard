using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MatchData
{
    public GameObject playerSpeedDice;
    public GameObject enemySpeedDice;
    public int playerSpeed;
    public int enemySpeed;
    public int matchSpeed = 0;  // �}�b�`�������߂�l�B�}�b�`�̏ꍇ�͑��x�̍������̒l�����C����U���̏ꍇ�͍U�����̑��x�̒l�����B
    public int matchStatus = 100;     // 0�Ȃ�}�b�`�C1�Ȃ�v���C���[����G�ւ̈���U���C����ȊO�i�ʏ��2�j�͓G����v���C���[�ւ̈���U��
}

public class MatchManager : MonoBehaviour
{

    public List<MatchData> matchDataList;

    public GameObject BattleManager;
    public BattleManager battleManager;

    public GameObject TurnManager;
    public TurnManager turnManager;

    void Awake()
    {
        BattleManager = GameObject.Find("BattleManager");
        battleManager = BattleManager.GetComponent<BattleManager>();

        TurnManager = GameObject.Find("TurnManager");
        turnManager = TurnManager.GetComponent<TurnManager>();

        matchDataList = turnManager.matchDataList;
    }

    public void RandomMatch(GameObject Enemy, GameObject Player)
    {
        // �e�I�u�W�F�N�g�̎q�v�f���擾
        Transform enemyTransform = Enemy.transform;
        int eChildCount = enemyTransform.childCount;

        // �q�v�f�̃��[�v����
        for (int i = 0; i < eChildCount; i++)
        {
            Transform eChildTransform = enemyTransform.GetChild(i);
            GameObject enemyDiceObject = eChildTransform.gameObject;
            EnemySpeedDice enemyDice = eChildTransform.GetComponent<EnemySpeedDice>(); // �q�v�f�����R���|�[�l���g�̎擾

            if (enemyDice != null)
            {
                // �q�v�f�̃R���|�[�l���g�������ɍ��v�����ꍇ�̏���
                if (!enemyDice.isMatch)
                {
                    // �v���C���[�̑��x�_�C�X�������_���ɑI��
                    Transform playerTransform = Player.transform;                       // �v���C���[��Tranform���擾                    
                    int pChildCount = playerTransform.childCount;                       // �v���C���[�̑��x�_�C�X�̌����擾
                    int randomIndex = Random.Range(0, pChildCount);
                    Transform pChildTransform = playerTransform.GetChild(randomIndex);  // �����_���ɑ��x�_�C�X��I�����C����Transform���擾
                    GameObject playerDiceObject = pChildTransform.gameObject;           // �����_���ɑI�񂾃v���C���[�̑��x�_�C�X��GameObject���擾

                    manageMatch(playerDiceObject, enemyDiceObject, true);               // �}�b�`����
                }
            }
        }
    }

    /// <summary>
    /// �U���Ώۂ����߂�ۂɎg�p
    /// </summary>
    /// <param name="pDice">�v���C���[�̃_�C�X</param>
    /// <param name="eDice">�G�̃_�C�X</param>
    public void manageMatch(GameObject pDice, GameObject eDice, bool isEnemy = false)
    {
        PlayerSpeedDice playerSpeedDice = pDice.GetComponent<PlayerSpeedDice>();
        EnemySpeedDice enemySpeedDice = eDice.GetComponent<EnemySpeedDice>();

        MatchData matchData = new MatchData();

        // �v���C���[���̑I��
        if (!isEnemy)
        {
            matchData.matchSpeed = playerSpeedDice.mySpeed;
            // �G�̃_�C�X���}�b�`��Ԃł͂Ȃ�
            if (!enemySpeedDice.isMatch)
            {
                // �}�b�`�����𖞂���
                if (playerSpeedDice.mySpeed > enemySpeedDice.mySpeed && enemySpeedDice.useCard != null)
                {
                    matchData.playerSpeedDice = pDice;
                    matchData.enemySpeedDice = eDice;
                    matchData.playerSpeed = playerSpeedDice.mySpeed;
                    matchData.enemySpeed = enemySpeedDice.mySpeed;                   
                    matchData.matchStatus = 0;
                    playerSpeedDice.isMatch = true;
                    enemySpeedDice.isMatch = true;
                }
                // �}�b�`�����𖞂����Ȃ�(���x������Ȃ�)
                else
                { 
                    matchData.playerSpeedDice = pDice;
                    matchData.enemySpeedDice = eDice;
                    matchData.playerSpeed = playerSpeedDice.mySpeed;
                    matchData.enemySpeed = enemySpeedDice.mySpeed;
                    matchData.matchStatus = 1;
                }
            }
            // �G�̃_�C�X���}�b�`���(������킸�Ƀv���C���[����G�ւ̈���U��)
            else
            {
                matchData.playerSpeedDice = pDice;
                matchData.enemySpeedDice = eDice;
                matchData.playerSpeed = playerSpeedDice.mySpeed;
                matchData.enemySpeed = enemySpeedDice.mySpeed;
                matchData.matchStatus = 1;
            }
        }
        // �G���̑I��
        else
        {
            matchData.matchSpeed = enemySpeedDice.mySpeed;
            // �v���C���[�̃_�C�X���}�b�`��Ԃł͂Ȃ�
            if (!playerSpeedDice.isMatch)
            {
                // �}�b�`�����𖞂���
                if (playerSpeedDice.mySpeed < enemySpeedDice.mySpeed && playerSpeedDice.useCard != null)
                {
                    matchData.playerSpeedDice = pDice;
                    matchData.enemySpeedDice = eDice;
                    matchData.playerSpeed = playerSpeedDice.mySpeed;
                    matchData.enemySpeed = enemySpeedDice.mySpeed;
                    matchData.matchStatus = 0;
                    playerSpeedDice.isMatch = true;
                    enemySpeedDice.isMatch = true;
                }
                // �}�b�`�����𖞂����Ȃ�(���x������Ȃ�)
                else
                {
                    matchData.playerSpeedDice = pDice;
                    matchData.enemySpeedDice = eDice;
                    matchData.playerSpeed = playerSpeedDice.mySpeed;
                    matchData.enemySpeed = enemySpeedDice.mySpeed;
                    matchData.matchStatus = 2;
                }
            }
            // �v���C���[�̃_�C�X���}�b�`���(������킸�ɓG����v���C���[�ւ̈���U��)
            else
            {
                matchData.playerSpeedDice = pDice;
                matchData.enemySpeedDice = eDice;
                matchData.playerSpeed = playerSpeedDice.mySpeed;
                matchData.enemySpeed = enemySpeedDice.mySpeed;
                matchData.matchStatus = 2;
            }
        }
        matchDataList.Add(matchData);
        //Debug.Log("Add: " + matchData.playerSpeedDice + ", " + matchData.enemySpeedDice + ", " + matchData.playerSpeed + ", " + matchData.enemySpeed + ", " + matchData.matchSpeed + ", " + matchData.matchStatus);
    }

    public List<MatchData> SortMatch()
    {
        matchDataList.Sort((a, b) => b.matchSpeed.CompareTo(a.matchSpeed));
        
        foreach(MatchData matchData in matchDataList)
        {
            //Debug.Log("Data: " + matchData.playerSpeedDice + ", " + matchData.enemySpeedDice + ", " + matchData.playerSpeed + ", " + matchData.enemySpeed + ", " + matchData.matchSpeed + ", " + matchData.matchStatus);
            //Debug.Log("Card; " + matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard + " v.s. " + matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard);
        }

        return matchDataList;
    }
}
