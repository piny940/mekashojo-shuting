using UnityEngine;

namespace View
{
    public class HowToPlay__Page : MonoBehaviour
    {
        public bool isActive
        {
            get { return this.gameObject.activeSelf; }
            set { this.gameObject.SetActive(value); }
        }
    }
}
