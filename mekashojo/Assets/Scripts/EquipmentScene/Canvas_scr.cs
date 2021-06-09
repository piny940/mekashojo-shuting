using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_scr : MonoBehaviour
{
    [SerializeField, Header("PopupBackgroundImage������")] private PopupBackgroundImage_scr _popupBackgroundImage;
    [SerializeField, Header("WeaponDescription������")] private WeaponDescriptions_scr _weaponDescriptions;
    [SerializeField, Header("MotionPreview������")] private MotionPreview_scr _motionPreview;
    [SerializeField, Header("Level__Title������")] private Level__Title_scr _level__Title;
    [SerializeField, Header("Level������")] private Level_scr _level;
    [SerializeField, Header("EnhancementMaterialsCount_Title������")] private EnhancementMaterialsCount_Title_scr _enhancementMaterialsCount_Title;
    [SerializeField, Header("EnhancementMaterialsCount������")] private EnhancementMaterialsCount_scr _enhancementMaterialsCount;
    [SerializeField, Header("EnhancementButton������")] private EnhancementButton_scr _enhancementButton;

    // ���ׂĂ̕���I���{�^����4���̍��W
    public Dictionary<Vector3[], KeyValuePair<EquipmentData_scr.equipmentType, Action>> equipmentSelectButtonCorners
        = new Dictionary<Vector3[], KeyValuePair<EquipmentData_scr.equipmentType, Action>>();

    private EquipmentData_scr.equipmentType _lastDisplayedEquipmentType;    // �Ō�ɕ\�����ꂽ����̖��O
    private bool _isFirst = true;   // ���[�h��ŏ��̕���\�����ǂ����𔻒�

    /// <summary>
    /// ���[���h��Ԃł̃}�E�X���W
    /// </summary>
    private Vector3 _mousePosition
    {
        get {
            Vector3 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mousePosition.z = 0;
            return _mousePosition;
        }
    }
    
    void Start()
    {
        // _lastDisplayedEquipmentType���������B�K���ɁA�I�𒆂̃��C�������ŏ��������Ă���B
        _lastDisplayedEquipmentType = EquipmentData_scr.equipmentData.selectedMainWeaponName;

        // �p�[�c�������̑S�Ă�UI�v�f���\��
        _popupBackgroundImage.isVisible = false;
        _weaponDescriptions.isVisible = false;
        _motionPreview.isVisible = false;
        _level__Title.isVisible = false;
        _level.isVisible = false;
        _enhancementMaterialsCount_Title.isVisible = false;
        _enhancementMaterialsCount.isVisible = false;
        _enhancementButton.isVisible = false;
    }

    void Update()
    {
        // �S�Ă̕���I���{�^����4���̍��W�ɂ��āA�}�E�X���W�Ɣ�r���ă}�E�X���ǂ̃{�^����ɂ��邩���肷��B
        // equipmentSelectButtonCorners�ɑS�Ă̕���I���{�^���̍��W�����肫��O����ȉ��̃R�[�h�����s����邱�Ƃɗ���
        foreach (KeyValuePair<Vector3[], KeyValuePair<EquipmentData_scr.equipmentType, Action>> corners in equipmentSelectButtonCorners)
        {
            // �}�E�X���{�^����ɂ��邩�ǂ����𔻒�
            if (_mousePosition.x > corners.Key[0].x && _mousePosition.x < corners.Key[2].x && _mousePosition.y > corners.Key[0].y && _mousePosition.y < corners.Key[2].y)
            {
                // �p�[�c���������X�V�����͈̂ȉ��̏ꍇ�̂�
                // ���[�h���x���p�[�c���������\������Ă��Ȃ��ꍇ
                // �Ō�ɕ\�����ꂽ����ƌ��ݕ\������ΏۂɂȂ��Ă��镐�킪�قȂ��Ă���ꍇ
                if (_isFirst || _lastDisplayedEquipmentType != corners.Value.Key)
                {
                    // ���[�h��ŏ��̕\���ł���ꍇ�́A�p�[�c�������̑S�Ă�UI�v�f��\������
                    if (_isFirst)
                    {
                        _isFirst = false;
                        _popupBackgroundImage.isVisible = true;
                        _weaponDescriptions.isVisible = true;
                        _motionPreview.isVisible = true;
                        _level__Title.isVisible = true;
                        _level.isVisible = true;
                        _enhancementMaterialsCount_Title.isVisible = true;
                        _enhancementMaterialsCount.isVisible = true;
                        _enhancementButton.isVisible = true;
                    }
                    corners.Value.Value();
                    _lastDisplayedEquipmentType = corners.Value.Key;
                }
            }
        }
    }
}
