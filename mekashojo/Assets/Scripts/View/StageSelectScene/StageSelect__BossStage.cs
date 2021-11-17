using UnityEngine.UI;

namespace View
{
    public class StageSelect__BossStage : StageSelectButtonBase
    {
        private void Start()
        {
            _stageName = Model.ProgressData.stageName.LastStage;
            _stageSceneName = SceneChangeManager.SceneNames.LastStage;
            // ステージ4がクリアされていれば有効化
            GetComponent<Button>().interactable = (int)Model.ProgressData.progressData.stageClearAchievement >= 4;
            this.Initialize();
        }
    }
}
