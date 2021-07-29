using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 武器選択ボタン用の基底クラス
/// </summary>
public class EquipmentSelectButtonBaseImp : ButtonBaseImp
{
    [SerializeField, Header("SelectedWeaponManagerを入れる")] private SelectedWeaponManager_scr _selectedWeaponManager;
    [SerializeField, Header("Canvasを入れる")] private Canvas_scr _canvas;
    [SerializeField, Header("PreviewImageを入れる")] private PreviewImage_scr _previewImage;
    [SerializeField, Header("WeaponDescriptionsを入れる")] private WeaponDescriptions_scr _weaponDescriptions;
    [SerializeField, Header("MotionPreviewを入れる")] private MotionPreview_scr _motionPreview;
    [SerializeField, Header("Levelを入れる")] private Level_scr _level;
    [SerializeField, Header("EnhancementMaterialsCount_Titleを入れる")] private EnhancementMaterialsCount_Title_scr _enhancementMaterialsCount_Title;
    [SerializeField, Header("EnhancementMaterialsCountを入れる")] private EnhancementMaterialsCount_scr _enhancementMaterialsCount;
    [SerializeField, Header("EnhancementButtonを入れる")] private EnhancementButton_scr _enhancementButton;
    [SerializeField, Header("Weight__Statusを入れる")] private Weight__Status_scr _weight__Status;
    [SerializeField, Header("DamageReductionRate__Statusを入れる")] private DamageReductionRate__Status_scr _damageReductionRate__Status;

    /// <summary>
    /// 武器の種類
    /// </summary>
    protected EquipmentData_scr.equipmentType type { get; set; }

    public bool isVisible
    {
        get { return this.gameObject.activeSelf; }
        set { this.gameObject.SetActive(value); }
    }

    protected void Initialize()
    {
        // 各武器選択ボタンのテキストを更新
        GetComponentInChildren<Text>().text = EquipmentData_scr.equipmentData.equipmentDisplayName[type];

        // ボタンの4隅の座標を取得
        Vector3[] corners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(corners);

        // ボタンの座標と関連するイベントの情報を、それが未登録であった場合は登録する
        if (!_canvas.equipmentSelectButtonCorners.ContainsKey(corners)) _canvas.equipmentSelectButtonCorners.Add(corners, new KeyValuePair<EquipmentData_scr.equipmentType, Action>(type, DisplayWeaponChanged));

        if (type == EquipmentData_scr.equipmentData.selectedMainWeaponName
            || type == EquipmentData_scr.equipmentData.selectedSubWeaponName
            || type == EquipmentData_scr.equipmentData.selectedShieldName)
        {
            _selectedWeaponManager.NotifySelectedWeaponChanged(type);
        }
    }

    /// <summary>
    /// 選択中の武器が変更された際に実行
    /// </summary>
    protected void SelectedWeaponChanged()
    {
        UpdateEquipmentDescriptions();
        UpdateSelectedEquipmentStatus();
    }

    /// <summary>
    /// 表示中の武器が変更された際に実行
    /// </summary>
    protected void DisplayWeaponChanged()
    {
        UpdateEquipmentDescriptions();
    }

    /// <summary>
    /// 武器強化を行う。<br></br>
    /// 武器強化が出来ない場合(武器が最大レベル/強化用素材の不足)の判定処理は入っていない。呼び出し側で判定する必要がある。
    /// </summary>
    public void Enhance()
    {
        // 強化用素材の所持数を必要数分減らす
        EquipmentData_scr.equipmentData.enhancementMaterialsCount[type]
            -= EquipmentData_scr.equipmentData.equipmentStatus[type][EquipmentData_scr.equipmentData.equipmentLevel[type]][EquipmentData_scr.equipmentParameter.RequiredEnhancementMaterialsCount];
        // 武器のレベルを1上げる
        EquipmentData_scr.equipmentData.equipmentLevel[type]++;
        // 表示情報の更新
        UpdateEquipmentDescriptions();
    }

    /// <summary>
    /// 表示情報の更新
    /// </summary>
    private void UpdateEquipmentDescriptions()
    {
        // 強化用ボタンの押下イベントに、この武器の強化ロジックを登録する。
        _enhancementButton.EnhanceAction = Enhance;

        // プレイヤーのプレビュー表示画面と武器モーション表示画面は、今後実装する。
        #region
        var _random = UnityEngine.Random.insideUnitSphere;
        _previewImage.color = new Color(_random.x, _random.y, _random.z);

        _random = UnityEngine.Random.insideUnitSphere;
        _motionPreview.color = new Color(_random.x, _random.y, _random.z);
        #endregion

        _weaponDescriptions.text = EquipmentData_scr.equipmentData.equipmentDescriptions[type];

        _level.text = EquipmentData_scr.equipmentData.levelDisplayName[EquipmentData_scr.equipmentData.equipmentLevel[type]];

        _enhancementMaterialsCount_Title.text = $"{EquipmentData_scr.equipmentData.equipmentDisplayName[type]}強化素材";

        if (IsMaxLevel())
        {
            // 武器が最大レベルなので、その通知と強化用ボタンの非アクティブ化を行う
            _enhancementMaterialsCount.text = "これ以上強化できません";
            _enhancementButton.isActive = false;
        }
        else
        {
            _enhancementMaterialsCount.text = $"{EquipmentData_scr.equipmentData.enhancementMaterialsCount[type]} / {EquipmentData_scr.equipmentData.equipmentStatus[type][EquipmentData_scr.equipmentData.equipmentLevel[type]][EquipmentData_scr.equipmentParameter.RequiredEnhancementMaterialsCount]}";

            // 強化用素材が不足している場合は、強化ボタンを非アクティブ化
            if (EquipmentData_scr.equipmentData.enhancementMaterialsCount[type] >= EquipmentData_scr.equipmentData.equipmentStatus[type][EquipmentData_scr.equipmentData.equipmentLevel[type]][EquipmentData_scr.equipmentParameter.RequiredEnhancementMaterialsCount])
            {
                _enhancementButton.isActive = true;
            }
            else
            {
                _enhancementButton.isActive = false;
            }
        }
    }

    /// <summary>
    /// 選択中の武器の更新と、ステータスの更新を行う
    /// </summary>
    private void UpdateSelectedEquipmentStatus()
    {
        // 選択中の武器の情報を更新
        _selectedWeaponManager.NotifySelectedWeaponChanged(type);

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
        switch (type)
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
