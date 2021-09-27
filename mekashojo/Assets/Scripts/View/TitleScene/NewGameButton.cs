using UnityEngine;

namespace View
{
    public class NewGameButton : ButtonBase
    {
        [SerializeField, Header("押したときになる音")] AudioClip _clickSound;

        // Update is called once per frame
        void Update()
        {
            ButtonUpdate();
        }

        public void OnPush()
        {
            if (CanPush())
            {
                BGMPlayer.bgmPlayer.ChangeBGM(SceneChangeManager.SceneNames.MenuScene);

                SEPlayer.sePlayer.PlayOneShot(_clickSound);

                SaveDataManager.saveDataManager.Initialize();

                SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.MenuScene);
            }
        }
    }
}
