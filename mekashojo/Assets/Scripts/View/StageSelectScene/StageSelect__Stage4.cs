namespace View
{
    public class StageSelect__Stage4 : StageSelectButtonBase
    {
        private void Start()
        {
            _stageName = Model.ProgressData.stageName.stage4;
            _stageSceneName = Model.SceneChangeManager.SceneNames.Stage4;
            this.Initialize();
        }
    }
}
