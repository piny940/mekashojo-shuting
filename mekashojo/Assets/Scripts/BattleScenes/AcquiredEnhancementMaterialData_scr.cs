using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquiredEnhancementMaterialData_scr : MonoBehaviour
{
    // 入手した強化用素材の数
    public Dictionary<EquipmentData_scr.equipmentType, int> acquiredEnhancementMaterialsCount { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        acquiredEnhancementMaterialsCount = new Dictionary<EquipmentData_scr.equipmentType, int>()
        {
            { EquipmentData_scr.equipmentType.MainWeapon__Cannon, 0 },
            { EquipmentData_scr.equipmentType.MainWeapon__Laser, 0 },
            { EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun, 0 },
            { EquipmentData_scr.equipmentType.SubWeapon__Balkan, 0 },
            { EquipmentData_scr.equipmentType.SubWeapon__Missile, 0 },
            { EquipmentData_scr.equipmentType.Bomb, 0 },
            { EquipmentData_scr.equipmentType.Shield__Heavy, 0 },
            { EquipmentData_scr.equipmentType.Shield__Light, 0 }
        };
    }
}
