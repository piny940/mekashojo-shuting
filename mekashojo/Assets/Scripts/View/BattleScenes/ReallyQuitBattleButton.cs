namespace View
{
    public class ReallyQuitBattleButton : ButtonBaseImp
    {
        private void Update()
        {
            ButtonUpdate();
        }

        public void OnPush()
        {
            if (CanPush())
            {
                SceneChangeManager_scr.sceneChangeManager.ChangeScene(SceneChangeManager_scr.SceneNames.MenuScene);
            }
        }
    }
}
