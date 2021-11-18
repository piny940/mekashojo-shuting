using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class HowToPlay__ForwardButton : ButtonBase
    {
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
                _pageManager.currentPage++;
            }
        }

        void Update()
        {
            ButtonUpdate();
        }
    }
}
