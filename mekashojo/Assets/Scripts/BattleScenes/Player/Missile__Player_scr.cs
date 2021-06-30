using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile__Player_scr : PlayerWeaponBaseImp
{
    [SerializeField, Header("GetInputを入れる")] GetInput_scr _getInput;
    [SerializeField, Header("Playerを入れる")] Player_scr _player;
    bool _hasAttacked = false;

    private void Start()
    {
        SetMethod(MyAttack, MyCanAttack, EquipmentData_scr.equipmentData.equipmentStatus[EquipmentData_scr.equipmentData.selectedSubWeaponName][EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentData.selectedSubWeaponName]][EquipmentData_scr.equipmentParameter.Cost], null, null);
    }

    bool MyCanAttack()
    {
        //左クリックを離した瞬間の処理
        if (_hasAttacked && !_getInput.isMouseLeft)
        {
            _hasAttacked = false;
        }

        return !_hasAttacked && _getInput.isMouseLeft && _player.subEnergyAmount > 0;
    }

    void MyAttack()
    {
        GameObject missileFire__Player = Instantiate((GameObject)Resources.Load("BattleScenes/MissileFire__Player"), transform.position, Quaternion.identity);

        float a = transform.position.x;
        float b = transform.position.y;
        float u = _getInput.mousePosition.x;
        float v = _getInput.mousePosition.y;
        float theta = Vector3.SignedAngle(new Vector3(1, 0, 0), new Vector3(u - a, v - b, 0), new Vector3(0, 0, 1));
        missileFire__Player.transform.localEulerAngles = new Vector3(0, 0, theta);

        _hasAttacked = true;
    }
}
