using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSelectButtonBaseImp : ButtonBaseImp
{
    [SerializeField] private Canvas_scr _canvas;
    [SerializeField] private PreviewImage_scr _previewImage;
    [SerializeField] private WeaponDescriptions_scr _weaponDescriptions;
    [SerializeField] private MotionPreview_scr _motionPreview;
    [SerializeField] private Level_scr _level;
    [SerializeField] private EnhancementMaterialsCount_Title_scr _enhancementMaterialsCount_Title;
    [SerializeField] private EnhancementMaterialsCount_scr _enhancementMaterialsCount;
    [SerializeField] private EnhancementButton_scr _enhancementButton;
    [SerializeField] private Weight__Status_scr _weight__Status;
    [SerializeField] private DamageReductionRate__Status_scr _damageReductionRate__Status;

    protected EquipmentData_scr.equipmentType type { get; set; }

    protected void Initialize()
    {
        GetComponentInChildren<Text>().text = EquipmentData_scr.equipmentData.equipmentDisplayName[type];
        Vector3[] corners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(corners);
        _canvas.equipmentSelectButtonCorners.Add(corners, new KeyValuePair<EquipmentData_scr.equipmentType, Action>(type, SelectedWeaponChanged));
    }

    protected void SelectedWeaponChanged()
    {
        UpdateEquipmentDescriptions();
    }

    public void Enhance()
    {
        EquipmentData_scr.equipmentData.enhancementMaterialsCount[type]
            -= EquipmentData_scr.equipmentData.equipmentStatus[type][EquipmentData_scr.equipmentData.equipmentLevel[type]][EquipmentData_scr.equipmentParameter.RequiredEnhancementMaterialsCount];
        EquipmentData_scr.equipmentData.equipmentLevel[type]++;
        UpdateEquipmentDescriptions();
    }

    private void UpdateEquipmentDescriptions()
    {
        _enhancementButton.EnhanceAction = Enhance;

        var _random = UnityEngine.Random.insideUnitSphere;
        _previewImage.color = new Color(_random.x, _random.y, _random.z);

        _weaponDescriptions.text = EquipmentData_scr.equipmentData.equipmentDescriptions[type];

        _random = UnityEngine.Random.insideUnitSphere;
        _motionPreview.color = new Color(_random.x, _random.y, _random.z);

        _level.text = EquipmentData_scr.equipmentData.levelDisplayName[EquipmentData_scr.equipmentData.equipmentLevel[type]];

        _enhancementMaterialsCount_Title.text = $"{EquipmentData_scr.equipmentData.equipmentDisplayName[type]}強化素材";

        if (IsMaxLevel())
        {
            _enhancementMaterialsCount.text = "これ以上強化できません";
            _enhancementButton.isActive = false;
        }
        else
        {
            _enhancementMaterialsCount.text = $"{EquipmentData_scr.equipmentData.enhancementMaterialsCount[type]} / {EquipmentData_scr.equipmentData.equipmentStatus[type][EquipmentData_scr.equipmentData.equipmentLevel[type]][EquipmentData_scr.equipmentParameter.RequiredEnhancementMaterialsCount]}";

            if (EquipmentData_scr.equipmentData.enhancementMaterialsCount[type] >= EquipmentData_scr.equipmentData.equipmentStatus[type][EquipmentData_scr.equipmentData.equipmentLevel[type]][EquipmentData_scr.equipmentParameter.RequiredEnhancementMaterialsCount])
            {
                _enhancementButton.isActive = true;
            }
            else
            {
                _enhancementButton.isActive = false;
            }
        }

        switch (type)
        {
            case EquipmentData_scr.equipmentType.MainWeapon__Cannon:
            case EquipmentData_scr.equipmentType.MainWeapon__Laser:
            case EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun:
                EquipmentData_scr.equipmentData.selectedMainWeaponName = type;
                break;

            case EquipmentData_scr.equipmentType.SubWeapon__Balkan:
            case EquipmentData_scr.equipmentType.SubWeapon__Missile:
                EquipmentData_scr.equipmentData.selectedSubWeaponName = type;
                break;

            case EquipmentData_scr.equipmentType.Shield__Heavy:
            case EquipmentData_scr.equipmentType.Shield__Light:
                EquipmentData_scr.equipmentData.selectedShieldName = type;
                break;

            default:
                break;
        }

        int _sumWeight
            = EquipmentData_scr.equipmentData.equipmentStatus
                [EquipmentData_scr.equipmentData.selectedMainWeaponName]
                [EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentData.selectedMainWeaponName]]
                [EquipmentData_scr.equipmentParameter.Weight]
            + EquipmentData_scr.equipmentData.equipmentStatus
                [EquipmentData_scr.equipmentData.selectedSubWeaponName]
                [EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentData.selectedSubWeaponName]]
                [EquipmentData_scr.equipmentParameter.Weight]
            + EquipmentData_scr.equipmentData.equipmentStatus
                [EquipmentData_scr.equipmentType.Bomb]
                [EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Bomb]]
                [EquipmentData_scr.equipmentParameter.Weight]
            + EquipmentData_scr.equipmentData.equipmentStatus
                [EquipmentData_scr.equipmentData.selectedShieldName]
                [EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentData.selectedShieldName]]
                [EquipmentData_scr.equipmentParameter.Weight];

        _weight__Status.text = $"{_sumWeight}kg";
        _damageReductionRate__Status.text = $"{EquipmentData_scr.equipmentData.equipmentStatus[EquipmentData_scr.equipmentData.selectedShieldName][EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentData.selectedShieldName]][EquipmentData_scr.equipmentParameter.DamageReductionRate]}%";
    }

    /// <summary>
    /// 武器・ボム・シールドのレベルが最大値かどうかを返す
    /// </summary>
    /// <returns></returns>
    private bool IsMaxLevel()
    {
        switch(type)
        {
            case EquipmentData_scr.equipmentType.MainWeapon__Cannon:
            case EquipmentData_scr.equipmentType.MainWeapon__Laser:
            case EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun:
                if (EquipmentData_scr.equipmentData.equipmentLevel[type] == EquipmentData_scr.level.Level5) return true;
                return false;

            default:
                if (EquipmentData_scr.equipmentData.equipmentLevel[type] == EquipmentData_scr.level.Level3) return true;
                return false;
        }
    }
}
