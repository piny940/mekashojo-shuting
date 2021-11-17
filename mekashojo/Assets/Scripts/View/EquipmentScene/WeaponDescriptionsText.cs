using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class WeaponDescriptionsText : MonoBehaviour
    {
        public bool isVisible
        {
            get { return this.gameObject.activeSelf; }
            set { this.gameObject.SetActive(value); }
        }

        public string text
        {
            get { return GetComponent<Text>().text; }
            set { GetComponent<Text>().text = value; }
        }
    }
}
