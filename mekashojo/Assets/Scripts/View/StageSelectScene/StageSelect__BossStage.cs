namespace View
{
    public class StageSelect__BossStage : StageSelectButtonBase
    {
        private void Start()
        {
            _stageName = Model.ProgressData.stageName.lastStage;
            _stageSceneName = Model.SceneChangeManager.SceneNames.LastStage;
            this.Initialize();
        }
    }
}
