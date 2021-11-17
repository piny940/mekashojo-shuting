using UnityEngine;

namespace View
{
    [System.Serializable]
    public class SavedData
    {
        //選択中の装備のデータ
        public Model.EquipmentData.equipmentType selectedMainWeaponName;
        public Model.EquipmentData.equipmentType selectedSubWeaponName;
        public Model.EquipmentData.equipmentType selectedShieldName;

        //装備のレベルのデータ
        public Model.EquipmentData.level cannonLevel;
        public Model.EquipmentData.level laserLevel;
        public Model.EquipmentData.level beamMachineGunLevel;
        public Model.EquipmentData.level balkanLevel;
        public Model.EquipmentData.level missileLevel;
        public Model.EquipmentData.level bombLevel;
        public Model.EquipmentData.level heavyShieldLevel;
        public Model.EquipmentData.level lightShieldLevel;

        //強化素材の所持数のデータ
        public int enhancementMaterialsCount__Cannon;
        public int enhancementMaterialsCount__Laser;
        public int enhancementMaterialsCount__BeamMachineGun;
        public int enhancementMaterialsCount__Balkan;
        public int enhancementMaterialsCount__Missile;
        public int enhancementMaterialsCount__Bomb;
        public int enhancementMaterialsCount__HeavyShield;
        public int enhancementMaterialsCount__LightShield;

        //ステージの進捗のデータ
        public Model.ProgressData.stageName stageClearAchievement;

        //設定のデータ
        public float bgmVolume;
        public float seVolume;
        public float mouseSensitivity;
        public char forwardKey;
        public char backKey;
        public char leftKey;
        public char rightKey;
    }

    public class SaveDataManager : MonoBehaviour
    {
        public static SaveDataManager saveDataManager = new SaveDataManager();
        private SavedData _savedData = new SavedData();
        private string _saveData__JsonString;   //セーブデータをstring型で保存

        /// <summary>
        /// データをセーブする
        /// </summary>
        public void Save()
        {
            //選択中の装備の保存
            _savedData.selectedMainWeaponName = Model.EquipmentData.equipmentData.selectedMainWeaponName;
            _savedData.selectedSubWeaponName = Model.EquipmentData.equipmentData.selectedSubWeaponName;
            _savedData.selectedShieldName = Model.EquipmentData.equipmentData.selectedShieldName;

            //装備のレベルの保存
            _savedData.cannonLevel = Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.MainWeapon__Cannon];
            _savedData.laserLevel = Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.MainWeapon__Laser];
            _savedData.beamMachineGunLevel = Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.MainWeapon__BeamMachineGun];
            _savedData.balkanLevel = Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.SubWeapon__Balkan];
            _savedData.missileLevel = Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.SubWeapon__Missile];
            _savedData.bombLevel = Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.Bomb];
            _savedData.heavyShieldLevel = Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.Shield__Heavy];
            _savedData.lightShieldLevel = Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.Shield__Light];

            //強化素材の所持数の保存
            _savedData.enhancementMaterialsCount__Cannon = Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.MainWeapon__Cannon];
            _savedData.enhancementMaterialsCount__Laser = Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.MainWeapon__Laser];
            _savedData.enhancementMaterialsCount__BeamMachineGun = Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.MainWeapon__BeamMachineGun];
            _savedData.enhancementMaterialsCount__Balkan = Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.SubWeapon__Balkan];
            _savedData.enhancementMaterialsCount__Bomb = Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.Bomb];
            _savedData.enhancementMaterialsCount__HeavyShield = Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.Shield__Heavy];
            _savedData.enhancementMaterialsCount__LightShield = Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.Shield__Light];

            //ステージの進捗の保存
            _savedData.stageClearAchievement = Model.ProgressData.progressData.stageClearAchievement;

            //設定のデータの保存
            _savedData.bgmVolume = Model.Setting.bgmVolume;
            _savedData.seVolume = Model.Setting.seVolume;
            _savedData.mouseSensitivity = Model.Setting.mouseSensitivity;
            _savedData.forwardKey = Model.Setting.forwardKey;
            _savedData.backKey = Model.Setting.backKey;
            _savedData.leftKey = Model.Setting.leftKey;
            _savedData.rightKey = Model.Setting.rightKey;

            _saveData__JsonString = JsonUtility.ToJson(_savedData);
            PlayerPrefs.SetString("SaveData", _saveData__JsonString);
        }

        /// <summary>
        /// セーブデータをロードする
        /// セーブデータがあればtrueを返し、なければfalseを返す
        /// </summary>
        public bool Load()
        {
            _saveData__JsonString = PlayerPrefs.GetString("SaveData");

            //セーブデータがなかった場合の処理
            if (_saveData__JsonString == "")
            {
                return false;
            }

            //セーブデータがあった場合の処理
            _savedData = JsonUtility.FromJson<SavedData>(_saveData__JsonString);

            //選択中の装備のロード
            Model.EquipmentData.equipmentData.selectedMainWeaponName = _savedData.selectedMainWeaponName;
            Model.EquipmentData.equipmentData.selectedSubWeaponName = _savedData.selectedSubWeaponName;
            Model.EquipmentData.equipmentData.selectedShieldName = _savedData.selectedShieldName;

            //装備のレベルのロード
            Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.MainWeapon__Cannon] = _savedData.cannonLevel;
            Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.MainWeapon__Laser] = _savedData.laserLevel;
            Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.MainWeapon__BeamMachineGun] = _savedData.beamMachineGunLevel;
            Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.SubWeapon__Balkan] = _savedData.balkanLevel;
            Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.SubWeapon__Missile] = _savedData.missileLevel;
            Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.Bomb] = _savedData.bombLevel;
            Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.Shield__Heavy] = _savedData.heavyShieldLevel;
            Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.Shield__Light] = _savedData.lightShieldLevel;

            //強化素材の所持数のロード
            Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.MainWeapon__Cannon] = _savedData.enhancementMaterialsCount__Cannon;
            Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.MainWeapon__Laser] = _savedData.enhancementMaterialsCount__Laser;
            Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.MainWeapon__BeamMachineGun] = _savedData.enhancementMaterialsCount__BeamMachineGun;
            Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.SubWeapon__Balkan] = _savedData.enhancementMaterialsCount__Balkan;
            Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.Bomb] = _savedData.enhancementMaterialsCount__Bomb;
            Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.Shield__Heavy] = _savedData.enhancementMaterialsCount__HeavyShield;
            Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.Shield__Light] = _savedData.enhancementMaterialsCount__LightShield;

            //ステージの進捗のロード
            Model.ProgressData.progressData.stageClearAchievement = _savedData.stageClearAchievement;

            //設定のデータのロード
            Model.Setting.bgmVolume = _savedData.bgmVolume;
            Model.Setting.seVolume = _savedData.seVolume;
            Model.Setting.mouseSensitivity = _savedData.mouseSensitivity;
            Model.Setting.forwardKey = _savedData.forwardKey;
            Model.Setting.backKey = _savedData.backKey;
            Model.Setting.leftKey = _savedData.leftKey;
            Model.Setting.rightKey = _savedData.rightKey;

            return true;
        }

        /// <summary>
        /// データの初期化
        /// </summary>
        public void Initialize()
        {
            //選択中の装備の初期化
            Model.EquipmentData.equipmentData.selectedMainWeaponName = Model.EquipmentData.equipmentType.MainWeapon__Cannon;
            Model.EquipmentData.equipmentData.selectedSubWeaponName = Model.EquipmentData.equipmentType.SubWeapon__Balkan;
            Model.EquipmentData.equipmentData.selectedShieldName = Model.EquipmentData.equipmentType.Shield__Heavy;

            //装備のレベルの初期化
            Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.MainWeapon__Cannon] = Model.EquipmentData.level.Level1;
            Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.MainWeapon__Laser] = Model.EquipmentData.level.Level1;
            Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.MainWeapon__BeamMachineGun] = Model.EquipmentData.level.Level1;
            Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.SubWeapon__Balkan] = Model.EquipmentData.level.Level1;
            Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.SubWeapon__Missile] = Model.EquipmentData.level.Level1;
            Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.Bomb] = Model.EquipmentData.level.Level1;
            Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.Shield__Heavy] = Model.EquipmentData.level.Level1;
            Model.EquipmentData.equipmentData.equipmentLevel[Model.EquipmentData.equipmentType.Shield__Light] = Model.EquipmentData.level.Level1;

            //強化素材の所持数の初期化
            Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.MainWeapon__Cannon] = 0;
            Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.MainWeapon__Laser] = 0;
            Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.MainWeapon__BeamMachineGun] = 0;
            Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.SubWeapon__Balkan] = 0;
            Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.Bomb] = 0;
            Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.Shield__Heavy] = 0;
            Model.EquipmentData.equipmentData.enhancementMaterialsCount[Model.EquipmentData.equipmentType.Shield__Light] = 0;

            // ステージの進捗の初期化
            // Stage1を仕様削除したため、初めからステージ1をクリアしている状態としてスタートする
            Model.ProgressData.progressData.stageClearAchievement = Model.ProgressData.stageName.stage1;

            //設定のデータの初期化
            Model.Setting.bgmVolume = _savedData.bgmVolume;
            Model.Setting.seVolume = _savedData.seVolume;
            Model.Setting.mouseSensitivity = _savedData.mouseSensitivity;
            Model.Setting.forwardKey = _savedData.forwardKey;
            Model.Setting.backKey = _savedData.backKey;
            Model.Setting.leftKey = _savedData.leftKey;
            Model.Setting.rightKey = _savedData.rightKey;

            Save();
        }
    }
}
