namespace View
{
    public class StageSelect__Stage3 : StageSelectButtonBase
    {
        private void Start()
        {
            _stageName = Model.ProgressData.stageName.stage3;
            _stageSceneName = Model.SceneChangeManager.SceneNames.Stage3;
            this.Initialize();
        }
    }
}
