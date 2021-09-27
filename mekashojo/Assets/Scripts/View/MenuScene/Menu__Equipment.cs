using UnityEngine;

namespace View
{
    public class Menu__Equipment : ButtonBase
    {
        [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;

        public void OnPush()
        {
            if (CanPush())
            {
                SEPlayer.sePlayer.PlayOneShot(_pushSound);
                SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.EquipmentScene);
            }
        }

        private void Update()
        {
            this.ButtonUpdate();
        }
    }
}
