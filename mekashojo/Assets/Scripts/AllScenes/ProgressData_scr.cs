using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class ProgressData_scr : MonoBehaviour
{
    public static ProgressData_scr progressData;
    public int latestStageNumber;   //ラストステージはステージ5として管理

    // 到達済みのステージ
    public stageName stageClearAchievement { get; set; }

    // ステージの説明
    public IReadOnlyDictionary<stageName, string> stageDescriptions { get; private set; }

    private Dictionary<stageName, string> _stageDescriptions__Data { get; set; }

    public enum stageName
    {
        stage1,
        stage2,
        stage3,
        stage4,
        lastStage,
    }

    //シングルトン
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
    }

    private void Start()
    {
        _stageDescriptions__Data = new Dictionary<stageName, string>()
        {
            { stageName.stage1, "ステージ1の説明" },
            { stageName.stage2, "ステージ2の説明" },
            { stageName.stage3, "ステージ3の説明" },
            { stageName.stage4, "ステージ4の説明" },
            { stageName.lastStage, "ラストステージの説明" },
        };

        stageDescriptions = new ReadOnlyDictionary<stageName, string>(_stageDescriptions__Data);
    }
}
