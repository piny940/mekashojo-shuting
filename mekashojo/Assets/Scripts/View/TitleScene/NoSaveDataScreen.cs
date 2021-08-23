using UnityEngine;

namespace View
{
    public class NoSaveDataScreen : MonoBehaviour
    {
        public bool isVisible
        {
            get { return this.gameObject.activeSelf; }
            set { this.gameObject.SetActive(value); }
        }

        private void Start()
        {
            isVisible = false;
        }
    }
}
