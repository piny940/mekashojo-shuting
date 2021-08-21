using UnityEngine;

namespace View
{
    public class Cannon__Player : MonoBehaviour
    {
        [SerializeField, Header("CannonFire__Playerを入れる")] private GameObject _cannonFire__Player;

        // Start is called before the first frame update
        void Start()
        {
            _cannonFire__Player.SetActive(false);

            Controller.ModelClassController.cannon__Player.OnFiringTargetChanged.AddListener((Vector3 firingTarget) =>
            {
                //キャノンを回転させる
                float a = transform.position.x;
                float b = transform.position.y;
                float u = firingTarget.x;
                float v = firingTarget.y;
                float theta = Vector3.SignedAngle(new Vector3(1, 0, 0), new Vector3(u - a, v - b, 0), new Vector3(0, 0, 1));
                this.gameObject.transform.localEulerAngles = new Vector3(0, 0, theta);
            });

            Controller.ModelClassController.cannon__Player.OnFireVisibilityChanged.AddListener((bool isFireVisible) => { _cannonFire__Player.SetActive(isFireVisible); });
        }
    }
}
