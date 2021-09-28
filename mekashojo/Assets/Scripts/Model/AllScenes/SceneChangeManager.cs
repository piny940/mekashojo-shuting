using UnityEngine.SceneManagement;

namespace View
{
    public class SceneChangeManager
    {
        public static SceneChangeManager sceneChangeManager = new SceneChangeManager();
        private SceneNames _currentSceneName;
        private SceneNames _previousSceneName;

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
            _previousSceneName = SceneNames.TitleScene;
        }

        public void ChangeScene(SceneNames nextSceneName, bool willChangeBGM = false)
        {
            if (willChangeBGM) BGMPlayer.bgmPlayer.ChangeBGM(nextSceneName);

            //シーンの移動情報の更新
            _previousSceneName = _currentSceneName;
            _currentSceneName = nextSceneName;

            //セーブデータの保存
            SaveDataManager.saveDataManager.Save();

            //シーンの移動
            SceneManager.LoadScene(nextSceneName.ToString());
        }

        /// <summary>
        /// 一つ前のシーンに戻る。このメソッドは今後の仕様によって変更が加わると思われる
        /// </summary>
        public void ReturnScene(bool willChangeBGM = false)
        {
            if (willChangeBGM) BGMPlayer.bgmPlayer.ChangeBGM(_previousSceneName);
            ChangeScene(_previousSceneName);
        }

        public static SceneNames ReturnSceneEnum(string sceneName)
        {
            foreach (SceneNames scene in System.Enum.GetValues(typeof(SceneNames)))
            {
                if (scene.ToString() == sceneName)
                    return scene;
            }

            // ここまできた場合は引数の名前がタイポ
            throw new System.Exception();
        }
    }
}
