using UnityEngine;

namespace View
{
    public class QuitBattleCheckScreen : MonoBehaviour
    {
        public bool isVisible
        {
            get { return this.gameObject.activeSelf; }
            set { this.gameObject.SetActive(value); }
        }
    }
}
