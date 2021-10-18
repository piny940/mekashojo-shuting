using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class StageDescriptions : MonoBehaviour
    {
        public bool IsObjectActive
        {
            set { this.gameObject.SetActive(value); }
        }

        public string text
        {
            get { return GetComponent<Text>().text; }
            set { GetComponent<Text>().text = value; }
        }

        public void Start()
        {
            this.IsObjectActive = false;
        }
    }
}
