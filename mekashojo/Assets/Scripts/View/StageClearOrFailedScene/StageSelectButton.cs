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
                BGMPlayer.bgmPlayer.ChangeBGM(SceneChangeManager.SceneNames.StageSelectScene);
                SEPlayer.sePlayer.audioSource.PlayOneShot(_pushSound);
                SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.StageSelectScene);
            }
        }

        // Update is called once per frame
        void Update()
        {
            ButtonUpdate();
        }
    }
}
