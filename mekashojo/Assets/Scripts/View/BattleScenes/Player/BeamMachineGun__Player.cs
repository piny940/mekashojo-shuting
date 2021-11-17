using UnityEngine;

namespace View
{
    public class BeamMachineGun__Player : FiringBulletBase
    {
        private const float MAX_ROTATE_ANGLE = 45f;
        private const float BULLET_SPEED = 15;

        void Start()
        {
            Controller.BattleScenesController.beamMachineGun__Player.OnBeamMachineGunNumberChanged.AddListener(() =>
            {
                Vector3 direction = Model.InputManager.mousePosition - transform.position;

                float theta = Vector3.SignedAngle(new Vector3(1, 0, 0), new Vector3(direction.x, direction.y, 0), new Vector3(0, 0, 1));

                theta = TurnDegreeToRadian(ResrtictAngle(theta));

                Vector3 bulletVelocity = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0) * BULLET_SPEED;

                Fire(bulletVelocity, Model.EquipmentData.equipmentFireNames.BeamMachineGunFire__Player.ToString());
            });

            Controller.BattleScenesController.beamMachineGun__Player.OnFiringTargetChanged.AddListener(RotateFire);
        }

        private void RotateFire(Vector3 firingTarget)
        {
            float a = transform.position.x;
            float b = transform.position.y;
            float u = firingTarget.x;
            float v = firingTarget.y;
            float theta = Vector3.SignedAngle(new Vector3(1, 0, 0), new Vector3(u - a, v - b, 0), new Vector3(0, 0, 1));

            theta = ResrtictAngle(theta);

            this.gameObject.transform.localEulerAngles = new Vector3(0, 0, theta);
        }

        private float ResrtictAngle(float theta)
        {
            if (theta < -MAX_ROTATE_ANGLE)
            {
                return -MAX_ROTATE_ANGLE;
            }
            else if (theta > MAX_ROTATE_ANGLE)
            {
                return MAX_ROTATE_ANGLE;
            }
            else
            {
                return theta;
            }
        }

        private float TurnDegreeToRadian(float theta)
        {
            return theta * Mathf.PI / 180;
        }
    }
}
