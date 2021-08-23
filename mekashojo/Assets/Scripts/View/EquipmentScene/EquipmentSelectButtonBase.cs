using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    /// <summary>
    /// 武器選択ボタン用の基底クラス
    /// </summary>
    public class EquipmentSelectButtonBase : ButtonBase
    {
        [SerializeField, Header("SelectedWeaponManagerを入れる")] private SelectedWeaponManager _selectedWeaponManager;
        [SerializeField, Header("Canvasを入れる")] private Canvas _canvas;
        [SerializeField, Header("PreviewImageを入れる")] private PreviewImage _previewImage;
        [SerializeField, Header("WeaponDescriptionsを入れる")] private WeaponDescriptions _weaponDescriptions;
        [SerializeField, Header("MotionPreviewを入れる")] private MotionPreview _motionPreview;
        [SerializeField, Header("Levelを入れる")] private Level _level;
        [SerializeField, Header("EnhancementMaterialsCount_Titleを入れる")] private EnhancementMaterialsCount_Title _enhancementMaterialsCount_Title;
        [SerializeField, Header("EnhancementMaterialsCountを入れる")] private EnhancementMaterialsCount _enhancementMaterialsCount;
        [SerializeField, Header("EnhancementButtonを入れる")] private EnhancementButton _enhancementButton;
        [SerializeField, Header("Weight__Statusを入れる")] private Weight__Status _weight__Status;
        [SerializeField, Header("DamageReductionRate__Statusを入れる")] private DamageReductionRate__Status _damageReductionRate__Status;

        /// <summary>
        /// 武器の種類
        /// </summary>
        protected Model.EquipmentData.equipmentType type { get; set; }

        public bool isVisible
        {
            get { return this.gameObject.activeSelf; }
            set { this.gameObject.SetActive(value); }
        }

        protected void Initialize()
        {
            // 各武器選択ボタンのテキストを更新
            GetComponentInChildren<Text>().text = Model.EquipmentData.equipmentData.equipmentDisplayName[type];

            // ボタンの4隅の座標を取得
            Vector3[] corners = new Vector3[4];
            GetComponent<RectTransform>().GetWorldCorners(corners);

            // ボタンの座標と関連するイベントの情報を、それが未登録であった場合は登録する
            if (!_canvas.equipmentSelectButtonCorners.ContainsKey(corners)) _canvas.equipmentSelectButtonCorners.Add(corners, new KeyValuePair<Model.EquipmentData.equipmentType, Action>(type, DisplayWeaponChanged));

            if (type == Model.EquipmentData.equipmentData.selectedMainWeaponName
                || type == Model.EquipmentData.equipmentData.selectedSubWeaponName
                || type == Model.EquipmentData.equipmentData.selectedShieldName)
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
            Model.EquipmentData.equipmentData.enhancementMaterialsCount[type]
                -= Model.EquipmentData.equipmentData.equipmentStatus[type][Model.EquipmentData.equipmentData.equipmentLevel[type]][Model.EquipmentData.equipmentParameter.RequiredEnhancementMaterialsCount];
            // 武器のレベルを1上げる
            Model.EquipmentData.equipmentData.equipmentLevel[type]++;
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

            _weaponDescriptions.text = Model.EquipmentData.equipmentData.equipmentDescriptions[type];

            _level.text = Model.EquipmentData.equipmentData.levelDisplayName[Model.EquipmentData.equipmentData.equipmentLevel[type]];

            _enhancementMaterialsCount_Title.text = $"{Model.EquipmentData.equipmentData.equipmentDisplayName[type]}強化素材";

            if (IsMaxLevel())
            {
                // 武器が最大レベルなので、その通知と強化用ボタンの非アクティブ化を行う
                _enhancementMaterialsCount.text = "これ以上強化できません";
                _enhancementButton.isActive = false;
            }
            else
            {
                _enhancementMaterialsCount.text = $"{Model.EquipmentData.equipmentData.enhancementMaterialsCount[type]} / {Model.EquipmentData.equipmentData.equipmentStatus[type][Model.EquipmentData.equipmentData.equipmentLevel[type]][Model.EquipmentData.equipmentParameter.RequiredEnhancementMaterialsCount]}";

                // 強化用素材が不足している場合は、強化ボタンを非アクティブ化
                if (Model.EquipmentData.equipmentData.enhancementMaterialsCount[type] >= Model.EquipmentData.equipmentData.equipmentStatus[type][Model.EquipmentData.equipmentData.equipmentLevel[type]][Model.EquipmentData.equipmentParameter.RequiredEnhancementMaterialsCount])
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
                = Model.EquipmentData.equipmentData.equipmentStatus
                    [Model.EquipmentData.equipmentData.selectedMainWeaponName]
                    [Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentData.selectedMainWeaponName]]
                    [Model.EquipmentData.equipmentParameter.Weight]
                + Model.EquipmentData.equipmentData.equipmentStatus
                    [Model.EquipmentData.equipmentData.selectedSubWeaponName]
                    [Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentData.selectedSubWeaponName]]
                    [Model.EquipmentData.equipmentParameter.Weight]
                + Model.EquipmentData.equipmentData.equipmentStatus
                    [Model.EquipmentData.equipmentType.Bomb]
                    [Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.Bomb]]
                    [Model.EquipmentData.equipmentParameter.Weight]
                + Model.EquipmentData.equipmentData.equipmentStatus
                    [Model.EquipmentData.equipmentData.selectedShieldName]
                    [Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentData.selectedShieldName]]
                    [Model.EquipmentData.equipmentParameter.Weight];

            _weight__Status.text = $"{_sumWeight}kg";
            _damageReductionRate__Status.text = $"{Model.EquipmentData.equipmentData.equipmentStatus[Model.EquipmentData.equipmentData.selectedShieldName][Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentData.selectedShieldName]][Model.EquipmentData.equipmentParameter.DamageReductionRate]}%";
        }

        /// <summary>
        /// 武器・ボム・シールドのレベルが最大値かどうかを返す
        /// </summary>
        /// <returns></returns>
        private bool IsMaxLevel()
        {
            switch (type)
            {
                case Model.EquipmentData.equipmentType.MainWeapon__Cannon:
                case Model.EquipmentData.equipmentType.MainWeapon__Laser:
                case Model.EquipmentData.equipmentType.MainWeapon__BeamMachineGun:
                    if (Model.EquipmentData.equipmentData.equipmentLevel[type] == Model.EquipmentData.level.Level5) return true;
                    return false;

                default:
                    if (Model.EquipmentData.equipmentData.equipmentLevel[type] == Model.EquipmentData.level.Level3) return true;
                    return false;
            }
        }
    }
}
