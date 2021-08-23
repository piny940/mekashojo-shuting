using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class SavedData
    {
        //選択中の装備のデータ
        public EquipmentData.equipmentType selectedMainWeaponName;
        public EquipmentData.equipmentType selectedSubWeaponName;
        public EquipmentData.equipmentType selectedShieldName;

        //装備のレベルのデータ
        public EquipmentData.level cannonLevel;
        public EquipmentData.level laserLevel;
        public EquipmentData.level beamMachineGunLevel;
        public EquipmentData.level balkanLevel;
        public EquipmentData.level missileLevel;
        public EquipmentData.level bombLevel;
        public EquipmentData.level heavyShieldLevel;
        public EquipmentData.level lightShieldLevel;

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
        public ProgressData.stageName stageClearAchievement;

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
            _savedData.selectedMainWeaponName = EquipmentData.equipmentData.selectedMainWeaponName;

            _savedData.selectedSubWeaponName = EquipmentData.equipmentData.selectedSubWeaponName;

            _savedData.selectedShieldName = EquipmentData.equipmentData.selectedShieldName;


            //装備のレベルの保存
            _savedData.cannonLevel = EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.MainWeapon__Cannon];

            _savedData.laserLevel = EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.MainWeapon__Laser];

            _savedData.beamMachineGunLevel = EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.MainWeapon__BeamMachineGun];

            _savedData.balkanLevel = EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.SubWeapon__Balkan];

            _savedData.missileLevel = EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.SubWeapon__Missile];

            _savedData.bombLevel = EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.Bomb];

            _savedData.heavyShieldLevel = EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.Shield__Heavy];

            _savedData.lightShieldLevel = EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.Shield__Light];


            //強化素材の所持数の保存
            _savedData.enhancementMaterialsCount__Cannon = EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.MainWeapon__Cannon];

            _savedData.enhancementMaterialsCount__Laser = EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.MainWeapon__Laser];

            _savedData.enhancementMaterialsCount__BeamMachineGun = EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.MainWeapon__BeamMachineGun];

            _savedData.enhancementMaterialsCount__Balkan = EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.SubWeapon__Balkan];

            _savedData.enhancementMaterialsCount__Bomb = EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.Bomb];

            _savedData.enhancementMaterialsCount__HeavyShield = EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.Shield__Heavy];

            _savedData.enhancementMaterialsCount__LightShield = EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.Shield__Light];


            //ステージの進捗の保存
            _savedData.stageClearAchievement = ProgressData.progressData.stageClearAchievement;


            //設定のデータの保存
            _savedData.bgmVolume = Setting.bgmVolume;

            _savedData.seVolume = Setting.seVolume;

            _savedData.mouseSensitivity = Setting.mouseSensitivity;

            _savedData.forwardKey = Setting.forwardKey;

            _savedData.backKey = Setting.backKey;

            _savedData.leftKey = Setting.leftKey;

            _savedData.rightKey = Setting.rightKey;


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
            EquipmentData.equipmentData.selectedMainWeaponName = _savedData.selectedMainWeaponName;

            EquipmentData.equipmentData.selectedSubWeaponName = _savedData.selectedSubWeaponName;

            EquipmentData.equipmentData.selectedShieldName = _savedData.selectedShieldName;


            //装備のレベルのロード
            EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.MainWeapon__Cannon] = _savedData.cannonLevel;

            EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.MainWeapon__Laser] = _savedData.laserLevel;

            EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.MainWeapon__BeamMachineGun] = _savedData.beamMachineGunLevel;

            EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.SubWeapon__Balkan] = _savedData.balkanLevel;

            EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.SubWeapon__Missile] = _savedData.missileLevel;

            EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.Bomb] = _savedData.bombLevel;

            EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.Shield__Heavy] = _savedData.heavyShieldLevel;

            EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.Shield__Light] = _savedData.lightShieldLevel;


            //強化素材の所持数のロード
            EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.MainWeapon__Cannon] = _savedData.enhancementMaterialsCount__Cannon;

            EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.MainWeapon__Laser] = _savedData.enhancementMaterialsCount__Laser;

            EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.MainWeapon__BeamMachineGun] = _savedData.enhancementMaterialsCount__BeamMachineGun;

            EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.SubWeapon__Balkan] = _savedData.enhancementMaterialsCount__Balkan;

            EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.Bomb] = _savedData.enhancementMaterialsCount__Bomb;

            EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.Shield__Heavy] = _savedData.enhancementMaterialsCount__HeavyShield;

            EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.Shield__Light] = _savedData.enhancementMaterialsCount__LightShield;


            //ステージの進捗のロード
            ProgressData.progressData.stageClearAchievement = _savedData.stageClearAchievement;


            //設定のデータのロード
            Setting.bgmVolume = _savedData.bgmVolume;

            Setting.seVolume = _savedData.seVolume;

            Setting.mouseSensitivity = _savedData.mouseSensitivity;

            Setting.forwardKey = _savedData.forwardKey;

            Setting.backKey = _savedData.backKey;

            Setting.leftKey = _savedData.leftKey;

            Setting.rightKey = _savedData.rightKey;

            return true;
        }


        /// <summary>
        /// データの初期化
        /// </summary>
        public void Initialize()
        {
            //選択中の装備の初期化
            EquipmentData.equipmentData.selectedMainWeaponName = EquipmentData.equipmentType.MainWeapon__Cannon;

            EquipmentData.equipmentData.selectedSubWeaponName = EquipmentData.equipmentType.SubWeapon__Balkan;

            EquipmentData.equipmentData.selectedShieldName = EquipmentData.equipmentType.Shield__Heavy;


            //装備のレベルの初期化
            EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.MainWeapon__Cannon] = EquipmentData.level.Level1;

            EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.MainWeapon__Laser] = EquipmentData.level.Level1;

            EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.MainWeapon__BeamMachineGun] = EquipmentData.level.Level1;

            EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.SubWeapon__Balkan] = EquipmentData.level.Level1;

            EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.SubWeapon__Missile] = EquipmentData.level.Level1;

            EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.Bomb] = EquipmentData.level.Level1;

            EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.Shield__Heavy] = EquipmentData.level.Level1;

            EquipmentData.equipmentData.equipmentLevel[EquipmentData.equipmentType.Shield__Light] = EquipmentData.level.Level1;


            //強化素材の所持数の初期化
            EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.MainWeapon__Cannon] = 0;

            EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.MainWeapon__Laser] = 0;

            EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.MainWeapon__BeamMachineGun] = 0;

            EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.SubWeapon__Balkan] = 0;

            EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.Bomb] = 0;

            EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.Shield__Heavy] = 0;

            EquipmentData.equipmentData.enhancementMaterialsCount[EquipmentData.equipmentType.Shield__Light] = 0;


            //ステージの進捗の初期化
            ProgressData.progressData.stageClearAchievement = ProgressData.stageName._none;


            //設定のデータの初期化
            Setting.bgmVolume = _savedData.bgmVolume;

            Setting.seVolume = _savedData.seVolume;

            Setting.mouseSensitivity = _savedData.mouseSensitivity;

            Setting.forwardKey = _savedData.forwardKey;

            Setting.backKey = _savedData.backKey;

            Setting.leftKey = _savedData.leftKey;

            Setting.rightKey = _savedData.rightKey;

            Save();
        }
    }
}
