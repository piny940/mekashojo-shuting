using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class PreviewImage : MonoBehaviour
    {
        public Color color
        {
            get { return GetComponent<Image>().color; }
            set { GetComponent<Image>().color = value; }
        }
    }
}
