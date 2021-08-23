using UnityEngine;

namespace View
{
    public class ReturnToStageButton : ButtonBase
    {
        [SerializeField, Header("QuitBattleCheckScreenを入れる")] QuitBattleCheckScreen _quitBattleCheckScreen;

        private void Update()
        {
            ButtonUpdate();
        }

        public void OnPush()
        {
            if (CanPush())
            {
                _quitBattleCheckScreen.isVisible = false;
            }
        }
    }
}
