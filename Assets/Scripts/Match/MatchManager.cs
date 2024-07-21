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
    public int matchSpeed = 0;  // マッチ順を決める値。マッチの場合は速度の高い方の値を取り，一方攻撃の場合は攻撃側の速度の値を取る。
    public int matchStatus = 100;     // 0ならマッチ，1ならプレイヤーから敵への一方攻撃，それ以外（通常は2）は敵からプレイヤーへの一方攻撃
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
        // 親オブジェクトの子要素を取得
        Transform enemyTransform = Enemy.transform;
        int eChildCount = enemyTransform.childCount;

        // 子要素のループ処理
        for (int i = 0; i < eChildCount; i++)
        {
            Transform eChildTransform = enemyTransform.GetChild(i);
            GameObject enemyDiceObject = eChildTransform.gameObject;
            EnemySpeedDice enemyDice = eChildTransform.GetComponent<EnemySpeedDice>(); // 子要素が持つコンポーネントの取得

            if (enemyDice != null)
            {
                // 子要素のコンポーネントが条件に合致した場合の処理
                if (!enemyDice.isMatch)
                {
                    // プレイヤーの速度ダイスをランダムに選択
                    Transform playerTransform = Player.transform;                       // プレイヤーのTranformを取得                    
                    int pChildCount = playerTransform.childCount;                       // プレイヤーの速度ダイスの個数を取得
                    int randomIndex = Random.Range(0, pChildCount);
                    Transform pChildTransform = playerTransform.GetChild(randomIndex);  // ランダムに速度ダイスを選択し，そのTransformを取得
                    GameObject playerDiceObject = pChildTransform.gameObject;           // ランダムに選んだプレイヤーの速度ダイスのGameObjectを取得

                    manageMatch(playerDiceObject, enemyDiceObject, true);               // マッチ成立
                }
            }
        }
    }

    /// <summary>
    /// 攻撃対象を決める際に使用
    /// </summary>
    /// <param name="pDice">プレイヤーのダイス</param>
    /// <param name="eDice">敵のダイス</param>
    public void manageMatch(GameObject pDice, GameObject eDice, bool isEnemy = false)
    {
        PlayerSpeedDice playerSpeedDice = pDice.GetComponent<PlayerSpeedDice>();
        EnemySpeedDice enemySpeedDice = eDice.GetComponent<EnemySpeedDice>();

        MatchData matchData = new MatchData();

        // プレイヤー側の選択
        if (!isEnemy)
        {
            matchData.matchSpeed = playerSpeedDice.mySpeed;
            // 敵のダイスがマッチ状態ではない
            if (!enemySpeedDice.isMatch)
            {
                // マッチ条件を満たす
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
                // マッチ条件を満たさない(速度が足りない)
                else
                { 
                    matchData.playerSpeedDice = pDice;
                    matchData.enemySpeedDice = eDice;
                    matchData.playerSpeed = playerSpeedDice.mySpeed;
                    matchData.enemySpeed = enemySpeedDice.mySpeed;
                    matchData.matchStatus = 1;
                }
            }
            // 敵のダイスがマッチ状態(条件問わずにプレイヤーから敵への一方攻撃)
            else
            {
                matchData.playerSpeedDice = pDice;
                matchData.enemySpeedDice = eDice;
                matchData.playerSpeed = playerSpeedDice.mySpeed;
                matchData.enemySpeed = enemySpeedDice.mySpeed;
                matchData.matchStatus = 1;
            }
        }
        // 敵側の選択
        else
        {
            matchData.matchSpeed = enemySpeedDice.mySpeed;
            // プレイヤーのダイスがマッチ状態ではない
            if (!playerSpeedDice.isMatch)
            {
                // マッチ条件を満たす
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
                // マッチ条件を満たさない(速度が足りない)
                else
                {
                    matchData.playerSpeedDice = pDice;
                    matchData.enemySpeedDice = eDice;
                    matchData.playerSpeed = playerSpeedDice.mySpeed;
                    matchData.enemySpeed = enemySpeedDice.mySpeed;
                    matchData.matchStatus = 2;
                }
            }
            // プレイヤーのダイスがマッチ状態(条件問わずに敵からプレイヤーへの一方攻撃)
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
