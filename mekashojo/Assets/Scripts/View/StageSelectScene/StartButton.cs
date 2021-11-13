using UnityEngine.UI;

namespace View
{
    public class StartButton : ButtonBase
    {
        private bool _hasNotSelected = true;

        private SceneChangeManager.SceneNames _selectingStageName;
        public SceneChangeManager.SceneNames selectingStageName
        {
            private get { return this._selectingStageName; }
            set
            {
                if (_hasNotSelected)
                {
                    this.isActive = true;
                    _hasNotSelected = false;
                }
                this._selectingStageName = value;
            }
        }

        public bool isActive
        {
            get { return GetComponent<Button>().interactable; }
            set { GetComponent<Button>().interactable = value; }
        }

        public bool IsObjectActive
        {
            set { this.gameObject.SetActive(value); }
        }

        public bool IsVisible
        {
            get { return this.GetComponent<Image>().enabled; }
            set { this.GetComponent<Image>().enabled = value; }
        }

        private void Start()
        {
            this.IsObjectActive = false;
        }

        public void OnPush()
        {
            if (CanPush())
            {
                SceneChangeManager.sceneChangeManager.ChangeScene(selectingStageName, true);
            }
        }

        private void Update()
        {
            this.ButtonUpdate();
        }
    }
}
