using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class ProgressData_scr : MonoBehaviour
{
    public static ProgressData_scr progressData;

    // 到達済みのステージ
    public stageName stageClearAchievement { get; set; }

    // ステージの説明
    public IReadOnlyDictionary<stageName, string> stageDescriptions { get; private set; }

    private Dictionary<stageName, string> _stageDescriptions__Data { get; set; }

    // ステージの表示名
    public IReadOnlyDictionary<stageName, string> stageDisplayName { get; private set; }

    private Dictionary<stageName, string> _stageDisplayName__Data { get; set; }

    public enum stageName
    {
        _none,
        stage1,
        stage2,
        stage3,
        stage4,
        lastStage,
    }

    private void Awake()
    {
        if (progressData == null)
        {
            progressData = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        _stageDescriptions__Data = new Dictionary<stageName, string>()
        {
            { stageName.stage1, "ステージ1の説明" },
            { stageName.stage2, "ステージ2の説明" },
            { stageName.stage3, "ステージ3の説明" },
            { stageName.stage4, "ステージ4の説明" },
            { stageName.lastStage, "ラストステージの説明" },
        };

        stageDescriptions = new ReadOnlyDictionary<stageName, string>(_stageDescriptions__Data);

        _stageDisplayName__Data = new Dictionary<stageName, string>()
        {
            { stageName.stage1, "Stage1" },
            { stageName.stage2, "Stage2" },
            { stageName.stage3, "Stage3" },
            { stageName.stage4, "Stage4" },
            { stageName.lastStage, "Boss" },
        };

        stageDisplayName = new ReadOnlyDictionary<stageName, string>(_stageDisplayName__Data);
    }
}
