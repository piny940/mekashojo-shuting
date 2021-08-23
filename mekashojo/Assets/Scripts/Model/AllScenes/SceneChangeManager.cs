using UnityEngine.SceneManagement;

namespace Model
{
    public class SceneChangeManager
    {
        public static SceneChangeManager sceneChangeManager = new SceneChangeManager();
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

        private SceneChangeManager()
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
            SaveDataManager.saveDataManager.Save();

            //シーンの移動
            SceneManager.LoadScene(nextSceneName.ToString());
        }
    }
}
