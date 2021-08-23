using UnityEngine.UI;

namespace View
{
    public class StartButton : ButtonBase
    {
        private bool _hasNotSelected = true;

        private Model.SceneChangeManager.SceneNames _selectingStageName;
        public Model.SceneChangeManager.SceneNames selectingStageName
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

        private void Start()
        {
            this.isActive = false;
        }

        public void OnPush()
        {
            if (CanPush())
            {
                Model.SceneChangeManager.sceneChangeManager.ChangeScene(selectingStageName);
            }
        }

        private void Update()
        {
            this.ButtonUpdate();
        }
    }
}
