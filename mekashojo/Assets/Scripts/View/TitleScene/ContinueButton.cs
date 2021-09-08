using UnityEngine;

namespace View
{
    public class ContinueButton : ButtonBase
    {
        [SerializeField, Header("NoSaveDataScreenを入れる")] NoSaveDataScreen _noSaveDataScreen;
        [SerializeField, Header("セーブデータがなかったときのボタンのサウンド")] AudioClip _noSaveDataSound;
        [SerializeField, Header("セーブデータがあったときのボタンのサウンド")] AudioClip _existSaveDataSound;

        private void Update()
        {
            ButtonUpdate();
        }

        public void OnPush()
        {
            if (CanPush())
            {
                //セーブデータがなかった場合
                if (!SaveDataManager.saveDataManager.Load())
                {
                    SEPlayer.sePlayer.audioSource.PlayOneShot(_noSaveDataSound);
                    _noSaveDataScreen.isVisible = true;
                    return;
                }

                //セーブデータがあった場合
                SEPlayer.sePlayer.audioSource.PlayOneShot(_existSaveDataSound);
                SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.MenuScene);
            }
        }
    }
}
