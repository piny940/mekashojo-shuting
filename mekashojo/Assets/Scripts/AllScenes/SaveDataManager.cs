using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class SavedData
    {
        //選択中の装備のデータ
        public EquipmentData_scr.equipmentType selectedMainWeaponName;
        public EquipmentData_scr.equipmentType selectedSubWeaponName;
        public EquipmentData_scr.equipmentType selectedShieldName;

        //装備のレベルのデータ
        public EquipmentData_scr.level cannonLevel;
        public EquipmentData_scr.level laserLevel;
        public EquipmentData_scr.level beamMachineGunLevel;
        public EquipmentData_scr.level balkanLevel;
        public EquipmentData_scr.level missileLevel;
        public EquipmentData_scr.level bombLevel;
        public EquipmentData_scr.level heavyShieldLevel;
        public EquipmentData_scr.level lightShieldLevel;

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
        public ProgressData_scr.stageName stageClearAchievement;

        //設定のデータ
        public float bgmVolume;
        public float seVolume;
        public float mouseSensitivity;
        public char forwardKey;
        public char backKey;
        public char leftKey;
        public char rightKey;
    }


    public class SaveDataManager
    {
        public static SaveDataManager saveDataManager = new SaveDataManager();
        private SavedData _savedData;
        private string _saveData__JsonString;   //セーブデータをstring型で保存

        /// <summary>
        /// データをセーブする
        /// </summary>
        public void Save()
        {
            //選択中の装備の保存
            _savedData.selectedMainWeaponName = EquipmentData_scr.equipmentData.selectedMainWeaponName;

            _savedData.selectedSubWeaponName = EquipmentData_scr.equipmentData.selectedSubWeaponName;

            _savedData.selectedShieldName = EquipmentData_scr.equipmentData.selectedShieldName;


            //装備のレベルの保存
            _savedData.cannonLevel = EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__Cannon];

            _savedData.laserLevel = EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__Laser];

            _savedData.beamMachineGunLevel = EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun];

            _savedData.balkanLevel = EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.SubWeapon__Balkan];

            _savedData.missileLevel = EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.SubWeapon__Missile];

            _savedData.bombLevel = EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Bomb];

            _savedData.heavyShieldLevel = EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Shield__Heavy];

            _savedData.lightShieldLevel = EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Shield__Light];


            //強化素材の所持数の保存
            _savedData.enhancementMaterialsCount__Cannon = EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__Cannon];

            _savedData.enhancementMaterialsCount__Laser = EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__Laser];

            _savedData.enhancementMaterialsCount__BeamMachineGun = EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun];

            _savedData.enhancementMaterialsCount__Balkan = EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.SubWeapon__Balkan];

            _savedData.enhancementMaterialsCount__Bomb = EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Bomb];

            _savedData.enhancementMaterialsCount__HeavyShield = EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Shield__Heavy];

            _savedData.enhancementMaterialsCount__LightShield = EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Shield__Light];


            //ステージの進捗の保存
            _savedData.stageClearAchievement = ProgressData_scr.progressData.stageClearAchievement;


            //設定のデータの保存
            _savedData.bgmVolume = Setting_scr.setting.bgmVolume;

            _savedData.seVolume = Setting_scr.setting.seVolume;

            _savedData.mouseSensitivity = Setting_scr.setting.mouseSensitivity;

            _savedData.forwardKey = Setting_scr.setting.forwardKey;

            _savedData.backKey = Setting_scr.setting.backKey;

            _savedData.leftKey = Setting_scr.setting.leftKey;

            _savedData.rightKey = Setting_scr.setting.rightKey;


            _saveData__JsonString = JsonUtility.ToJson(_savedData);
            PlayerPrefs.SetString("SaveData", _saveData__JsonString);
        }


        /// <summary>
        /// セーブデータをロードする
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
            EquipmentData_scr.equipmentData.selectedMainWeaponName = _savedData.selectedMainWeaponName;

            EquipmentData_scr.equipmentData.selectedSubWeaponName = _savedData.selectedSubWeaponName;

            EquipmentData_scr.equipmentData.selectedShieldName = _savedData.selectedShieldName;


            //装備のレベルのロード
            EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__Cannon] = _savedData.cannonLevel;

            EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__Laser] = _savedData.laserLevel;

            EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun] = _savedData.beamMachineGunLevel;

            EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.SubWeapon__Balkan] = _savedData.balkanLevel;

            EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.SubWeapon__Missile] = _savedData.missileLevel;

            EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Bomb] = _savedData.bombLevel;

            EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Shield__Heavy] = _savedData.heavyShieldLevel;

            EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Shield__Light] = _savedData.lightShieldLevel;


            //強化素材の所持数のロード
            EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__Cannon] = _savedData.enhancementMaterialsCount__Cannon;

            EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__Laser] = _savedData.enhancementMaterialsCount__Laser;

            EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun] = _savedData.enhancementMaterialsCount__BeamMachineGun;

            EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.SubWeapon__Balkan] = _savedData.enhancementMaterialsCount__Balkan;

            EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Bomb] = _savedData.enhancementMaterialsCount__Bomb;

            EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Shield__Heavy] = _savedData.enhancementMaterialsCount__HeavyShield;

            EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Shield__Light] = _savedData.enhancementMaterialsCount__LightShield;


            //ステージの進捗のロード
            ProgressData_scr.progressData.stageClearAchievement = _savedData.stageClearAchievement;


            //設定のデータのロード
            Setting_scr.setting.bgmVolume = _savedData.bgmVolume;

            Setting_scr.setting.seVolume = _savedData.seVolume;

            Setting_scr.setting.mouseSensitivity = _savedData.mouseSensitivity;

            Setting_scr.setting.forwardKey = _savedData.forwardKey;

            Setting_scr.setting.backKey = _savedData.backKey;

            Setting_scr.setting.leftKey = _savedData.leftKey;

            Setting_scr.setting.rightKey = _savedData.rightKey;

            return true;
        }


        /// <summary>
        /// データの初期化
        /// </summary>
        public void Initialize()
        {
            //選択中の装備の初期化
            EquipmentData_scr.equipmentData.selectedMainWeaponName = EquipmentData_scr.equipmentType.MainWeapon__Cannon;

            EquipmentData_scr.equipmentData.selectedSubWeaponName = EquipmentData_scr.equipmentType.SubWeapon__Balkan;

            EquipmentData_scr.equipmentData.selectedShieldName = EquipmentData_scr.equipmentType.Shield__Heavy;


            //装備のレベルの初期化
            EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__Cannon] = EquipmentData_scr.level.Level1;

            EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__Laser] = EquipmentData_scr.level.Level1;

            EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun] = EquipmentData_scr.level.Level1;

            EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.SubWeapon__Balkan] = EquipmentData_scr.level.Level1;

            EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.SubWeapon__Missile] = EquipmentData_scr.level.Level1;

            EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Bomb] = EquipmentData_scr.level.Level1;

            EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Shield__Heavy] = EquipmentData_scr.level.Level1;

            EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Shield__Light] = EquipmentData_scr.level.Level1;


            //強化素材の所持数の初期化
            EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__Cannon] = 0;

            EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__Laser] = 0;

            EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun] = 0;

            EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.SubWeapon__Balkan] = 0;

            EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Bomb] = 0;

            EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Shield__Heavy] = 0;

            EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Shield__Light] = 0;


            //ステージの進捗の初期化
            ProgressData_scr.progressData.stageClearAchievement = ProgressData_scr.stageName._none;


            //設定のデータの初期化
            Setting_scr.setting.bgmVolume = _savedData.bgmVolume;

            Setting_scr.setting.seVolume = _savedData.seVolume;

            Setting_scr.setting.mouseSensitivity = _savedData.mouseSensitivity;

            Setting_scr.setting.forwardKey = _savedData.forwardKey;

            Setting_scr.setting.backKey = _savedData.backKey;

            Setting_scr.setting.leftKey = _savedData.leftKey;

            Setting_scr.setting.rightKey = _savedData.rightKey;

            Save();
        }
    }
}
