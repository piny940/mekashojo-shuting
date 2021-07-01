using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire__Player_scr : CannonAndLaserFire__PlayerBaseImp
{
    // Start is called before the first frame update
    void Start()
    {
        power = EquipmentData_scr.equipmentData.equipmentStatus[EquipmentData_scr.equipmentType.MainWeapon__Cannon][EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__Cannon]][EquipmentData_scr.equipmentParameter.Power];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
