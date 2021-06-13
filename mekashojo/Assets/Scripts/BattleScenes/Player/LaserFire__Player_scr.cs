using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFire__Player_scr : CannonAndLaserFire__PlayerBaseIMP
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        power = EquipmentData_scr.equipmentData.equipmentStatus[EquipmentData_scr.equipmentType.MainWeapon__Laser][EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__Laser]][EquipmentData_scr.equipmentParameter.Power];

    }
}
