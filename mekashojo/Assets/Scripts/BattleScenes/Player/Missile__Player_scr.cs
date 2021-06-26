using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile__Player_scr : MonoBehaviour
{
    [SerializeField, Header("GetInputを入れる")] GetInput_scr _getInput;
    [SerializeField, Header("Playerを入れる")] Player_scr _player;
    bool _canFire = true;

    public void Attack()
    {
        //ミサイルは1クリック分の処理しかしない
        AttackProcess();

        if (_canFire && _getInput.isMouseLeft && _player.subEnergyAmount > 0)
        {
            Fire();
        }
        
    }

    /// <summary>
    /// 攻撃に関する細かい処理をする
    /// </summary>
    void AttackProcess()
    {
        //左クリックを離した瞬間の処理
        if (!_canFire && !_getInput.isMouseLeft)
        {
            _canFire = true;
        }
    }


    /// <summary>
    /// ミサイルを発射する
    /// </summary>
    void Fire()
    {
        GameObject missileFire__Player = Instantiate((GameObject)Resources.Load("BattleScenes/MissileFire__Player"), transform.position, Quaternion.identity);

        float a = transform.position.x;
        float b = transform.position.y;
        float u = _getInput.mousePosition.x;
        float v = _getInput.mousePosition.y;
        float theta = Vector3.SignedAngle(new Vector3(1, 0, 0), new Vector3(u - a, v - b, 0), new Vector3(0, 0, 1));
        missileFire__Player.transform.localEulerAngles = new Vector3(0, 0, theta);

        _player.subEnergyAmount -= EquipmentData_scr.equipmentData.equipmentStatus[EquipmentData_scr.equipmentData.selectedSubWeaponName][EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentData.selectedSubWeaponName]][EquipmentData_scr.equipmentParameter.Cost];

        _canFire = false;
    }
}
