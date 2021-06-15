using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetDamage_scr : MonoBehaviour
{

    [SerializeField,Header("敵のタイプを選ぶ")] NormalEnemyData_scr.normalEnemyType _enemyType;
    EnemyController_scr _enemyController;
    public float hp { get; set; }
    const float ENHANCEMENT_MATERIAL_DROP_RATE = 0.05f;
    const float ENERGY_CHARGE_MATERIAL_DROP_RATE = 0.03f;
    const float BOMB_CHARGE_MATERIAL_DROP_RATE = 0.01f;

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

        float randomValueA = Random.value;

        if (randomValueA <= ENHANCEMENT_MATERIAL_DROP_RATE)
        {
            //強化素材を落とす
            float randomValueB = Random.value * 8;
            switch (randomValueB - randomValueB % 1)    //0から8までのランダムな整数値を取得するもっといい方法があったら教えてください
            {
                case 0:
                    //Cannonの強化素材を落とす
                    Instantiate((GameObject)Resources.Load("BattleScenes/CannonEnhancementMaterial"), transform.position, Quaternion.identity);
                    break;
                case 1:
                    //Laserの強化素材を落とす
                    Instantiate((GameObject)Resources.Load("BattleScenes/LaserEnhancementMaterial"), transform.position, Quaternion.identity);
                    break;
                case 2:
                    //BeamMachineGunの強化素材を落とす
                    Instantiate((GameObject)Resources.Load("BattleScenes/BeamMachineGunEnhancementMaterial"), transform.position, Quaternion.identity);
                    break;
                case 3:
                    //Balkanの強化素材を落とす
                    Instantiate((GameObject)Resources.Load("BattleScenes/BalkanEnhancementMaterial"), transform.position, Quaternion.identity);
                    break;
                case 4:
                    //Missileの強化素材を落とす
                    Instantiate((GameObject)Resources.Load("BattleScenes/MissileEnhancementMaterial"), transform.position, Quaternion.identity);
                    break;
                case 5:
                    //Bombの強化素材を落とす
                    Instantiate((GameObject)Resources.Load("BattleScenes/BombEnhancementMaterial"), transform.position, Quaternion.identity);
                    break;
                case 6:
                    //HeavyShieldの強化素材を落とす
                    Instantiate((GameObject)Resources.Load("BattleScenes/HeavyShieldEnhancementMaterial"), transform.position, Quaternion.identity);
                    break;
                case 7:
                case 8:
                    //LightShieldの強化素材を落とす
                    Instantiate((GameObject)Resources.Load("BattleScenes/LightShieldEnhancementMaterial"), transform.position, Quaternion.identity);
                    break;

            }

        }
        else if (randomValueA <= ENHANCEMENT_MATERIAL_DROP_RATE + ENERGY_CHARGE_MATERIAL_DROP_RATE)
        {
            //エネルギー回復パックを落とす
            Instantiate((GameObject)Resources.Load("BattleScenes/EnergyChargeMaterial"), transform.position, Quaternion.identity);
        }
        else if (randomValueA <= ENHANCEMENT_MATERIAL_DROP_RATE + ENERGY_CHARGE_MATERIAL_DROP_RATE + BOMB_CHARGE_MATERIAL_DROP_RATE)
        {
            //ボム補充アイテムを落とす
            Instantiate((GameObject)Resources.Load("BattleScenes/BombChargeMaterial"), transform.position, Quaternion.identity);
        }

        //消滅する
        _enemyController.EnemyAmount--;
        Destroy(this.gameObject);
    }
}
