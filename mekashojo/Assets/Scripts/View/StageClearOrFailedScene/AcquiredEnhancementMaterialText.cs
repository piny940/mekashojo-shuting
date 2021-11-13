using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class AcquiredEnhancementMaterialText : MonoBehaviour
    {
        [SerializeField, Header("Cannonを入れる")] private Text _cannonText;
        [SerializeField, Header("Laserを入れる")] private Text _laserText;
        [SerializeField, Header("BeamMachineGunを入れる")] private Text _beamMachineGunText;
        [SerializeField, Header("Balkanを入れる")] private Text _balkanText;
        [SerializeField, Header("Missileを入れる")] private Text _missileText;
        [SerializeField, Header("Bombを入れる")] private Text _bombText;
        [SerializeField, Header("HeavyShieldを入れる")] private Text _heavyShieldText;
        [SerializeField, Header("LightShieldを入れる")] private Text _lightShieldText;

        // 入手した強化用素材の数
        private Dictionary<Model.EquipmentData.equipmentType, int> _data;

        // Start is called before the first frame update
        void Start()
        {
            _data = Controller.BattleScenesController.acquiredEnhancementMaterialData.acquiredEnhancementMaterialsCount;

            if (_data == null) return;

            //Textの内容を更新する
            _cannonText.text = $"キャノン強化素材:{_data[Model.EquipmentData.equipmentType.MainWeapon__Cannon]}個";
            _laserText.text = $"レーザー強化素材:{_data[Model.EquipmentData.equipmentType.MainWeapon__Laser]}個";
            _beamMachineGunText.text = $"ビームマシンガン強化素材:{_data[Model.EquipmentData.equipmentType.MainWeapon__BeamMachineGun]}個";
            _balkanText.text = $"バルカン強化素材:{_data[Model.EquipmentData.equipmentType.SubWeapon__Balkan]}個";
            _missileText.text = $"ミサイル強化素材:{_data[Model.EquipmentData.equipmentType.SubWeapon__Missile]}個";
            _bombText.text = $"ボム強化素材:{_data[Model.EquipmentData.equipmentType.Bomb]}個";
            _heavyShieldText.text = $"重シールド強化素材:{_data[Model.EquipmentData.equipmentType.Shield__Heavy]}個";
            _lightShieldText.text = $"軽シールド強化素材:{_data[Model.EquipmentData.equipmentType.Shield__Light]}個";
        }
    }
}
