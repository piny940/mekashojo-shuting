using UnityEngine;

namespace View
{
    public class HowToPlayButton : ButtonBase
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
                SEPlayer.sePlayer.PlayOneShot(_clickSound);

                SceneChangeManager.sceneChangeManager.ChangeScene(SceneChangeManager.SceneNames.HowToPlayScene, true);
            }
        }
    }
}
