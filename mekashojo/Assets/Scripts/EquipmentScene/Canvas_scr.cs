using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_scr : MonoBehaviour
{
    [SerializeField, Header("PopupBackgroundImageを入れる")] private PopupBackgroundImage_scr _popupBackgroundImage;
    [SerializeField, Header("WeaponDescriptionを入れる")] private WeaponDescriptions_scr _weaponDescriptions;
    [SerializeField, Header("MotionPreviewを入れる")] private MotionPreview_scr _motionPreview;
    [SerializeField, Header("Level__Titleを入れる")] private Level__Title_scr _level__Title;
    [SerializeField, Header("Levelを入れる")] private Level_scr _level;
    [SerializeField, Header("EnhancementMaterialsCount_Titleを入れる")] private EnhancementMaterialsCount_Title_scr _enhancementMaterialsCount_Title;
    [SerializeField, Header("EnhancementMaterialsCountを入れる")] private EnhancementMaterialsCount_scr _enhancementMaterialsCount;
    [SerializeField, Header("EnhancementButtonを入れる")] private EnhancementButton_scr _enhancementButton;

    // すべての武器選択ボタンの4隅の座標
    public Dictionary<Vector3[], KeyValuePair<EquipmentData_scr.equipmentType, Action>> equipmentSelectButtonCorners
        = new Dictionary<Vector3[], KeyValuePair<EquipmentData_scr.equipmentType, Action>>();

    private EquipmentData_scr.equipmentType _lastDisplayedEquipmentType;    // 最後に表示された武器の名前
    private bool _isFirst = true;   // ロード後最初の武器表示かどうかを判定

    /// <summary>
    /// ワールド空間でのマウス座標
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
        // _lastDisplayedEquipmentTypeを初期化。適当に、選択中のメイン装備で初期化している。
        _lastDisplayedEquipmentType = EquipmentData_scr.equipmentData.selectedMainWeaponName;

        // パーツ説明欄の全てのUI要素を非表示
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
        // 全ての武器選択ボタンの4隅の座標について、マウス座標と比較してマウスがどのボタン上にあるか判定する。
        // equipmentSelectButtonCornersに全ての武器選択ボタンの座標が入りきる前から以下のコードが実行されることに留意
        foreach (KeyValuePair<Vector3[], KeyValuePair<EquipmentData_scr.equipmentType, Action>> corners in equipmentSelectButtonCorners)
        {
            // マウスがボタン上にあるかどうかを判定
            if (_mousePosition.x > corners.Key[0].x && _mousePosition.x < corners.Key[2].x && _mousePosition.y > corners.Key[0].y && _mousePosition.y < corners.Key[2].y)
            {
                // パーツ説明欄が更新されるのは以下の場合のみ
                // ロード後一度もパーツ説明欄が表示されていない場合
                // 最後に表示された武器と現在表示する対象になっている武器が異なっている場合
                if (_isFirst || _lastDisplayedEquipmentType != corners.Value.Key)
                {
                    // ロード後最初の表示である場合は、パーツ説明欄の全てのUI要素を表示する
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
