using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager_scr : MonoBehaviour
{
    static public SceneChangeManager_scr sceneChangeManager = null;
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
        _currentSceneName = SceneNames.TitleScene;
        previousSceneName = SceneNames.TitleScene;
    }

    public void ChangeScene(SceneNames nextSceneName)
    {
        //シーンの移動情報の更新
        previousSceneName = _currentSceneName;
        _currentSceneName = nextSceneName;

        //セーブデータの保存
        SaveDataManager_scr.saveDataManager.Save();

        //シーンの移動
        SceneManager.LoadScene(nextSceneName.ToString());
    }
}
