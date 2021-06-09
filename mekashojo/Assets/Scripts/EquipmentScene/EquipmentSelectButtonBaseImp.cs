using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����I���{�^���p�̊��N���X
/// </summary>
public class EquipmentSelectButtonBaseImp : ButtonBaseImp
{
    [SerializeField, Header("Canvas������")] private Canvas_scr _canvas;
    [SerializeField, Header("PreviewImage������")] private PreviewImage_scr _previewImage;
    [SerializeField, Header("WeaponDescriptions������")] private WeaponDescriptions_scr _weaponDescriptions;
    [SerializeField, Header("MotionPreview������")] private MotionPreview_scr _motionPreview;
    [SerializeField, Header("Level������")] private Level_scr _level;
    [SerializeField, Header("EnhancementMaterialsCount_Title������")] private EnhancementMaterialsCount_Title_scr _enhancementMaterialsCount_Title;
    [SerializeField, Header("EnhancementMaterialsCount������")] private EnhancementMaterialsCount_scr _enhancementMaterialsCount;
    [SerializeField, Header("EnhancementButton������")] private EnhancementButton_scr _enhancementButton;
    [SerializeField, Header("Weight__Status������")] private Weight__Status_scr _weight__Status;
    [SerializeField, Header("DamageReductionRate__Status������")] private DamageReductionRate__Status_scr _damageReductionRate__Status;

    /// <summary>
    /// ����̎��
    /// </summary>
    protected EquipmentData_scr.equipmentType type { get; set; }

    protected void Initialize()
    {
        // �e����I���{�^���̃e�L�X�g���X�V
        GetComponentInChildren<Text>().text = EquipmentData_scr.equipmentData.equipmentDisplayName[type];

        // �{�^����4���̍��W���擾
        Vector3[] corners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(corners);

        // �{�^���̍��W�Ɗ֘A����C�x���g�̏���o�^
        _canvas.equipmentSelectButtonCorners.Add(corners, new KeyValuePair<EquipmentData_scr.equipmentType, Action>(type, SelectedWeaponChanged));
    }

    /// <summary>
    /// �\������Ώۂ̕��킪�ύX���ꂽ�ۂɎ��s
    /// </summary>
    protected void SelectedWeaponChanged()
    {
        UpdateEquipmentDescriptions();
    }

    /// <summary>
    /// ���틭�����s���B<br></br>
    /// ���틭�����o���Ȃ��ꍇ(���킪�ő僌�x��/�����p�f�ނ̕s��)�̔��菈���͓����Ă��Ȃ��B�Ăяo�����Ŕ��肷��K�v������B
    /// </summary>
    public void Enhance()
    {
        // �����p�f�ނ̏�������K�v�������炷
        EquipmentData_scr.equipmentData.enhancementMaterialsCount[type]
            -= EquipmentData_scr.equipmentData.equipmentStatus[type][EquipmentData_scr.equipmentData.equipmentLevel[type]][EquipmentData_scr.equipmentParameter.RequiredEnhancementMaterialsCount];
        // ����̃��x����1�グ��
        EquipmentData_scr.equipmentData.equipmentLevel[type]++;
        // �\�����̍X�V
        UpdateEquipmentDescriptions();
    }

    /// <summary>
    /// �\�����̍X�V
    /// </summary>
    private void UpdateEquipmentDescriptions()
    {
        // �����p�{�^���̉����C�x���g�ɁA���̕���̋������W�b�N��o�^����B
        _enhancementButton.EnhanceAction = Enhance;

        // �v���C���[�̃v���r���[�\����ʂƕ��탂�[�V�����\����ʂ́A�����������B
        #region
        var _random = UnityEngine.Random.insideUnitSphere;
        _previewImage.color = new Color(_random.x, _random.y, _random.z);

        _random = UnityEngine.Random.insideUnitSphere;
        _motionPreview.color = new Color(_random.x, _random.y, _random.z);
        #endregion

        _weaponDescriptions.text = EquipmentData_scr.equipmentData.equipmentDescriptions[type];

        _level.text = EquipmentData_scr.equipmentData.levelDisplayName[EquipmentData_scr.equipmentData.equipmentLevel[type]];

        _enhancementMaterialsCount_Title.text = $"{EquipmentData_scr.equipmentData.equipmentDisplayName[type]}�����f��";

        if (IsMaxLevel())
        {
            // ���킪�ő僌�x���Ȃ̂ŁA���̒ʒm�Ƌ����p�{�^���̔�A�N�e�B�u�����s��
            _enhancementMaterialsCount.text = "����ȏ㋭���ł��܂���";
            _enhancementButton.isActive = false;
        }
        else
        {
            _enhancementMaterialsCount.text = $"{EquipmentData_scr.equipmentData.enhancementMaterialsCount[type]} / {EquipmentData_scr.equipmentData.equipmentStatus[type][EquipmentData_scr.equipmentData.equipmentLevel[type]][EquipmentData_scr.equipmentParameter.RequiredEnhancementMaterialsCount]}";

            // �����p�f�ނ��s�����Ă���ꍇ�́A�����{�^�����A�N�e�B�u��
            if (EquipmentData_scr.equipmentData.enhancementMaterialsCount[type] >= EquipmentData_scr.equipmentData.equipmentStatus[type][EquipmentData_scr.equipmentData.equipmentLevel[type]][EquipmentData_scr.equipmentParameter.RequiredEnhancementMaterialsCount])
            {
                _enhancementButton.isActive = true;
            }
            else
            {
                _enhancementButton.isActive = false;
            }
        }

        // �I�𒆂̕���̏����X�V
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
    /// ����E�{���E�V�[���h�̃��x�����ő�l���ǂ�����Ԃ�
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
