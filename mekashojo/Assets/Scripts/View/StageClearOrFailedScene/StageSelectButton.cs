using UnityEngine;

namespace View
{
    public class StageSelectButton : ButtonBase
    {
        [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;

        public void OnPush()
        {
            if (CanPush())
            {
                SEPlayer.sePlayer.PlayOneShot(_pushSound);
                SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.StageSelectScene, true);
            }
        }

        // Update is called once per frame
        void Update()
        {
            ButtonUpdate();
        }
    }
}
