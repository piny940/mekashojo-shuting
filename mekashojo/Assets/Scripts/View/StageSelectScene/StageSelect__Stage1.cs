namespace View
{
    public class StageSelect__Stage1 : StageSelectButtonBase
    {
        private void Start()
        {
            _stageName = Model.ProgressData.stageName.stage1;
            _stageSceneName = Model.SceneChangeManager.SceneNames.Stage1;
            this.Initialize();
        }
    }
}
