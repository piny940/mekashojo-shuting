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
                SEPlayer.sePlayer.audioSource.PlayOneShot(_clickSound);

                Model.SaveDataManager.saveDataManager.Initialize();

                Model.SceneChangeManager.sceneChangeManager.ChangeScene(Model.SceneChangeManager.SceneNames.MenuScene);
            }
        }
    }
}
