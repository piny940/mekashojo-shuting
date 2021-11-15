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
        [SerializeField, Header("SelectingStageTitle_Stage1を入れる")] private SelectingStageTitle_Stage1 _selectingStageTitle_Stage1;
        [SerializeField, Header("SelectingStageTitle_Stage2を入れる")] private SelectingStageTitle_Stage2 _selectingStageTitle_Stage2;
        [SerializeField, Header("SelectingStageTitle_Stage3を入れる")] private SelectingStageTitle_Stage3 _selectingStageTitle_Stage3;
        [SerializeField, Header("SelectingStageTitle_Stage4を入れる")] private SelectingStageTitle_Stage4 _selectingStageTitle_Stage4;
        [SerializeField, Header("SelectingStageTitle_Stage5を入れる")] private SelectingStageTitle_Stage5 _selectingStageTitle_Stage5;

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
                switch ((int)_stageName)
                {
                    case 1:
                        _selectingStageTitle_Stage1.IsVisible = true;
                        break;
                    case 2:
                        _selectingStageTitle_Stage2.IsVisible = true;
                        break;
                    case 3:
                        _selectingStageTitle_Stage3.IsVisible = true;
                        break;
                    case 4:
                        _selectingStageTitle_Stage4.IsVisible = true;
                        break;
                    case 5:
                        _selectingStageTitle_Stage5.IsVisible = true;
                        break;
                }
            }
        }

        private void Update()
        {
            this.ButtonUpdate();
        }
    }
}
