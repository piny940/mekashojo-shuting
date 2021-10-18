using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect_OverlayReturn : ButtonBase
{
    [SerializeField, Header("StageDescriptionsを入れる")] private View.StageDescriptions _stageDescriptions;
    [SerializeField, Header("StartButtonを入れる")] private View.StartButton _startButton;
    [SerializeField, Header("Overlayを入れる")] private StageSelect_Overlay _overlay;
    [SerializeField, Header("OverlayReturnを入れる")] private StageSelect_OverlayReturn _overlayReturn;
    [SerializeField, Header("SelectingStageTitle_Stage1を入れる")] private SelectingStageTitle_Stage1 _selectingStageTitle_Stage1;
    [SerializeField, Header("SelectingStageTitle_Stage2を入れる")] private SelectingStageTitle_Stage2 _selectingStageTitle_Stage2;
    [SerializeField, Header("SelectingStageTitle_Stage3を入れる")] private SelectingStageTitle_Stage3 _selectingStageTitle_Stage3;
    [SerializeField, Header("SelectingStageTitle_Stage4を入れる")] private SelectingStageTitle_Stage4 _selectingStageTitle_Stage4;
    [SerializeField, Header("SelectingStageTitle_Stage5を入れる")] private SelectingStageTitle_Stage5 _selectingStageTitle_Stage5;

    public bool IsObjectActive
    {
        set { this.gameObject.SetActive(value); }
    }

    void Start()
    {
        this.IsObjectActive = false;
    }

    public void OnPush()
    {
        if (CanPush())
        {
            // 元の状態に戻す
            _stageDescriptions.IsObjectActive = false;
            _overlay.IsVisible = false;
            _overlayReturn.IsObjectActive = false;
            _startButton.IsObjectActive = false;
            _selectingStageTitle_Stage1.IsVisible = false;
            _selectingStageTitle_Stage2.IsVisible = false;
            _selectingStageTitle_Stage3.IsVisible = false;
            _selectingStageTitle_Stage4.IsVisible = false;
            _selectingStageTitle_Stage5.IsVisible = false;
        }
    }

    private void Update()
    {
        this.ButtonUpdate();
    }
}
