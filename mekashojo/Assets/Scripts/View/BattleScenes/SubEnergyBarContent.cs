using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class SubEnergyBarContent : MonoBehaviour
    {
        Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Controller.ModelClassController.playerStatusController.OnSubEnergyChanged.AddListener((float subEnergy) =>
            {
                _image.fillAmount = subEnergy / Controller.ModelClassController.playerStatusController.maxSubEnergy;
            });
        }
    }
}
