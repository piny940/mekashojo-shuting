using UnityEngine;

namespace View
{
    public class CustomWeaponsButton : ButtonBase
    {
        [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;

        public void OnPush()
        {
            if (CanPush())
            {
                SEPlayer.sePlayer.audioSource.PlayOneShot(_pushSound);
                Model.SceneChangeManager.sceneChangeManager.ChangeScene(Model.SceneChangeManager.SceneNames.EquipmentScene);
            }
        }

        // Update is called once per frame
        void Update()
        {
            ButtonUpdate();
        }
    }
}
