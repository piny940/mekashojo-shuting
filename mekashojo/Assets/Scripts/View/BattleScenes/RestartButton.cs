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
                Controller.BattleScenesClassController.pauseController.isPauseScreenVisible = false;
            }
        }
    }
}
