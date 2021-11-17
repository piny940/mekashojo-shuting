using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public abstract class StageSelectButtonBase : ButtonBase
    {
        [SerializeField, Header("StartButtonを入れる")] private StartButton _startButton;
        [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;
        [SerializeField, Header("Overlayを入れる")] private StageSelect_Overlay _overlay;
        [SerializeField, Header("OverlayReturnを入れる")] private StageSelect_OverlayReturn _overlayReturn;

        protected Model.ProgressData.stageName _stageName;
        // ステージのシーン名
        protected SceneChangeManager.SceneNames _stageSceneName;

        protected void Initialize()
        {
            GetComponentInChildren<Text>().text = Model.ProgressData.progressData.stageDisplayName[_stageName];
        }

        public void OnPush()
        {
            if (CanPush())
            {
                SEPlayer.sePlayer.PlayOneShot(_pushSound);
                _startButton.IsObjectActive = true;
                _startButton.selectingStageName = _stageSceneName;

                _overlay.IsVisible = true;
                _overlayReturn.IsObjectActive = true;
            }
        }

        private void Update()
        {
            this.ButtonUpdate();
        }
    }
}
