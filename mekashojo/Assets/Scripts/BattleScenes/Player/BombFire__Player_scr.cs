using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFire__Player_scr : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Common_scr.Tags.Enemy__BattleScene.ToString())
        {
            EnemyDamageManager enemyDamageManager = collision.GetComponent<EnemyDamageManager>();

            if (enemyDamageManager == null)
            {
                throw new System.Exception();
            }

            //敵がボムの内側にスポーンした場合はダメージを与えない
            if (enemyDamageManager.frameCounterForPlayerBomb < enemyDamageManager.noBombDamageFrames)
            {
                enemyDamageManager.isInsideBomb = true;
            }
            else if (!enemyDamageManager.isInsideBomb)
            {
                enemyDamageManager.GetDamage(EquipmentData_scr.equipmentData.equipmentStatus[EquipmentData_scr.equipmentType.Bomb][EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Bomb]][EquipmentData_scr.equipmentParameter.Power]);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Common_scr.Tags.Enemy__BattleScene.ToString())
        {
            EnemyDamageManager enemyDamageManager = collision.GetComponent<EnemyDamageManager>();

            if (enemyDamageManager == null)
            {
                throw new System.Exception();
            }

            enemyDamageManager.isInsideBomb = false;
        }
    }
}
