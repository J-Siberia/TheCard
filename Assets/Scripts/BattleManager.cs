using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    //public MatchData matchData;
    //public List<MatchData> matchDataList;

    private GameObject playerObject;
    private GameObject enemyObject;

    private Chara player;
    private Chara enemy;

    public GameObject DataBase;
    public DataBase dataBase;

    public GameObject BattleWindow;
    private BattleWindow battleWindow;

    void Start()
    {
        //DataBase = GameObject.Find("DataBase");
        //Debug.Log(DataBase);
        dataBase = DataBase.GetComponent<DataBase>();
        //Debug.Log(dataBase.GetComponentName(10000));

        BattleWindow = GameObject.Find("BattleWindow");
        battleWindow = BattleWindow.GetComponent<BattleWindow>();
    }

    public void Battle(MatchData matchData)
    {
        List<int> playerDiceIntList;
        List<int> enemyDiceIntList;
        int playerDiceSum = 0;
        int enemyDiceSum = 0;
        
        string pComponentName;
        System.Type pComponentType;
        Component pComponent;
        MethodInfo pMethodInfo;
        object[] pMethodArgs;
        object pResultObject;

        string eComponentName;
        System.Type eComponentType;
        Component eComponent;
        MethodInfo eMethodInfo;
        object[] eMethodArgs;
        object eResultObject;

        playerDiceIntList = new List<int>();
        enemyDiceIntList = new List<int>();
        
        // Step1�F�}�b�`�̏��s�𔻒�
        // Step2�F�_���[�W�̌v�Z
        // Step3�F�L�����N�^�[�̗̑͂��v�Z�i�_�C�X�̐e�v�f�ł���L�����N�^�[���擾����j
        // �\���̂��߂�UI�ɏ���n��
        
        if(matchData.playerSpeedDice != null)
        {
            playerObject = matchData.playerSpeedDice.transform.parent.gameObject;
            player = playerObject.GetComponent<Chara>();
        }
        else
        {
            playerObject = null;
            return;
        }
        
        if(matchData.enemySpeedDice != null)
        {
            enemyObject = matchData.enemySpeedDice.transform.parent.gameObject;
            enemy = enemyObject.GetComponent<Chara>();
        }
        else
        {
            enemyObject = null;
            return;
        }

        // �U���ΏۂƍU�����̃L�����N�^�[�S�Ă��������Ă���ꍇ�Ɍ���U�����s���B
        if (playerObject != null && enemyObject != null)
        {
            // �}�b�`��
            if (matchData.matchStatus == 0)
            {
                Debug.Log("�v���C���[�̃_�C�X�̏���(�}�b�`)");
                // �v���C���[�̃_�C�X�̏���
                pComponentName = dataBase.GetComponentName(matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent<AttackCard>().cardID);   // �f�[�^�x�[�X����R���|�[�l���g�̌^�𕶎���Ƃ��Ď擾
                pComponentType = System.Type.GetType(pComponentName); // �R���|�[�l���g�̌^�𕶎��񂩂�擾(DogEatDog�Ȃ�)
                if (pComponentType != null)
                {
                    pComponent = matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent(pComponentType);  // �Q�[���I�u�W�F�N�g����w�肳�ꂽ�^�̃R���|�l���g���擾
                    pMethodInfo = pComponentType.GetMethod("CardAwake");
                    pMethodInfo.Invoke(pComponent, null);                                                   // Awake()�ɂ�鏉����

                    player.CalculateMental(matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent<AttackCard>().cardMental);

                    pMethodInfo = pComponentType.GetMethod("RollDice");
                    pMethodArgs = new object[] { matchData.playerSpeedDice.GetComponent<SpeedDice>().isBousou }; // ���\�b�h�̈������܂ރI�u�W�F�N�g�z����쐬
                    pResultObject = pMethodInfo.Invoke(pComponent, pMethodArgs);
                    playerDiceIntList = pResultObject as List<int>;
                }
                else
                {
                    playerDiceIntList = new List<int>()
                    {
                        0
                    };
                }
                
                playerDiceSum = (playerDiceIntList.Sum() >= 0) ? playerDiceIntList.Sum() : 0;
                Debug.Log("�v���C���[�̏o��: " + playerDiceSum);

                Debug.Log("�G�̃_�C�X�̏���(�}�b�`)");
                // �G�̃_�C�X�̏���               
                eComponentName = dataBase.GetComponentName(matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent<AttackCard>().cardID);  // �f�[�^�x�[�X����R���|�[�l���g�̌^�𕶎���Ƃ��Ď擾               
                eComponentType = System.Type.GetType(eComponentName);   // �R���|�[�l���g�̌^�𕶎��񂩂�擾
                if (eComponentType != null)
                {
                    eComponent = matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent(eComponentType);  // �Q�[���I�u�W�F�N�g����w�肳�ꂽ�^�̃R���|�l���g���擾
                    eMethodInfo = eComponentType.GetMethod("CardAwake");
                    eMethodInfo.Invoke(eComponent, null);                                                   // Awake()�ɂ�鏉����

                    enemy.CalculateMental(matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent<AttackCard>().cardMental);

                    eMethodInfo = eComponentType.GetMethod("RollDice");
                    eMethodArgs = new object[] { matchData.enemySpeedDice.GetComponent<SpeedDice>().isBousou }; // ���\�b�h�̈������܂ރI�u�W�F�N�g�z����쐬
                    eResultObject = eMethodInfo.Invoke(eComponent, eMethodArgs);
                    enemyDiceIntList = eResultObject as List<int>;
                }
                else
                {
                    enemyDiceIntList = new List<int>()
                    {
                        0
                    };
                }

                enemyDiceSum = (enemyDiceIntList.Sum() >= 0) ? enemyDiceIntList.Sum() : 0;
                Debug.Log("�G�̏o��: " + enemyDiceSum);

                battleWindow.DisplayResult(matchData, playerDiceIntList, enemyDiceIntList, playerDiceSum, enemyDiceSum);

                // �J�[�h�g�p���̌��ʂ�K�p
                if (pComponentType != null)
                {
                    pComponent = matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent(pComponentType);  // �Q�[���I�u�W�F�N�g����w�肳�ꂽ�^�̃R���|�l���g���擾
                    pMethodInfo = pComponentType.GetMethod("OnUse");
                    if (pMethodInfo != null)
                    {
                        pMethodArgs = new object[] { player, enemy, matchData.playerSpeedDice.GetComponent<SpeedDice>().isBousou };
                        pMethodInfo.Invoke(pComponent, pMethodArgs);
                    }
                }
                if (eComponentType != null)
                {
                    eComponent = matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent(eComponentType);  // �Q�[���I�u�W�F�N�g����w�肳�ꂽ�^�̃R���|�l���g���擾
                    eMethodInfo = eComponentType.GetMethod("OnUse");
                    if (eMethodInfo != null)
                    {
                        eMethodArgs = new object[] { enemy, player, matchData.enemySpeedDice.GetComponent<SpeedDice>().isBousou };
                        eMethodInfo.Invoke(eComponent, eMethodArgs);
                    }
                }

                // �I����or�}�b�`�������̌��ʂ�K�p
                if (playerDiceSum > enemyDiceSum)
                {
                    enemy.CalculateHitPoint(playerDiceSum);
                    if (pComponentType != null)
                    {
                        pComponent = matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent(pComponentType);  // �Q�[���I�u�W�F�N�g����w�肳�ꂽ�^�̃R���|�l���g���擾
                        pMethodInfo = pComponentType.GetMethod("OnHit");
                        if (pMethodInfo != null)
                        {
                            pMethodArgs = new object[] { player, enemy, matchData.playerSpeedDice, matchData.playerSpeedDice.GetComponent<SpeedDice>().isBousou, true };
                            pMethodInfo.Invoke(pComponent, pMethodArgs);
                        }
                    }
                }
                else if(enemyDiceSum > playerDiceSum)
                {
                    player.CalculateHitPoint(enemyDiceSum);
                    if (eComponentType != null)
                    {
                        eComponent = matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent(eComponentType);  // �Q�[���I�u�W�F�N�g����w�肳�ꂽ�^�̃R���|�l���g���擾
                        eMethodInfo = eComponentType.GetMethod("OnHit");
                        if (eMethodInfo != null)
                        {
                            eMethodArgs = new object[] { enemy, player, matchData.enemySpeedDice, matchData.enemySpeedDice.GetComponent<SpeedDice>().isBousou, true };
                            eMethodInfo.Invoke(eComponent, eMethodArgs);
                        }
                    }
                }

            }// ����U���i�v���C���[����G�j
            else if (matchData.matchStatus == 1)
            {
                Debug.Log("�v���C���[�̃_�C�X�̏���(����U��)");
                pComponentName = dataBase.GetComponentName(matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent<AttackCard>().cardID);   // �f�[�^�x�[�X����R���|�[�l���g�̌^�𕶎���Ƃ��Ď擾               
                pComponentType = System.Type.GetType(pComponentName); // �R���|�[�l���g�̌^�𕶎��񂩂�擾(DogEatDog�Ȃ�)
                if (pComponentType != null)
                {
                    pComponent = matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent(pComponentType);  // �Q�[���I�u�W�F�N�g����w�肳�ꂽ�^�̃R���|�l���g���擾
                    pMethodInfo = pComponentType.GetMethod("CardAwake");
                    pMethodInfo.Invoke(pComponent, null);                                                   // Awake()�ɂ�鏉����

                    player.CalculateMental(matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent<AttackCard>().cardMental);

                    pMethodInfo = pComponentType.GetMethod("RollDice");
                    pMethodArgs = new object[] { matchData.playerSpeedDice.GetComponent<SpeedDice>().isBousou }; // ���\�b�h�̈������܂ރI�u�W�F�N�g�z����쐬
                    pResultObject = pMethodInfo.Invoke(pComponent, pMethodArgs);
                    playerDiceIntList = pResultObject as List<int>;
                }
                else
                {
                    playerDiceIntList = new List<int>()
                    {
                        0
                    };
                }

                Debug.Log("flag0");
                playerDiceSum = (playerDiceIntList.Sum() >= 0) ? playerDiceIntList.Sum() : 0;

                List<int> dammy = new List<int>
                {
                    0
                };
                int dammySum = 0;
                battleWindow.DisplayResult(matchData, playerDiceIntList, dammy, playerDiceSum, dammySum);

                // �J�[�h�g�p���̌��ʂ�K�p
                if (pComponentType != null)
                {
                    pComponent = matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent(pComponentType);  // �Q�[���I�u�W�F�N�g����w�肳�ꂽ�^�̃R���|�l���g���擾
                    pMethodInfo = pComponentType.GetMethod("OnUse");
                    if (pMethodInfo != null)
                    {
                        Debug.Log("flag1");
                        pMethodArgs = new object[] { player, enemy, matchData.playerSpeedDice.GetComponent<SpeedDice>().isBousou };
                        pMethodInfo.Invoke(pComponent, pMethodArgs);
                        Debug.Log("flag2");
                    }
                }

                enemy.CalculateHitPoint(playerDiceSum);

                if (pComponentType != null)
                {
                    pComponent = matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent(pComponentType);  // �Q�[���I�u�W�F�N�g����w�肳�ꂽ�^�̃R���|�l���g���擾
                    pMethodInfo = pComponentType.GetMethod("OnHit");
                    if (pMethodInfo != null)
                    {
                        pMethodArgs = new object[] { player, enemy, matchData.playerSpeedDice, matchData.playerSpeedDice.GetComponent<SpeedDice>().isBousou, false };
                        pMethodInfo.Invoke(pComponent, pMethodArgs);
                    }
                }

            }// ����U���i�G����v���C���[�j
            else
            {
                Debug.Log("�G�̃_�C�X�̏���(����U��)");
                eComponentName = dataBase.GetComponentName(matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent<AttackCard>().cardID);                
                eComponentType = System.Type.GetType(eComponentName);   // �R���|�[�l���g�̌^�𕶎��񂩂�擾
                if (eComponentType != null)
                {
                    eComponent = matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent(eComponentType);  // �Q�[���I�u�W�F�N�g����w�肳�ꂽ�^�̃R���|�l���g���擾
                    eMethodInfo = eComponentType.GetMethod("CardAwake");
                    eMethodInfo.Invoke(eComponent, null);                                                   // Awake()�ɂ�鏉����

                    enemy.CalculateMental(matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent<AttackCard>().cardMental);

                    eMethodInfo = eComponentType.GetMethod("RollDice");
                    eMethodArgs = new object[] { matchData.enemySpeedDice.GetComponent<SpeedDice>().isBousou }; // ���\�b�h�̈������܂ރI�u�W�F�N�g�z����쐬
                    eResultObject = eMethodInfo.Invoke(eComponent, eMethodArgs);
                    enemyDiceIntList = eResultObject as List<int>;
                }
                else
                {
                    enemyDiceIntList = new List<int>()
                    {
                        0
                    };
                }

                enemyDiceSum = (enemyDiceIntList.Sum() >= 0) ? enemyDiceIntList.Sum() : 0;

                List<int> dammy = new List<int>
                {
                    0
                };
                int dammySum = 0;
                battleWindow.DisplayResult(matchData, dammy, enemyDiceIntList, dammySum, enemyDiceSum);

                // �J�[�h�g�p���̌��ʂ�K�p
                if (eComponentType != null)
                {
                    eComponent = matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent(eComponentType);  // �Q�[���I�u�W�F�N�g����w�肳�ꂽ�^�̃R���|�l���g���擾
                    eMethodInfo = eComponentType.GetMethod("OnUse");
                    if (eMethodInfo != null)
                    {
                        eMethodArgs = new object[] { enemy, player, matchData.enemySpeedDice.GetComponent<SpeedDice>().isBousou };
                        eMethodInfo.Invoke(eComponent, eMethodArgs);
                    }
                }

                player.CalculateHitPoint(enemyDiceSum);

                if (eComponentType != null)
                {
                    eComponent = matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent(eComponentType);  // �Q�[���I�u�W�F�N�g����w�肳�ꂽ�^�̃R���|�l���g���擾
                    eMethodInfo = eComponentType.GetMethod("OnHit");
                    if(eMethodInfo != null)
                    {
                        eMethodArgs = new object[] { enemy, player, matchData.enemySpeedDice, matchData.enemySpeedDice.GetComponent<SpeedDice>().isBousou, false };
                        eMethodInfo.Invoke(eComponent, eMethodArgs);
                    }
                }
            }
        }
    }
}
