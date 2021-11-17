using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class StageSelect_Overlay : MonoBehaviour
    {
        public bool IsVisible
        {
            get { return this.GetComponent<Image>().enabled; }
            set { this.GetComponent<Image>().enabled = value; }
        }

        void Start()
        {
            this.IsVisible = false;
        }
    }
}
