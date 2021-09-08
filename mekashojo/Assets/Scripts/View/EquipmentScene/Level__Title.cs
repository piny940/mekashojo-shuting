using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Level__Title : MonoBehaviour
    {
        public bool isVisible
        {
            get { return GetComponent<Text>().enabled; }
            set { GetComponent<Text>().enabled = value; }
        }
    }
}
