using System.Collections.Generic;

namespace Model
{
    public class AcquiredEnhancementMaterialData
    {
        // 入手した強化用素材の数
        public Dictionary<EquipmentData.equipmentType, int> acquiredEnhancementMaterialsCount { get; set; }

        public AcquiredEnhancementMaterialData()
        {
            acquiredEnhancementMaterialsCount = new Dictionary<EquipmentData.equipmentType, int>()
            {
                { EquipmentData.equipmentType.MainWeapon__Cannon, 0 },
                { EquipmentData.equipmentType.MainWeapon__Laser, 0 },
                { EquipmentData.equipmentType.MainWeapon__BeamMachineGun, 0 },
                { EquipmentData.equipmentType.SubWeapon__Balkan, 0 },
                { EquipmentData.equipmentType.SubWeapon__Missile, 0 },
                { EquipmentData.equipmentType.Bomb, 0 },
                { EquipmentData.equipmentType.Shield__Heavy, 0 },
                { EquipmentData.equipmentType.Shield__Light, 0 }
            };
        }
    }
}
