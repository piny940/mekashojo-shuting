using UnityEngine;

namespace View
{
    public class CannonAndLaser__PlayerBase : MonoBehaviour
    {
        [SerializeField, Header("Fire__Playerを入れる")] protected GameObject fire__Player;

        protected void RotateFire(Vector3 firingTarget)
        {
            float a = transform.position.x;
            float b = transform.position.y;
            float u = firingTarget.x;
            float v = firingTarget.y;
            float theta = Vector3.SignedAngle(new Vector3(1, 0, 0), new Vector3(u - a, v - b, 0), new Vector3(0, 0, 1));
            this.gameObject.transform.localEulerAngles = new Vector3(0, 0, theta);
        }
    }
}
