using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile__Player_scr : MonoBehaviour
{
    [SerializeField, Header("GetInputを入れる")] GetInput_scr _getInput;
    [SerializeField, Header("Playerを入れる")] Player_scr _player;
    bool _hasAttacked = false;

    public void Attack()
    {
        //ミサイルは1クリック分の処理しかしない

        //左クリックを離した瞬間の処理
        if (_hasAttacked && !_getInput.isMouseLeft)
        {
            _hasAttacked = false;
            return;
        }

        //左クリックを押した状態でも2フレーム目以降は何もしない
        if (_hasAttacked)
        {
            return;
        }

        if (_getInput.isMouseLeft && _player.subEnergyAmount > 0)
        {
            _hasAttacked = true;
            GameObject missileFire__Player = Instantiate((GameObject)Resources.Load("BattleScenes/MissileFire__Player"), transform.position, Quaternion.identity);

            float a = transform.position.x;
            float b = transform.position.y;
            float u = _getInput.mousePosition.x;
            float v = _getInput.mousePosition.y;
            float theta = Vector3.SignedAngle(new Vector3(1, 0, 0), new Vector3(u - a, v - b, 0), new Vector3(0, 0, 1));
            missileFire__Player.transform.localEulerAngles = new Vector3(0, 0, theta);

            _player.subEnergyAmount -= EquipmentData_scr.equipmentData.equipmentStatus[_player.subWeaponName][EquipmentData_scr.equipmentData.equipmentLevel[_player.subWeaponName]][EquipmentData_scr.equipmentParameter.Cost];
        }
    }
}
