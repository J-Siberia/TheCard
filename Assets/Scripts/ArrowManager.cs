using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public GameObject matchArrowPrefab; // ���̃v���n�u(�}�b�`)
    public GameObject playerArrowPrefab; // ���̃v���n�u(�v���C���[�̈���U��)
    public GameObject enemyArrowPrefab; // ���̃v���n�u(�G�̈���U��)

    [SerializeField] private AnimationCurve _animationCurve;

    public void ArrowUpdate(MatchData data)
    {
        if(data.matchStatus == 0)
        {
            GameObject arrow = Instantiate(matchArrowPrefab, transform);

            // 2�̃I�u�W�F�N�g�̈ʒu���擾
            Vector3 startPos = data.playerSpeedDice.transform.GetChild(0).position;
            Vector3 endPos = data.enemySpeedDice.transform.GetChild(1).position;

            // ���̌����Ɗp�x���v�Z
            Vector3 direction = endPos - startPos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // ���̈ʒu�Ɗp�x��ݒ�
            arrow.transform.position = startPos;
            arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        }else if(data.matchStatus == 1)
        {
            GameObject arrow = Instantiate(playerArrowPrefab, transform);

            // 2�̃I�u�W�F�N�g�̈ʒu���擾
            Vector3 startPos = data.playerSpeedDice.transform.GetChild(0).position;
            Vector3 endPos = data.enemySpeedDice.transform.GetChild(1).position;

            // ���̌����Ɗp�x���v�Z
            Vector3 direction = endPos - startPos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // ���̈ʒu�Ɗp�x��ݒ�
            arrow.transform.position = startPos;
            arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            GameObject arrow = Instantiate(enemyArrowPrefab, transform);

            // 2�̃I�u�W�F�N�g�̈ʒu���擾
            Vector3 startPos = data.playerSpeedDice.transform.GetChild(0).position;
            Vector3 endPos = data.enemySpeedDice.transform.GetChild(1).position;

            // ���̌����Ɗp�x���v�Z
            Vector3 direction = endPos - startPos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // ���̈ʒu�Ɗp�x��ݒ�
            arrow.transform.position = startPos;
            arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
    }
}
