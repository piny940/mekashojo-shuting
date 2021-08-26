namespace View
{
    public class RetryButton : ButtonBase
    {
        public void OnPush()
        {
            if (CanPush())
            {
                SceneChangeManager.sceneChangeManager.ReturnScene();
            }
        }

        // Update is called once per frame
        void Update()
        {
            ButtonUpdate();
        }
    }
}
