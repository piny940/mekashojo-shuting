namespace View
{
    public class RetryButton : ButtonBase
    {
        public void OnPush()
        {
            if (CanPush())
            {
                Model.SceneChangeManager.sceneChangeManager.ChangeScene(Model.SceneChangeManager.sceneChangeManager.previousSceneName);
            }
        }

        // Update is called once per frame
        void Update()
        {
            ButtonUpdate();
        }
    }
}
