using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Level : MonoBehaviour
    {
        public string text
        {
            get { return GetComponent<Text>().text; }
            set { GetComponent<Text>().text = value; }
        }

        public bool isVisible
        {
            get { return GetComponent<Text>().enabled; }
            set { GetComponent<Text>().enabled = value; }
        }
    }
}
