using UnityEngine;

namespace View
{
    public class Menu__ReturnButton : ButtonBase
    {
        [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;

        public void OnPush()
        {
            if (CanPush())
            {
                SEPlayer.sePlayer.audioSource.PlayOneShot(_pushSound);
                SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.TitleScene);
            }
        }

        private void Update()
        {
            this.ButtonUpdate();
        }
    }
}
