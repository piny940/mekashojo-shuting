using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class MainEnergyBarContent : MonoBehaviour
    {
        Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Controller.BattleScenesClassController.playerStatusController.OnMainEnergyChanged.AddListener((float mainEnergy) =>
            {
                _image.fillAmount = mainEnergy / Controller.BattleScenesClassController.playerStatusController.maxMainEnergy;
            });
        }
    }
}
