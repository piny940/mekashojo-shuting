using System;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class Canvas : MonoBehaviour
    {
        [SerializeField, Header("PopupBackgroundImageを入れる")] private PopupBackgroundImage _popupBackgroundImage;
        [SerializeField, Header("WeaponDescriptionsFrameを入れる")] private WeaponDescriptionsFrame _weaponDescriptionsFrame;
        [SerializeField, Header("PreviewImageFrameを入れる")] private PreviewImageFrame _previewImageFrame;
        [SerializeField, Header("PreviewImageModelを入れる")] private PreviewImageModel _previewImageModel;
        [SerializeField, Header("Level__Titleを入れる")] private Level__Title _level__Title;
        [SerializeField, Header("Levelを入れる")] private Level _level;
        [SerializeField, Header("EnhancementMaterialsCount_Titleを入れる")] private EnhancementMaterialsCount_Title _enhancementMaterialsCount_Title;
        [SerializeField, Header("EnhancementMaterialsCountを入れる")] private EnhancementMaterialsCount _enhancementMaterialsCount;
        [SerializeField, Header("EnhancementButtonを入れる")] private EnhancementButton _enhancementButton;

        // すべての武器選択ボタンの4隅の座標
        public Dictionary<Vector3[], KeyValuePair<Model.EquipmentData.equipmentType, Action>> equipmentSelectButtonCorners
            = new Dictionary<Vector3[], KeyValuePair<Model.EquipmentData.equipmentType, Action>>();

        private Model.EquipmentData.equipmentType _lastDisplayedEquipmentType;    // 最後に表示された武器の名前
        private bool _isFirst = true;   // ロード後最初の武器表示かどうかを判定

        /// <summary>
        /// ワールド空間でのマウス座標
        /// </summary>
        private Vector3 _mousePosition
        {
            get
            {
                Vector3 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _mousePosition.z = 0;
                return _mousePosition;
            }
        }

        void Start()
        {
            // _lastDisplayedEquipmentTypeを初期化。適当に、選択中のメイン装備で初期化している。
            _lastDisplayedEquipmentType = Model.EquipmentData.equipmentData.selectedMainWeaponName;

            // パーツ説明欄の全てのUI要素を非表示
            _popupBackgroundImage.isVisible = false;
            _weaponDescriptionsFrame.isVisible = false;
            _previewImageFrame.isVisible = false;
            _previewImageModel.isVisible = false;
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
            foreach (KeyValuePair<Vector3[], KeyValuePair<Model.EquipmentData.equipmentType, Action>> corners in equipmentSelectButtonCorners)
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
                            _weaponDescriptionsFrame.isVisible = true;
                            _previewImageFrame.isVisible = true;
                            _previewImageModel.isVisible = true;
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
}
