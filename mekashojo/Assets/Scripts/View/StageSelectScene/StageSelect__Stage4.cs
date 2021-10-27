using UnityEngine.UI;

namespace View
{
    public class StageSelect__Stage4 : StageSelectButtonBase
    {
        private void Start()
        {
            _stageName = Model.ProgressData.stageName.stage4;
            _stageSceneName = SceneChangeManager.SceneNames.Stage4;
            // ステージ3がクリアされていれば有効化
            GetComponent<Button>().interactable = (int)Model.ProgressData.progressData.stageClearAchievement >= 3;
            this.Initialize();
        }
    }
}
