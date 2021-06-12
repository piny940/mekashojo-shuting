using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetDamage_scr : MonoBehaviour
{
    [SerializeField,Header("敵のタイプを選ぶ")] NormalEnemyData_scr.normalEnemyType _enemyType;
    EnemyController_scr _enemyController;
    public float hp { get; set; }

    private void Start()
    {
        _enemyController = GameObject.FindGameObjectWithTag(Common_scr.Tags.EnemyController_BattleScene.ToString()).GetComponent<EnemyController_scr>();

        hp = NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[_enemyType][NormalEnemyData_scr.normalEnemyParameter.HP];

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
