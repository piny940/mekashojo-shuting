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
        _player = GameObject.FindGameObjectWithTag(TagManager_scr.Tags.Player__BattleScene.ToString()).GetComponent<Player_scr>();

        //nullの場合
        if (_player == null)
        {
            throw new System.Exception();
        }
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
        if (collision.tag == TagManager_scr.Tags.Player__BattleScene.ToString())
        {
            switch ((int)materialType)
            {
                case (int)MaterialType.CannonEnhancementMaterial:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__Cannon]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case (int)MaterialType.LaserEnhancementMaterial:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__Laser]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case (int)MaterialType.BeamMachineGunEnhancementMaterial:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case (int)MaterialType.BalkanEnhancementMaterial:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.SubWeapon__Balkan]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case (int)MaterialType.MissileEnhancementMaterial:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.SubWeapon__Missile]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case (int)MaterialType.BombEnhancementMaterial:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Bomb]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case (int)MaterialType.HeavyShieldEnhancementMaterial:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Shield__Heavy]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case (int)MaterialType.LightShieldEnhancementMaterial:
                    EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Shield__Light]++;
                    //AcuiredEnhancementMaterialCountを1増やす
                    break;

                case (int)MaterialType.EnergyChargeMaterial:
                    //エネルギーをチャージする
                    _player.mainEnergyAmount += Mathf.Min(ENERGY_CHARGE_AMOUNT, _player.maxMainEnergyAmount - _player.mainEnergyAmount);

                    //サブエネルギーのチャージ
                    _player.subEnergyAmount += Mathf.Min(ENERGY_CHARGE_AMOUNT, _player.maxSubEnergyAmount - _player.subEnergyAmount);

                    break;

                case (int)MaterialType.BombChargeMaterial:
                    //ボムをチャージする
                    _player.AddBomb();
                    break;

                default:
                    break;
            }
            
            //拾った時のエフェクトを表示する？
            Destroy(this.gameObject);

        }
    }
}
