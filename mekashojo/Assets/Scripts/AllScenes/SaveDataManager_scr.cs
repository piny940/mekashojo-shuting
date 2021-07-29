using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
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


public class SaveDataManager_scr : MonoBehaviour
{
    public static SaveDataManager_scr saveDataManager { get; set; }
    SaveData _saveData;
    string _saveData__JsonString;   //セーブデータをstring型で保存


    private void Awake()
    {
        if (saveDataManager == null)
        {
            saveDataManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        _saveData = new SaveData();
    }

    /// <summary>
    /// データをセーブする
    /// </summary>
    public void Save()
    {
        //選択中の装備の保存
        _saveData.selectedMainWeaponName = EquipmentData_scr.equipmentData.selectedMainWeaponName;

        _saveData.selectedSubWeaponName = EquipmentData_scr.equipmentData.selectedSubWeaponName;

        _saveData.selectedShieldName = EquipmentData_scr.equipmentData.selectedShieldName;


        //装備のレベルの保存
        _saveData.cannonLevel = EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__Cannon];

        _saveData.laserLevel = EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__Laser];

        _saveData.beamMachineGunLevel = EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun];

        _saveData.balkanLevel = EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.SubWeapon__Balkan];

        _saveData.missileLevel = EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.SubWeapon__Missile];

        _saveData.bombLevel = EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Bomb];

        _saveData.heavyShieldLevel = EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Shield__Heavy];

        _saveData.lightShieldLevel = EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Shield__Light];


        //強化素材の所持数の保存
        _saveData.enhancementMaterialsCount__Cannon = EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__Cannon];

        _saveData.enhancementMaterialsCount__Laser = EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__Laser];

        _saveData.enhancementMaterialsCount__BeamMachineGun = EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun];

        _saveData.enhancementMaterialsCount__Balkan = EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.SubWeapon__Balkan];

        _saveData.enhancementMaterialsCount__Bomb = EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Bomb];

        _saveData.enhancementMaterialsCount__HeavyShield = EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Shield__Heavy];

        _saveData.enhancementMaterialsCount__LightShield = EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Shield__Light];


        //ステージの進捗の保存
        _saveData.stageClearAchievement = ProgressData_scr.progressData.stageClearAchievement;


        //設定のデータの保存
        _saveData.bgmVolume = Setting_scr.setting.bgmVolume;

        _saveData.seVolume = Setting_scr.setting.seVolume;

        _saveData.mouseSensitivity = Setting_scr.setting.mouseSensitivity;

        _saveData.forwardKey = Setting_scr.setting.forwardKey;

        _saveData.backKey = Setting_scr.setting.backKey;

        _saveData.leftKey = Setting_scr.setting.leftKey;

        _saveData.rightKey = Setting_scr.setting.rightKey;


        _saveData__JsonString = JsonUtility.ToJson(_saveData);
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
        _saveData = JsonUtility.FromJson<SaveData>(_saveData__JsonString);

        //選択中の装備のロード
        EquipmentData_scr.equipmentData.selectedMainWeaponName = _saveData.selectedMainWeaponName;

        EquipmentData_scr.equipmentData.selectedSubWeaponName = _saveData.selectedSubWeaponName;

        EquipmentData_scr.equipmentData.selectedShieldName = _saveData.selectedShieldName;


        //装備のレベルのロード
        EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__Cannon] = _saveData.cannonLevel;

        EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__Laser] = _saveData.laserLevel;

        EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun] = _saveData.beamMachineGunLevel;

        EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.SubWeapon__Balkan] = _saveData.balkanLevel;

        EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.SubWeapon__Missile] = _saveData.missileLevel;

        EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Bomb] = _saveData.bombLevel;

        EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Shield__Heavy] = _saveData.heavyShieldLevel;

        EquipmentData_scr.equipmentData.equipmentLevel[EquipmentData_scr.equipmentType.Shield__Light] = _saveData.lightShieldLevel;


        //強化素材の所持数のロード
        EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__Cannon] = _saveData.enhancementMaterialsCount__Cannon;

        EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__Laser] = _saveData.enhancementMaterialsCount__Laser;

        EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.MainWeapon__BeamMachineGun] = _saveData.enhancementMaterialsCount__BeamMachineGun;

        EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.SubWeapon__Balkan] = _saveData.enhancementMaterialsCount__Balkan;

        EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Bomb] = _saveData.enhancementMaterialsCount__Bomb;

        EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Shield__Heavy] = _saveData.enhancementMaterialsCount__HeavyShield;

        EquipmentData_scr.equipmentData.enhancementMaterialsCount[EquipmentData_scr.equipmentType.Shield__Light] = _saveData.enhancementMaterialsCount__LightShield;


        //ステージの進捗のロード
        ProgressData_scr.progressData.stageClearAchievement = _saveData.stageClearAchievement;


        //設定のデータのロード
        Setting_scr.setting.bgmVolume = _saveData.bgmVolume;

        Setting_scr.setting.seVolume = _saveData.seVolume;

        Setting_scr.setting.mouseSensitivity = _saveData.mouseSensitivity;

        Setting_scr.setting.forwardKey = _saveData.forwardKey;

        Setting_scr.setting.backKey = _saveData.backKey;

        Setting_scr.setting.leftKey = _saveData.leftKey;

        Setting_scr.setting.rightKey = _saveData.rightKey;

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
        Setting_scr.setting.bgmVolume = _saveData.bgmVolume;

        Setting_scr.setting.seVolume = _saveData.seVolume;

        Setting_scr.setting.mouseSensitivity = _saveData.mouseSensitivity;

        Setting_scr.setting.forwardKey = _saveData.forwardKey;

        Setting_scr.setting.backKey = _saveData.backKey;

        Setting_scr.setting.leftKey = _saveData.leftKey;

        Setting_scr.setting.rightKey = _saveData.rightKey;

        Save();
    }

}




