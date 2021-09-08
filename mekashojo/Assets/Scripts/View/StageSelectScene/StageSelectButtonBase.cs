using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class StageSelectButtonBase : ButtonBase
    {
        [SerializeField, Header("StageDescriptionsを入れる")] private StageDescriptions _stageDescriptions;
        [SerializeField, Header("StartButtonを入れる")] private StartButton _startButton;
        [SerializeField, Header("ボタンを押したときになる音")] AudioClip _pushSound;

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
                SEPlayer.sePlayer.audioSource.PlayOneShot(_pushSound);
                _stageDescriptions.text = Model.ProgressData.progressData.stageDescriptions[_stageName];
                _startButton.selectingStageName = _stageSceneName;
            }
        }

        private void Update()
        {
            this.ButtonUpdate();
        }
    }
}
