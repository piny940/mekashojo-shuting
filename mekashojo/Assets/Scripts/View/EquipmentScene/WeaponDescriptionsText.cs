using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class WeaponDescriptionsText : MonoBehaviour
    {
        public string text
        {
            get { return GetComponent<Text>().text; }
            set { GetComponent<Text>().text = value; }
        }
    }
}
