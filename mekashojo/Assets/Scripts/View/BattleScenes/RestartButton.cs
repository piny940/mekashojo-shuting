namespace View
{
    public class RestartButton : ButtonBase
    {
        private void Update()
        {
            ButtonUpdate();
        }

        public void OnPush()
        {
            if (CanPush())
            {
                Controller.BattleScenesController.stageStatusManager.Restart();
            }
        }
    }
}
