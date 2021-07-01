using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAndLaserFire__PlayerBaseImp : MonoBehaviour
{
    protected float power;

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == Common_scr.Tags.Enemy__BattleScene.ToString())
        {
            EnemyDamageManager enemyDamageManager = collision.GetComponent<EnemyDamageManager>();
            enemyDamageManager.GetDamage(power * Time.deltaTime);
        }
    }

    
}
