using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class EquipmentData_scr : MonoBehaviour
{
    #region
    //変数の宣言

    public static EquipmentData_scr equipmentData = null;

    // メイン装備の名前
    public equipmentType selectedMainWeaponName { get; set; }
    // サブ装備の名前
    public equipmentType selectedSubWeaponName { get; set; }
    // 盾の名前
    public equipmentType selectedShieldName { get; set; }

    // 所持している強化用素材の数
    public Dictionary<equipmentType, int> enhancementMaterialsCount { get; set; }

    // 現在の装備ごとのレベル
    public Dictionary<equipmentType, level> equipmentLevel { get; set; }

    // 装備/装甲のパラメーター一覧(存在しないパラメーターには-1を設定)
    public IReadOnlyDictionary<equipmentType, Dictionary<level, Dictionary<equipmentParameter, int>>> equipmentStatus { get; private set; }

    private Dictionary<equipmentType, Dictionary<level, Dictionary<equipmentParameter, int>>> _equipmentStatus__Data { get; set; }

    // 装備の説明
    public IReadOnlyDictionary<equipmentType, string> equipmentDescriptions { get; private set; }

    private Dictionary<equipmentType, string> _equipmentDescriptions__Data { get; set; }

    // 装備の表示名
    public IReadOnlyDictionary<equipmentType, string> equipmentDisplayName { get; private set; }

    private Dictionary<equipmentType, string> _equipmentDisplayName__Data { get; set; }

    // レベルの表示名
    public IReadOnlyDictionary<level, string> levelDisplayName { get; private set; }

    private Dictionary<level, string> _levelDisplayName__Data { get; set; }

    public enum equipmentType
    {
        MainWeapon__Cannon,
        MainWeapon__Laser,
        MainWeapon__BeamMachineGun,
        SubWeapon__Balkan,
        SubWeapon__Missile,
        Bomb,
        Shield__Heavy,
        Shield__Light,
    }

    public enum level
    {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
    }

    // 装備パラメーター(火力/重量など)
    public enum equipmentParameter
    {
        Power,
        Weight,
        Cost,
        RequiredEnhancementMaterialsCount,
        DamageReductionRate,
    }

    #endregion

    private void Awake()
    {
        if (equipmentData == null)
        {
            equipmentData = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        selectedMainWeaponName = equipmentType.MainWeapon__Cannon;

        selectedSubWeaponName = equipmentType.SubWeapon__Balkan;

        selectedShieldName = equipmentType.Shield__Heavy;

        enhancementMaterialsCount = new Dictionary<equipmentType, int>()
        {
            { equipmentType.MainWeapon__Cannon, 0 },
            { equipmentType.MainWeapon__Laser, 0 },
            { equipmentType.MainWeapon__BeamMachineGun, 0 },
            { equipmentType.SubWeapon__Balkan, 0 },
            { equipmentType.SubWeapon__Missile, 0 },
            { equipmentType.Bomb, 0 },
            { equipmentType.Shield__Heavy, 0 },
            { equipmentType.Shield__Light, 0 }
        };

        equipmentLevel = new Dictionary<equipmentType, level>()
        {
            { equipmentType.MainWeapon__Cannon, level.Level1 },
            { equipmentType.MainWeapon__Laser, level.Level1 },
            { equipmentType.MainWeapon__BeamMachineGun, level.Level1 },
            { equipmentType.SubWeapon__Balkan, level.Level1 },
            { equipmentType.SubWeapon__Missile, level.Level1 },
            { equipmentType.Bomb, level.Level1 },
            { equipmentType.Shield__Heavy, level.Level1 },
            { equipmentType.Shield__Light, level.Level1 }
        };

        _equipmentStatus__Data = new Dictionary<equipmentType, Dictionary<level, Dictionary<equipmentParameter, int>>>()
        {
            {
                equipmentType.MainWeapon__Cannon,
                new Dictionary<level, Dictionary<equipmentParameter, int>>()
                {
                    {
                        level.Level1,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 70 },
                            { equipmentParameter.Weight, 60 },
                            { equipmentParameter.Cost, 50 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 5 },
                        }
                    },
                    {
                        level.Level2,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 72 },
                            { equipmentParameter.Weight, 58 },
                            { equipmentParameter.Cost, 48 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 10 },
                        }
                    },
                    {
                        level.Level3,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 75 },
                            { equipmentParameter.Weight, 56 },
                            { equipmentParameter.Cost, 46 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 20 },
                        }
                    },
                    {
                        level.Level4,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 78 },
                            { equipmentParameter.Weight, 53 },
                            { equipmentParameter.Cost, 43 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 40 },
                        }
                    },
                    {
                        level.Level5,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 80 },
                            { equipmentParameter.Weight, 50 },
                            { equipmentParameter.Cost, 40 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, -1 },
                        }
                    },
                }
            },
            {
                equipmentType.MainWeapon__Laser,
                new Dictionary<level, Dictionary<equipmentParameter, int>>()
                {
                    {
                        level.Level1,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 40 },
                            { equipmentParameter.Weight, 40 },
                            { equipmentParameter.Cost, 30 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 5 },
                        }
                    },
                    {
                        level.Level2,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 42 },
                            { equipmentParameter.Weight, 38 },
                            { equipmentParameter.Cost, 28 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 10 },
                        }
                    },
                    {
                        level.Level3,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 44 },
                            { equipmentParameter.Weight, 36 },
                            { equipmentParameter.Cost, 26 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 20 },
                        }
                    },
                    {
                        level.Level4,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 47 },
                            { equipmentParameter.Weight, 33 },
                            { equipmentParameter.Cost, 23 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 40 },
                        }
                    },
                    {
                        level.Level5,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 50 },
                            { equipmentParameter.Weight, 30 },
                            { equipmentParameter.Cost, 20 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, -1 },
                        }
                    },
                }
            },
            {
                equipmentType.MainWeapon__BeamMachineGun,
                new Dictionary<level, Dictionary<equipmentParameter, int>>()
                {
                    {
                        level.Level1,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 10 },
                            { equipmentParameter.Weight, 30 },
                            { equipmentParameter.Cost, 5 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 5 },
                        }
                    },
                    {
                        level.Level2,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 12 },
                            { equipmentParameter.Weight, 28 },
                            { equipmentParameter.Cost, 4 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 10 },
                        }
                    },
                    {
                        level.Level3,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 14 },
                            { equipmentParameter.Weight, 26 },
                            { equipmentParameter.Cost, 3 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 20 },
                        }
                    },
                    {
                        level.Level4,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 17 },
                            { equipmentParameter.Weight, 23 },
                            { equipmentParameter.Cost, 2 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 40 },
                        }
                    },
                    {
                        level.Level5,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 20 },
                            { equipmentParameter.Weight, 20 },
                            { equipmentParameter.Cost, 1 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, -1 },
                        }
                    },
                }
            },
            {
                equipmentType.SubWeapon__Balkan,
                new Dictionary<level, Dictionary<equipmentParameter, int>>()
                {
                    {
                        level.Level1,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 5 },
                            { equipmentParameter.Weight, 10 },
                            { equipmentParameter.Cost, 5 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 10 },
                        }
                    },
                    {
                        level.Level2,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 7 },
                            { equipmentParameter.Weight, 10 },
                            { equipmentParameter.Cost, 5 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 15 },
                        }
                    },
                    {
                        level.Level3,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 10 },
                            { equipmentParameter.Weight, 10 },
                            { equipmentParameter.Cost, 5 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, -1 },
                        }
                    },
                }
            },
            {
                equipmentType.SubWeapon__Missile,
                new Dictionary<level, Dictionary<equipmentParameter, int>>()
                {
                    {
                        level.Level1,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 30 },
                            { equipmentParameter.Weight, 20 },
                            { equipmentParameter.Cost, 30 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 30 },
                        }
                    },
                    {
                        level.Level2,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 32 },
                            { equipmentParameter.Weight, 18 },
                            { equipmentParameter.Cost, 30 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 32 },
                        }
                    },
                    {
                        level.Level3,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 35 },
                            { equipmentParameter.Weight, 15 },
                            { equipmentParameter.Cost, 25 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, -1 },
                        }
                    },
                }
            },
            {
                equipmentType.Bomb,
                new Dictionary<level, Dictionary<equipmentParameter, int>>()
                {
                    {
                        level.Level1,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 180 },
                            { equipmentParameter.Weight, 0 },
                            { equipmentParameter.Cost, 0 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 30 },
                        }
                    },
                    {
                        level.Level2,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 185 },
                            { equipmentParameter.Weight, 0 },
                            { equipmentParameter.Cost, 0 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 32 },
                        }
                    },
                    {
                        level.Level3,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Power, 200 },
                            { equipmentParameter.Weight, 0 },
                            { equipmentParameter.Cost, 0 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, -1 },
                        }
                    },
                }
            },
            {
                equipmentType.Shield__Heavy,
                new Dictionary<level, Dictionary<equipmentParameter, int>>()
                {
                    {
                        level.Level1,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Weight, 35 },
                            { equipmentParameter.DamageReductionRate, 80 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 5 },
                        }
                    },
                    {
                        level.Level2,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Weight, 32 },
                            { equipmentParameter.DamageReductionRate, 85 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 10 },
                        }
                    },
                    {
                        level.Level3,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Weight, 30 },
                            { equipmentParameter.DamageReductionRate, 90 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, -1 },
                        }
                    }
                }
            },
            {
                equipmentType.Shield__Light,
                new Dictionary<level, Dictionary<equipmentParameter, int>>()
                {
                    {
                        level.Level1,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Weight, 30 },
                            { equipmentParameter.DamageReductionRate, 65 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 5 },
                        }
                    },
                    {
                        level.Level2,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Weight, 28 },
                            { equipmentParameter.DamageReductionRate, 70 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, 10 },
                        }
                    },
                    {
                        level.Level3,
                        new Dictionary<equipmentParameter, int>()
                        {
                            { equipmentParameter.Weight, 25 },
                            { equipmentParameter.DamageReductionRate, 75 },
                            { equipmentParameter.RequiredEnhancementMaterialsCount, -1 },
                        }
                    }
                }
            }
        };

        equipmentStatus
            = new ReadOnlyDictionary<equipmentType, Dictionary<level, Dictionary<equipmentParameter, int>>>(_equipmentStatus__Data);

        _equipmentDescriptions__Data = new Dictionary<equipmentType, string>()
        {
            {
                equipmentType.MainWeapon__Cannon,
                "キャノンの説明"
            },
            {
                equipmentType.MainWeapon__Laser,
                "レーザーの説明"
            },
            {
                equipmentType.MainWeapon__BeamMachineGun,
                "ビームマシンガンの説明"
            },
            {
                equipmentType.SubWeapon__Balkan,
                "バルカンの説明"
            },
            {
                equipmentType.SubWeapon__Missile,
                "ミサイルの説明"
            },
            {
                equipmentType.Bomb,
                "ボムの説明"
            },
            {
                equipmentType.Shield__Heavy,
                "重シールドの説明"
            },
            {
                equipmentType.Shield__Light,
                "軽シールドの説明"
            },
        };

        equipmentDescriptions = new ReadOnlyDictionary<equipmentType, string>(_equipmentDescriptions__Data);

        _equipmentDisplayName__Data = new Dictionary<equipmentType, string>()
        {
            { equipmentType.MainWeapon__Cannon, "キャノン" },
            { equipmentType.MainWeapon__Laser, "レーザー" },
            { equipmentType.MainWeapon__BeamMachineGun, "ビームマシンガン" },
            { equipmentType.SubWeapon__Balkan, "バルカン" },
            { equipmentType.SubWeapon__Missile, "ミサイル" },
            { equipmentType.Bomb, "ボム" },
            { equipmentType.Shield__Heavy, "重シールド" },
            { equipmentType.Shield__Light, "軽シールド" },
        };

        equipmentDisplayName = new ReadOnlyDictionary<equipmentType, string>(_equipmentDisplayName__Data);

        _levelDisplayName__Data = new Dictionary<level, string>()
        {
            { level.Level1, "Lv.1" },
            { level.Level2, "Lv.2" },
            { level.Level3, "Lv.3" },
            { level.Level4, "Lv.4" },
            { level.Level5, "Lv.5" },
        };

        levelDisplayName = new ReadOnlyDictionary<level, string>(_levelDisplayName__Data);
    }
}
