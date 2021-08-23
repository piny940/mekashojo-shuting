using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class MotionPreview : MonoBehaviour
    {
        public Color color
        {
            get { return GetComponent<Image>().color; }
            set { GetComponent<Image>().color = value; }
        }

        public bool isVisible
        {
            get { return GetComponent<Image>().enabled; }
            set { GetComponent<Image>().enabled = value; }
        }
    }
}
