using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balkan__Player_scr : PlayerWeaponBaseImp
{
    [SerializeField, Header("GetInputを入れる")] GetInput_scr _getInput;
    [SerializeField, Header("Playerを入れる")] Player_scr _player;
    [SerializeField, Header("1秒あたりに発射する球の数")] int _firePerSecound;
    int _count;
    public AttackManager attackManager;

    // Start is called before the first frame update
    void Start()
    {
        _count = 0;
        attackManager = new AttackManager(Attack, CanAttack, EquipmentData_scr.equipmentData.equipmentStatus[EquipmentData_scr.equipmentData.selectedSubWeaponName][EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentData.selectedSubWeaponName]][EquipmentData_scr.equipmentParameter.Cost], null, null, null);
    }


    void Attack()
    {
        Instantiate((GameObject)Resources.Load("BattleScenes/BalkanFire__Player"), transform.position, Quaternion.identity);

        _count = 0;
    }


    bool CanAttack()
    {
        _count++;
        return _count > 60 / _firePerSecound && _getInput.isMouseLeft && _player.subEnergyAmount > 0;
    }
}
