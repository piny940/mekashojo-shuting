using UnityEngine.UI;

namespace View
{
    public class StageSelect__Stage3 : StageSelectButtonBase
    {
        private void Start()
        {
            _stageName = Model.ProgressData.stageName.stage3;
            _stageSceneName = SceneChangeManager.SceneNames.Stage3;
            // ステージ2がクリアされていれば有効化
            GetComponent<Button>().interactable = (int)Model.ProgressData.progressData.stageClearAchievement >= 2;
            this.Initialize();
        }
    }
}
