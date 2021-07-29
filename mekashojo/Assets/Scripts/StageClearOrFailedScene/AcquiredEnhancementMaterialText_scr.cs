using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquiredEnhancementMaterialText_scr : MonoBehaviour
{
    // 入手した強化用素材の数
    private Dictionary<EquipmentData_scr.equipmentType, int> _acquiredEnhancementMaterialsCount;

    GameObject _acquiredEnhancementMaterialData;

    // Start is called before the first frame update
    void Start()
    {
        _acquiredEnhancementMaterialData = GameObject.FindGameObjectWithTag(TagManager_scr.Tags.AcquiredEnhancementMaterialData__BattleScene.ToString());

        if (_acquiredEnhancementMaterialData == null)
        {
            _acquiredEnhancementMaterialsCount = new Dictionary<EquipmentData_scr.equipmentType, int>()
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

            return;
        }

        _acquiredEnhancementMaterialsCount = _acquiredEnhancementMaterialData.GetComponent<AcquiredEnhancementMaterialData_scr>().acquiredEnhancementMaterialsCount;

        Destroy(_acquiredEnhancementMaterialData);

        //Textの内容を更新する（未実装）
    }
}
