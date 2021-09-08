namespace View
{
    public class ReallyQuitBattleButton : ButtonBase
    {
        private void Update()
        {
            ButtonUpdate();
        }

        public void OnPush()
        {
            if (CanPush())
            {
                SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.MenuScene);
            }
        }
    }
}
