using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAndLaser__PlayerBaseIMP : MonoBehaviour
{
    [SerializeField, Header("◯◯Fire__Playerを入れる")] GameObject _fire__Player;
    [SerializeField, Header("GetInputを入れる")] GetInput_scr _getInput;
    [SerializeField, Header("Playerを入れる")] Player_scr _player;
    bool _hasActived;

    public void Start()
    {
        _fire__Player.SetActive(false);
    }

    public void Attack()
    {
        //マウスを離した瞬間orエネルギーがなくなった瞬間の処理
        if (_hasActived && (!_getInput.isMouseLeft || _player.mainEnergyAmount <= 0))
        {
            _fire__Player.SetActive(false);
            _hasActived = false;
        }

        if (_getInput.isMouseLeft && _player.mainEnergyAmount > 0)
        {
            //左クリックした瞬間の処理
            if (!_hasActived)
            {
                _fire__Player.SetActive(true);
                _hasActived = true;
            }

            //エネルギーを減らす
            _player.mainEnergyAmount -= EquipmentData_scr.equipmentData.equipmentStatus[_player.mainWeaponName][EquipmentData_scr.equipmentData.equipmentLevel[_player.mainWeaponName]][EquipmentData_scr.equipmentParameter.Cost] * Time.deltaTime;

            //ミサイルの向きを変える
            float a = transform.position.x;
            float b = transform.position.y;
            float u = _getInput.mousePosition.x;
            float v = _getInput.mousePosition.y;
            float theta = Vector3.SignedAngle(new Vector3(1, 0, 0), new Vector3(u - a, v - b, 0), new Vector3(0, 0, 1));
            transform.localEulerAngles = new Vector3(0, 0, theta);
            
        }
        

        
    }

}
