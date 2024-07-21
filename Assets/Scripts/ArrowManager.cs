using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public GameObject matchArrowPrefab; // 矢印のプレハブ(マッチ)
    public GameObject playerArrowPrefab; // 矢印のプレハブ(プレイヤーの一方攻撃)
    public GameObject enemyArrowPrefab; // 矢印のプレハブ(敵の一方攻撃)

    [SerializeField] private AnimationCurve _animationCurve;

    public void ArrowUpdate(MatchData data)
    {
        if(data.matchStatus == 0)
        {
            GameObject arrow = Instantiate(matchArrowPrefab, transform);

            // 2つのオブジェクトの位置を取得
            Vector3 startPos = data.playerSpeedDice.transform.GetChild(0).position;
            Vector3 endPos = data.enemySpeedDice.transform.GetChild(1).position;

            // 矢印の向きと角度を計算
            Vector3 direction = endPos - startPos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 矢印の位置と角度を設定
            arrow.transform.position = startPos;
            arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        }else if(data.matchStatus == 1)
        {
            GameObject arrow = Instantiate(playerArrowPrefab, transform);

            // 2つのオブジェクトの位置を取得
            Vector3 startPos = data.playerSpeedDice.transform.GetChild(0).position;
            Vector3 endPos = data.enemySpeedDice.transform.GetChild(1).position;

            // 矢印の向きと角度を計算
            Vector3 direction = endPos - startPos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 矢印の位置と角度を設定
            arrow.transform.position = startPos;
            arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            GameObject arrow = Instantiate(enemyArrowPrefab, transform);

            // 2つのオブジェクトの位置を取得
            Vector3 startPos = data.playerSpeedDice.transform.GetChild(0).position;
            Vector3 endPos = data.enemySpeedDice.transform.GetChild(1).position;

            // 矢印の向きと角度を計算
            Vector3 direction = endPos - startPos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 矢印の位置と角度を設定
            arrow.transform.position = startPos;
            arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
    }
}
