using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_scr : MonoBehaviour
{
    [SerializeField] private PopupBackgroundImage_scr _popupBackgroundImage;
    [SerializeField] private WeaponDescriptions_scr _weaponDescriptions;
    [SerializeField] private MotionPreview_scr _motionPreview;
    [SerializeField] private Level__Title_scr _level__Title;
    [SerializeField] private Level_scr _level;
    [SerializeField] private EnhancementMaterialsCount_Title_scr _enhancementMaterialsCount_Title;
    [SerializeField] private EnhancementMaterialsCount_scr _enhancementMaterialsCount;
    [SerializeField] private EnhancementButton_scr _enhancementButton;

    public Dictionary<Vector3[], KeyValuePair<EquipmentData_scr.equipmentType, Action>> equipmentSelectButtonCorners
        = new Dictionary<Vector3[], KeyValuePair<EquipmentData_scr.equipmentType, Action>>();

    private EquipmentData_scr.equipmentType _lastDisplayedEquipmentType;
    private bool _isFirst = true;

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
        _lastDisplayedEquipmentType = EquipmentData_scr.equipmentData.selectedMainWeaponName;

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
        foreach (KeyValuePair<Vector3[], KeyValuePair<EquipmentData_scr.equipmentType, Action>> corners in equipmentSelectButtonCorners)
        {
            if (_mousePosition.x > corners.Key[0].x && _mousePosition.x < corners.Key[2].x && _mousePosition.y > corners.Key[0].y && _mousePosition.y < corners.Key[2].y)
            {
                if (_isFirst || _lastDisplayedEquipmentType != corners.Value.Key)
                {
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
