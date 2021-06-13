using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAndLaserFire__PlayerBaseImp : MonoBehaviour
{
    protected float power;

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == Common_scr.Tags.Enemy_BattleScene.ToString())
        {
            EnemyGetDamage_scr enemyGetDamage = collision.GetComponent<EnemyGetDamage_scr>();
            enemyGetDamage.GetDamage(power * Time.deltaTime);
        }
    }

    
}
