using UnityEngine.UI;

namespace View
{
    public class StageSelect__Stage1 : StageSelectButtonBase
    {
        private void Start()
        {
            _stageName = Model.ProgressData.stageName.stage1;
            _stageSceneName = SceneChangeManager.SceneNames.Stage1;
            // 有効化に必要なクリア条件はなし
            GetComponent<Button>().interactable = (int)Model.ProgressData.progressData.stageClearAchievement >= 0;
            this.Initialize();
        }
    }
}
