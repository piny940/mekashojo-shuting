using UnityEngine;

namespace View
{
    public class Menu__Story : ButtonBase
    {
        [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;

        public void OnPush()
        {
            if (CanPush())
            {
                SEPlayer.sePlayer.audioSource.PlayOneShot(_pushSound);
                Model.SceneChangeManager.sceneChangeManager.ChangeScene(Model.SceneChangeManager.SceneNames.StoryScene);
            }
        }

        private void Update()
        {
            this.ButtonUpdate();
        }
    }
}
