namespace View
{
    public class StageSelect__Stage2 : StageSelectButtonBase
    {
        private void Start()
        {
            _stageName = Model.ProgressData.stageName.stage2;
            _stageSceneName = Model.SceneChangeManager.SceneNames.Stage2;
            this.Initialize();
        }
    }
}
