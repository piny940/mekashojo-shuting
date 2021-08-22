using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class HPBarContent : MonoBehaviour
    {
        Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Controller.ModelClassController.playerStatusController.OnHPChanged.AddListener((float hp) =>
            {
                _image.fillAmount = hp / Controller.ModelClassController.playerStatusController.maxHP;
            });
        }
    }
}
