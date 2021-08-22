using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageManager : MonoBehaviour
{
    [SerializeField, Header("敵のタイプを選ぶ")] NormalEnemyData_scr.normalEnemyType _enemyType;
    EnemyController_scr _enemyController;
    public float hp { get; set; }
    const float ENHANCEMENT_MATERIAL_DROP_RATE = 0.05f;
    const float ENERGY_CHARGE_MATERIAL_DROP_RATE = 0.03f;
    const float BOMB_CHARGE_MATERIAL_DROP_RATE = 1;
    public readonly int noBombDamageFrames = 3;
    public int frameCounterForPlayerBomb { get; private set; }  //BombFire__Playerで"frameCounterForPlayerBombがnoBombDamageFramesより小さかったら
                                                                //「isInsideBomb」をtrueにする”って処理を入れることで、ボムのなかにスポーンしたのかどうかを判定する
    public bool isInsideBomb = false;

    private void Start()
    {
        _enemyController = GameObject.FindGameObjectWithTag(TagManager_scr.Tags.EnemyController__BattleScene.ToString()).GetComponent<EnemyController_scr>();

        //nullの場合
        if (_enemyController == null)
        {
            throw new System.Exception();
        }

        hp = NormalEnemyData_scr.normalEnemyData.normalEnemyStatus[_enemyType][NormalEnemyData_scr.normalEnemyParameter.HP];

        //noBombDamageFramesは1だと意味がないため１以下だとエラーを吐くようにする
        if (noBombDamageFrames < 2)
        {
            throw new System.Exception();
        }
    }


    private void Update()
    {
        CountFrameForPlayerBomb();
    }


    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="power"></param>
    public void GetDamage(float power)
    {
        hp -= power;

        if (hp <= 0)
        {
            Die();
        }
    }


    /// <summary>
    /// 死ぬ
    /// </summary>
    void Die()
    {
        //ドロップアイテムを落とす

        float randomValueForChoosingWhatMaterialToDrop = Random.value;

        if (randomValueForChoosingWhatMaterialToDrop <= ENHANCEMENT_MATERIAL_DROP_RATE)
        {
            //強化素材を落とす
            float randomValueForChoosingWhatEnhancementMaterialToDrop = Mathf.Floor(Random.value * 8);
            switch (randomValueForChoosingWhatEnhancementMaterialToDrop)
            {
                case (int)EquipmentData_scr.equipmentType.MainWeapon__Cannon:
                    //Cannonの強化素材を落とす
                    Instantiate((GameObject)Resources.Load("BattleScenes/CannonEnhancementMaterial"), transform.position, Quaternion.identity);
                    break;

                case (int)EquipmentData_scr.equipmentType.MainWeapon__Laser:
                    //Laserの強化素材を落とす
                    Instantiate((GameObject)Resources.Load("BattleScenes/LaserEnhancementMaterial"), transform.position, Quaternion.identity);
                    break;

                case (int)EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun:
                    //BeamMachineGunの強化素材を落とす
                    Instantiate((GameObject)Resources.Load("BattleScenes/BeamMachineGunEnhancementMaterial"), transform.position, Quaternion.identity);
                    break;

                case (int)EquipmentData_scr.equipmentType.SubWeapon__Balkan:
                    //Balkanの強化素材を落とす
                    Instantiate((GameObject)Resources.Load("BattleScenes/BalkanEnhancementMaterial"), transform.position, Quaternion.identity);
                    break;

                case (int)EquipmentData_scr.equipmentType.SubWeapon__Missile:
                    //Missileの強化素材を落とす
                    Instantiate((GameObject)Resources.Load("BattleScenes/MissileEnhancementMaterial"), transform.position, Quaternion.identity);
                    break;

                case (int)EquipmentData_scr.equipmentType.Bomb:
                    //Bombの強化素材を落とす
                    Instantiate((GameObject)Resources.Load("BattleScenes/BombEnhancementMaterial"), transform.position, Quaternion.identity);
                    break;

                case (int)EquipmentData_scr.equipmentType.Shield__Heavy:
                    //HeavyShieldの強化素材を落とす
                    Instantiate((GameObject)Resources.Load("BattleScenes/HeavyShieldEnhancementMaterial"), transform.position, Quaternion.identity);
                    break;

                case (int)EquipmentData_scr.equipmentType.Shield__Light:
                    //LightShieldの強化素材を落とす
                    Instantiate((GameObject)Resources.Load("BattleScenes/LightShieldEnhancementMaterial"), transform.position, Quaternion.identity);
                    break;

                default:
                    break;

            }

        }
        else if (randomValueForChoosingWhatMaterialToDrop <= ENHANCEMENT_MATERIAL_DROP_RATE + ENERGY_CHARGE_MATERIAL_DROP_RATE)
        {
            //エネルギー回復パックを落とす
            Instantiate((GameObject)Resources.Load("BattleScenes/EnergyChargeMaterial"), transform.position, Quaternion.identity);
        }
        else if (randomValueForChoosingWhatMaterialToDrop <= ENHANCEMENT_MATERIAL_DROP_RATE + ENERGY_CHARGE_MATERIAL_DROP_RATE + BOMB_CHARGE_MATERIAL_DROP_RATE)
        {
            //ボム補充アイテムを落とす
            Instantiate((GameObject)Resources.Load("BattleScenes/BombChargeMaterial"), transform.position, Quaternion.identity);
        }

        //消滅する
        _enemyController.EnemyAmount--;
        Destroy(this.gameObject);
    }


    /// <summary>
    /// プレイヤーのボムの内側にスポーンした時はボムをダメージを受けないようにするためのフレームカウンター「frameCounterForPlayerBomb」をマイフレームincrementする<br></br>
    /// Updateで呼ぶ
    /// </summary>
    void CountFrameForPlayerBomb()
    {
        if (frameCounterForPlayerBomb < noBombDamageFrames)
        {
            frameCounterForPlayerBomb++;
        }
    }
}
