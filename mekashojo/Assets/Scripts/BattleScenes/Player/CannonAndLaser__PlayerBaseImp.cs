using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAndLaser__PlayerBaseImp : PlayerWeaponBaseImp
{
    [SerializeField, Header("Cannon/LaserFire__Playerを入れる")] GameObject _fire__Player;
    [SerializeField, Header("GetInputを入れる")] GetInput_scr _getInput;
    [SerializeField, Header("Playerを入れる")] Player_scr _player;
    const float UNABLE_TO_START_USING_RATE = 0.01f; //武器を使い始めることができなくなるエネルギー量(全体に対する割合)


    public void Start()
    {
        _fire__Player.SetActive(false);

        SetMethod(MyAttack, MyCanAttack, EquipmentData_scr.equipmentData.equipmentStatus[EquipmentData_scr.equipmentData.selectedMainWeaponName][EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentData.selectedMainWeaponName]][EquipmentData_scr.equipmentParameter.Cost] * Time.deltaTime, MyProceedFirst, MyProceedLast, MyCanStartAttack);
    }

    /// <summary>
    /// 使用をやめる
    /// </summary>
    public void StopUsing()
    {
        MyProceedLast();
    }

    /// <summary>
    /// 攻撃そのもの
    /// </summary>
    void MyAttack()
    {
        //ミサイルの向きを変える
        float a = transform.position.x;
        float b = transform.position.y;
        float u = _getInput.mousePosition.x;
        float v = _getInput.mousePosition.y;
        float theta = Vector3.SignedAngle(new Vector3(1, 0, 0), new Vector3(u - a, v - b, 0), new Vector3(0, 0, 1));
        transform.localEulerAngles = new Vector3(0, 0, theta);
    }

    /// <summary>
    /// 攻撃のはじめにする処理
    /// </summary>
    void MyProceedFirst()
    {
        _fire__Player.SetActive(true);
        canAttack = true;
    }

    /// <summary>
    /// 攻撃の最後にする処理
    /// </summary>
    void MyProceedLast()
    {
        _fire__Player.SetActive(false);
        canAttack = false;
    }

    /// <summary>
    /// 攻撃し続けることができるか
    /// </summary>
    /// <returns></returns>
    bool MyCanAttack()
    {
        return _getInput.isMouseLeft && _player.mainEnergyAmount > 0;
    }

    /// <summary>
    /// 攻撃し始めることができるか
    /// </summary>
    /// <returns></returns>
    bool MyCanStartAttack()
    {
        return _getInput.isMouseLeft && _player.mainEnergyAmount > _player.maxMainEnergyAmount * UNABLE_TO_START_USING_RATE;
    }
}
