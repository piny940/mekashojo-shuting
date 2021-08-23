using UnityEngine;

namespace View
{
    public class StageSelect__ReturnButton : ButtonBase
    {
        [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;

        public void OnPush()
        {
            if (CanPush())
            {
                SEPlayer.sePlayer.audioSource.PlayOneShot(_pushSound);
                Model.SceneChangeManager.sceneChangeManager.ChangeScene(Model.SceneChangeManager.SceneNames.MenuScene);
            }
        }

        private void Update()
        {
            this.ButtonUpdate();
        }
    }
}
