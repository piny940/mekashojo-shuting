using UnityEngine;

namespace View
{
    public class QuitBattleButton : ButtonBaseImp
    {
        [SerializeField, Header("QuitBattleCheckScreenを入れる")] QuitBattleCheckScreen _quitBattleCheckScreen;

        private void Start()
        {
            _quitBattleCheckScreen.isVisible = false;
        }

        private void Update()
        {
            ButtonUpdate();
        }

        public void OnPush()
        {
            if (CanPush())
            {
                _quitBattleCheckScreen.isVisible = true;
            }
        }
    }
}
