using UnityEngine;

namespace View
{
    public class SEPlayer : MonoBehaviour
    {
        public static SEPlayer sePlayer = null;
        [HideInInspector] public AudioSource audioSource;

        private void Awake()
        {
            if (sePlayer == null)
            {
                sePlayer = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }
    }
}
