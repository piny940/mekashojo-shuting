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
                SEPlayer.sePlayer.PlayOneShot(_pushSound);
                SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.MenuScene);
            }
        }

        private void Update()
        {
            this.ButtonUpdate();
        }
    }
}
