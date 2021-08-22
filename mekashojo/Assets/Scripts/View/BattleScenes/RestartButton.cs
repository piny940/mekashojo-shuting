namespace View
{
    public class RestartButton : ButtonBaseImp
    {
        private void Update()
        {
            ButtonUpdate();
        }

        public void OnPush()
        {
            if (CanPush())
            {
                Controller.ModelClassController.pauseController.isPauseScreenVisible = false;
            }
        }
    }
}
