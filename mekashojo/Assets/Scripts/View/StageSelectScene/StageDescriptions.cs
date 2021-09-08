using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class StageDescriptions : MonoBehaviour
    {
        public string text
        {
            get { return GetComponent<Text>().text; }
            set { GetComponent<Text>().text = value; }
        }
    }
}
