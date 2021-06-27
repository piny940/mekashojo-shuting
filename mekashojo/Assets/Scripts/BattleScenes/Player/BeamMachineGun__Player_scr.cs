using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamMachineGun__Player_scr : PlayerWeaponBaseImp
{
    [SerializeField, Header("GetInputを入れる")] GetInput_scr _getInput;
    [SerializeField, Header("Playerを入れる")] Player_scr _player;
    [SerializeField, Header("1秒あたりに発射する球の数")] int _firePerSecound;
    int _count;

    // Start is called before the first frame update
    void Start()
    {
        _count = 0;
        SetMethod(MyAttack, MyCanAttack, EquipmentData_scr.equipmentData.equipmentStatus[EquipmentData_scr.equipmentData.selectedMainWeaponName][EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentData.selectedMainWeaponName]][EquipmentData_scr.equipmentParameter.Cost], null, null, null);
    }

    void MyAttack()
    {
        GameObject beamMachineGunFire__Player = Instantiate((GameObject)Resources.Load("BattleScenes/BeamMachineGunFire__Player"), transform.position, Quaternion.identity);

        float a = transform.position.x;
        float b = transform.position.y;
        float u = _getInput.mousePosition.x;
        float v = _getInput.mousePosition.y;
        float theta = Vector3.SignedAngle(new Vector3(1, 0, 0), new Vector3(u - a, v - b, 0), new Vector3(0, 0, 1));
        beamMachineGunFire__Player.transform.localEulerAngles = new Vector3(0, 0, theta);

        _count = 0;
    }

    bool MyCanAttack()
    {
        _count++;
        return _count > 60 / _firePerSecound && _getInput.isMouseLeft && _player.mainEnergyAmount > 0;
    }
}
