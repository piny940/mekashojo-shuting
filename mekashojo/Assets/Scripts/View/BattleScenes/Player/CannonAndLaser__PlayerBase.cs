using UnityEngine;

namespace View
{
    public class CannonAndLaser__PlayerBase : MonoBehaviour
    {
        private const float MAX_ROTATE_ANGLE = 45f;

        [SerializeField, Header("Fire__Playerを入れる")] protected GameObject fire__Player;

        protected void RotateFire(Vector3 firingTarget)
        {
            float a = transform.position.x;
            float b = transform.position.y;
            float u = firingTarget.x;
            float v = firingTarget.y;
            float theta = Vector3.SignedAngle(new Vector3(1, 0, 0), new Vector3(u - a, v - b, 0), new Vector3(0, 0, 1));

            // ビームが回転できる角の大きさに制限を設ける
            if (theta < -MAX_ROTATE_ANGLE)
            {
                this.gameObject.transform.localEulerAngles = new Vector3(0, 0, -MAX_ROTATE_ANGLE);
                return;
            }
            else if (theta > MAX_ROTATE_ANGLE)
            {
                this.gameObject.transform.localEulerAngles = new Vector3(0, 0, MAX_ROTATE_ANGLE);
                return;
            }

            this.gameObject.transform.localEulerAngles = new Vector3(0, 0, theta);
        }
    }
}
