using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class EnhancementButton : ButtonBase
    {
        [SerializeField, Header("押した時になる音を入れる")] private AudioClip _clickSound;

        /// <summary>
        /// ボタンが押下可能であるかどうかを示す
        /// </summary>
        public bool isActive
        {
            get { return GetComponent<Button>().interactable; }
            set { GetComponent<Button>().interactable = value; }
        }

        /// <summary>
        /// ボタンが表示されているかどうかを示す
        /// </summary>
        public bool isVisible
        {
            get { return this.gameObject.activeSelf; }
            set { this.gameObject.SetActive(value); }
        }

        /// <summary>
        /// ボタンが押下された時のアクション
        /// </summary>
        public Action EnhanceAction;

        public void OnPush()
        {
            if (CanPush())
            {
                SEPlayer.sePlayer.PlayOneShot(_clickSound);
                EnhanceAction();
            }
        }

        private void Update()
        {
            this.ButtonUpdate();
        }
    }
}
