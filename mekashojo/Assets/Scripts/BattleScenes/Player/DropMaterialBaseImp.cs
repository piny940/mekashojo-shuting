using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMaterialBaseImp : MonoBehaviour
{
    const float RISE_TIME = 0.1f;   //上昇する時間
    const float FALL_TIME = 0.1f;   //下降する時間
    const float ENERGY_CHARGE_AMOUNT = 300; //エネルギー回復量
    protected MaterialType materialType;
    Rigidbody2D _rigidbody2D;
    float _time;
    bool _hasAppeared;
    bool _isRising;
    Player_scr _player;

    protected enum MaterialType
    {
        CannonEnhancementMaterial,
        LaserEnhancementMaterial,
        BeamMachineGunEnhancementMaterial,
        BalkanEnhancementMaterial,
        MissileEnhancementMaterial,
        BombEnhancementMaterial,
        HeavyShieldEnhancementMaterial,
        LightShieldEnhancementMaterial,
        EnergyChargeMaterial,
        BombChargeMaterial
    }


    /// <summary>
    /// Startメソッドで呼ぶ
    /// </summary>
    protected void Initialize()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _time = 0;
        _hasAppeared = false;
        _isRising = false;
        _player = GameObject.FindGameObjectWithTag(Common_scr.Tags.Player_BattleScene.ToString()).GetComponent<Player_scr>();
    }


    /// <summary>
    /// 出現するときにぴょこんと跳ぶ動きをつける<br></br>
    /// Updateメソッドで呼ぶ
    /// </summary>
    protected void Emerge()
    {
        if (!_hasAppeared)
        {
            _time += Time.deltaTime;

            //上昇
            if (!_isRising && _time < RISE_TIME)
            {
                _rigidbody2D.velocity = new Vector3(0, 2, 0);
                _isRising = true;
                return;
            }

            //下降
            if (_isRising && _time > RISE_TIME && _time < RISE_TIME + FALL_TIME)
            {
                _rigidbody2D.velocity = new Vector3(0, -2, 0);
                _isRising = false;
                return;
            }

            //停止
            if (_time >= RISE_TIME + FALL_TIME)
            {
                _rigidbody2D.velocity = Vector3.zero;
                _hasAppeared = true;
            }

        }
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Common_scr.Tags.Player_BattleScene.ToString())
        {
            switch ((int)materialType)
            {
                case 0:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__Cannon]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;
                case 1:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__Laser]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;
                case 2:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;
                case 3:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.SubWeapon__Balkan]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;
                case 4:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.SubWeapon__Missile]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;
                case 5:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Bomb]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;
                case 6:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Shield__Heavy]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;
                case 7:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Shield__Light]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;
                case 8:
                    //エネルギーをチャージする
                    //メインエネルギーのチャージ(エネルギーが満タンになる場合の処理は分けた)
                    if (_player.mainEnergyAmount < _player.maxMainEnergyAmount - ENERGY_CHARGE_AMOUNT)
                    {
                        _player.mainEnergyAmount += ENERGY_CHARGE_AMOUNT;
                    }
                    else
                    {
                        _player.mainEnergyAmount = _player.maxMainEnergyAmount;
                    }

                    //サブエネルギーのチャージ(エネルギーが満タンになる場合の処理は分けた)
                    if (_player.subEnergyAmount < _player.maxSubEnergyAmount - ENERGY_CHARGE_AMOUNT)
                    {
                        _player.subEnergyAmount += ENERGY_CHARGE_AMOUNT;
                    }
                    else
                    {
                        _player.subEnergyAmount = _player.maxSubEnergyAmount;
                    }

                    break;
                case 9:
                    //ボムをチャージする
                    _player.AddBomb();
                    break;
            }
            
            //拾った時のエフェクトを表示する？
            Destroy(this.gameObject);

        }
    }
}
