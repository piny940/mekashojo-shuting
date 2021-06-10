using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetDamage_scr : MonoBehaviour
{
    [Header("HP")] public float hp;
    EnemyController_scr _enemyController;

    private void Start()
    {
        _enemyController = GameObject.FindGameObjectWithTag(Common_scr.Tags.EnemyController_BattleScene.ToString()).GetComponent<EnemyController_scr>();
    }

    public void GetDamage(float power)
    {
        hp -= power;

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //ドロップアイテムを落とす
        _enemyController.EnemyAmount--;
        Destroy(this.gameObject);
    }
}
