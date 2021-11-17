using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class StageSelect_OverlayReturn : ButtonBase
    {
        [SerializeField, Header("押した時に鳴る音を入れる")] private AudioClip _clickSound;
        [SerializeField, Header("StartButtonを入れる")] private StartButton _startButton;
        [SerializeField, Header("Overlayを入れる")] private StageSelect_Overlay _overlay;
        [SerializeField, Header("OverlayReturnを入れる")] private StageSelect_OverlayReturn _overlayReturn;

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
                _overlay.IsVisible = false;
                _overlayReturn.IsObjectActive = false;
                _startButton.IsObjectActive = false;

                // クリック音を鳴らす
                SEPlayer.sePlayer.PlayOneShot(_clickSound);
            }
        }

        private void Update()
        {
            this.ButtonUpdate();
        }
    }
}
