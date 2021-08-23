using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class PopupBackgroundImage : MonoBehaviour
    {
        public bool isVisible
        {
            get { return GetComponent<Image>().enabled; }
            set { GetComponent<Image>().enabled = value; }
        }
    }
}
