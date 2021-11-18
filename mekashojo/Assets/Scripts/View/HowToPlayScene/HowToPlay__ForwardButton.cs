using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class HowToPlay__ForwardButton : ButtonBase
    {
        [SerializeField, Header("押したときになる音")] AudioClip _clickSound;
        [SerializeField, Header("PageManagerを入れる")] private HowToPlay__PageManager _pageManager;

        public bool isActive
        {
            get { return GetComponent<Button>().interactable; }
            set { GetComponent<Button>().interactable = value; }
        }

        public void OnPush()
        {
            if (CanPush())
            {
                SEPlayer.sePlayer.PlayOneShot(_clickSound);

                _pageManager.currentPage++;
            }
        }

        void Update()
        {
            ButtonUpdate();
        }
    }
}
