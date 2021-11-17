using UnityEngine.UI;

namespace View
{
    public class StageSelect__Stage2 : StageSelectButtonBase
    {
        private void Start()
        {
            _stageName = Model.ProgressData.stageName.Stage2;
            _stageSceneName = SceneChangeManager.SceneNames.Stage2;
            // ステージ1がクリアされていれば有効化
            GetComponent<Button>().interactable = (int)Model.ProgressData.progressData.stageClearAchievement >= 1;
            this.Initialize();
        }
    }
}
