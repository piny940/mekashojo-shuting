using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAndLaser__PlayerBaseImp : MonoBehaviour
{
    [SerializeField, Header("◯◯Fire__Playerを入れる")] GameObject _fire__Player;
    [SerializeField, Header("GetInputを入れる")] GetInput_scr _getInput;
    [SerializeField, Header("Playerを入れる")] Player_scr _player;
    bool _isActive;
    const float UNABLE_TO_START_USING_RATE = 0.01f; //武器を使い始めることができなくなるエネルギー量(全体に対する割合)

    public void Start()
    {
        _fire__Player.SetActive(false);
    }

    public void Attack()
    {
        //マウスを離した瞬間orエネルギーがなくなった瞬間の処理
        if (_isActive && (!_getInput.isMouseLeft || _player.mainEnergyAmount <= 0))
        {
            _fire__Player.SetActive(false);
            _isActive = false;
        }

        //左クリックした瞬間の処理
        if (_getInput.isMouseLeft && _player.mainEnergyAmount > _player.maxMainEnergyAmount * UNABLE_TO_START_USING_RATE && !_isActive)
        {
            _fire__Player.SetActive(true);
            _isActive = true;
        }

        //左クリックしている間の処理
        if (_isActive)
        {
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

    public void StopUsing()
    {
        if (_isActive)
        {
            _fire__Player.SetActive(false);
            _isActive = false;
        }
    }
}
