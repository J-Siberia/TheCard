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
        
        // Step1：マッチの勝敗を判定
        // Step2：ダメージの計算
        // Step3：キャラクターの体力を計算（ダイスの親要素であるキャラクターを取得する）
        // 表示のためにUIに情報を渡す
        
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

        // 攻撃対象と攻撃元のキャラクター全てが生存している場合に限り攻撃を行う。
        if (playerObject != null && enemyObject != null)
        {
            // マッチ時
            if (matchData.matchStatus == 0)
            {
                Debug.Log("プレイヤーのダイスの処理(マッチ)");
                // プレイヤーのダイスの処理
                pComponentName = dataBase.GetComponentName(matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent<AttackCard>().cardID);   // データベースからコンポーネントの型を文字列として取得
                pComponentType = System.Type.GetType(pComponentName); // コンポーネントの型を文字列から取得(DogEatDogなど)
                if (pComponentType != null)
                {
                    pComponent = matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent(pComponentType);  // ゲームオブジェクトから指定された型のコンポネントを取得
                    pMethodInfo = pComponentType.GetMethod("CardAwake");
                    pMethodInfo.Invoke(pComponent, null);                                                   // Awake()による初期化

                    player.CalculateMental(matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent<AttackCard>().cardMental);

                    pMethodInfo = pComponentType.GetMethod("RollDice");
                    pMethodArgs = new object[] { matchData.playerSpeedDice.GetComponent<SpeedDice>().isBousou }; // メソッドの引数を含むオブジェクト配列を作成
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
                Debug.Log("プレイヤーの出目: " + playerDiceSum);

                Debug.Log("敵のダイスの処理(マッチ)");
                // 敵のダイスの処理               
                eComponentName = dataBase.GetComponentName(matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent<AttackCard>().cardID);  // データベースからコンポーネントの型を文字列として取得               
                eComponentType = System.Type.GetType(eComponentName);   // コンポーネントの型を文字列から取得
                if (eComponentType != null)
                {
                    eComponent = matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent(eComponentType);  // ゲームオブジェクトから指定された型のコンポネントを取得
                    eMethodInfo = eComponentType.GetMethod("CardAwake");
                    eMethodInfo.Invoke(eComponent, null);                                                   // Awake()による初期化

                    enemy.CalculateMental(matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent<AttackCard>().cardMental);

                    eMethodInfo = eComponentType.GetMethod("RollDice");
                    eMethodArgs = new object[] { matchData.enemySpeedDice.GetComponent<SpeedDice>().isBousou }; // メソッドの引数を含むオブジェクト配列を作成
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
                Debug.Log("敵の出目: " + enemyDiceSum);

                battleWindow.DisplayResult(matchData, playerDiceIntList, enemyDiceIntList, playerDiceSum, enemyDiceSum);

                // カード使用時の効果を適用
                if (pComponentType != null)
                {
                    pComponent = matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent(pComponentType);  // ゲームオブジェクトから指定された型のコンポネントを取得
                    pMethodInfo = pComponentType.GetMethod("OnUse");
                    if (pMethodInfo != null)
                    {
                        pMethodArgs = new object[] { player, enemy, matchData.playerSpeedDice.GetComponent<SpeedDice>().isBousou };
                        pMethodInfo.Invoke(pComponent, pMethodArgs);
                    }
                }
                if (eComponentType != null)
                {
                    eComponent = matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent(eComponentType);  // ゲームオブジェクトから指定された型のコンポネントを取得
                    eMethodInfo = eComponentType.GetMethod("OnUse");
                    if (eMethodInfo != null)
                    {
                        eMethodArgs = new object[] { enemy, player, matchData.enemySpeedDice.GetComponent<SpeedDice>().isBousou };
                        eMethodInfo.Invoke(eComponent, eMethodArgs);
                    }
                }

                // 的中時orマッチ勝利時の効果を適用
                if (playerDiceSum > enemyDiceSum)
                {
                    enemy.CalculateHitPoint(playerDiceSum);
                    if (pComponentType != null)
                    {
                        pComponent = matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent(pComponentType);  // ゲームオブジェクトから指定された型のコンポネントを取得
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
                        eComponent = matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent(eComponentType);  // ゲームオブジェクトから指定された型のコンポネントを取得
                        eMethodInfo = eComponentType.GetMethod("OnHit");
                        if (eMethodInfo != null)
                        {
                            eMethodArgs = new object[] { enemy, player, matchData.enemySpeedDice, matchData.enemySpeedDice.GetComponent<SpeedDice>().isBousou, true };
                            eMethodInfo.Invoke(eComponent, eMethodArgs);
                        }
                    }
                }

            }// 一方攻撃（プレイヤーから敵）
            else if (matchData.matchStatus == 1)
            {
                Debug.Log("プレイヤーのダイスの処理(一方攻撃)");
                pComponentName = dataBase.GetComponentName(matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent<AttackCard>().cardID);   // データベースからコンポーネントの型を文字列として取得               
                pComponentType = System.Type.GetType(pComponentName); // コンポーネントの型を文字列から取得(DogEatDogなど)
                if (pComponentType != null)
                {
                    pComponent = matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent(pComponentType);  // ゲームオブジェクトから指定された型のコンポネントを取得
                    pMethodInfo = pComponentType.GetMethod("CardAwake");
                    pMethodInfo.Invoke(pComponent, null);                                                   // Awake()による初期化

                    player.CalculateMental(matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent<AttackCard>().cardMental);

                    pMethodInfo = pComponentType.GetMethod("RollDice");
                    pMethodArgs = new object[] { matchData.playerSpeedDice.GetComponent<SpeedDice>().isBousou }; // メソッドの引数を含むオブジェクト配列を作成
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

                // カード使用時の効果を適用
                if (pComponentType != null)
                {
                    pComponent = matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent(pComponentType);  // ゲームオブジェクトから指定された型のコンポネントを取得
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
                    pComponent = matchData.playerSpeedDice.GetComponent<PlayerSpeedDice>().useCard.GetComponent(pComponentType);  // ゲームオブジェクトから指定された型のコンポネントを取得
                    pMethodInfo = pComponentType.GetMethod("OnHit");
                    if (pMethodInfo != null)
                    {
                        pMethodArgs = new object[] { player, enemy, matchData.playerSpeedDice, matchData.playerSpeedDice.GetComponent<SpeedDice>().isBousou, false };
                        pMethodInfo.Invoke(pComponent, pMethodArgs);
                    }
                }

            }// 一方攻撃（敵からプレイヤー）
            else
            {
                Debug.Log("敵のダイスの処理(一方攻撃)");
                eComponentName = dataBase.GetComponentName(matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent<AttackCard>().cardID);                
                eComponentType = System.Type.GetType(eComponentName);   // コンポーネントの型を文字列から取得
                if (eComponentType != null)
                {
                    eComponent = matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent(eComponentType);  // ゲームオブジェクトから指定された型のコンポネントを取得
                    eMethodInfo = eComponentType.GetMethod("CardAwake");
                    eMethodInfo.Invoke(eComponent, null);                                                   // Awake()による初期化

                    enemy.CalculateMental(matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent<AttackCard>().cardMental);

                    eMethodInfo = eComponentType.GetMethod("RollDice");
                    eMethodArgs = new object[] { matchData.enemySpeedDice.GetComponent<SpeedDice>().isBousou }; // メソッドの引数を含むオブジェクト配列を作成
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

                // カード使用時の効果を適用
                if (eComponentType != null)
                {
                    eComponent = matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent(eComponentType);  // ゲームオブジェクトから指定された型のコンポネントを取得
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
                    eComponent = matchData.enemySpeedDice.GetComponent<EnemySpeedDice>().useCard.GetComponent(eComponentType);  // ゲームオブジェクトから指定された型のコンポネントを取得
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
