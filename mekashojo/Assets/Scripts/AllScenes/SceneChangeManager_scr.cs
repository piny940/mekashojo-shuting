using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager_scr : MonoBehaviour
{
    static public SceneChangeManager_scr sceneChangeManager = null;
    [SerializeField, Header("はじめにいるScene")] SceneNames _firstSceneName;
    private SceneNames _currentSceneName;
    public SceneNames previousSceneName { get; private set; }

    public enum SceneNames
    {
        TitleScene,
        MenuScene,
        EquipmentScene,
        StageSelectScene,
        HowToPlayScene,
        StoryScene,
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        LastStage,
        StageClearScene,
        StageFailedScene
    }

    private void Awake()
    {
        if (sceneChangeManager == null)
        {
            sceneChangeManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentSceneName = _firstSceneName;
        previousSceneName = _firstSceneName;    //previousSceneNameはとりあえず_firstSceneNameで初期化しとく
    }

    public void ChangeScene(SceneNames nextSceneName)
    {
        //シーンの移動情報の更新
        previousSceneName = _currentSceneName;
        _currentSceneName = nextSceneName;

        //セーブデータの保存
        SaveDataManager_scr.saveDataManager.SaveData();

        //シーンの移動
        SceneManager.LoadScene(nextSceneName.ToString());
    }
}
