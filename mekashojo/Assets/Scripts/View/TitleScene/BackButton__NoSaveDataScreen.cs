using UnityEngine;

namespace View
{
    public class BackButton__NoSaveDataScreen : ButtonBase
    {
        [SerializeField, Header("NoSaveDataScreenを入れる")] NoSaveDataScreen _noSaveDataScreen;
        [SerializeField, Header("ボタンを押した時のサウンド")] AudioClip _pushSound;

        private void Update()
        {
            ButtonUpdate();
        }

        public void OnPush()
        {
            if (CanPush())
            {
                SEPlayer.sePlayer.PlayOneShot(_pushSound);
                _noSaveDataScreen.isVisible = false;
            }
        }
    }
}
