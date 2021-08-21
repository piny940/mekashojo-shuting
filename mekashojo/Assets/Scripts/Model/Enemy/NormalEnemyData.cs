using UnityEngine;

namespace Model
{
    [CreateAssetMenu(menuName = "ScriptableObject/NormalEnemyData")]
    public class NormalEnemyData : ScriptableObject
    {
        public enum normalEnemyType
        {
            SpreadBullet,
            SingleBullet,
            StunBullet,
            FastBullet,
            SlowBullet,
            Missile,
            RepeatedFire,
            WideBeam,
            GuidedBullet,
            WideSpreadBullet,
            SelfDestruct,
        }

        public normalEnemyType type;
        public float hp = 10;
        public float movingSpeed = 1;
        public float damageAmount;
        public int firintgAmount;
        public float firingInterval;
        public int shortFiringIntervalFrameAmount;
        public float beamTime;
        public float beamNoticingTime;
        public float bulletSpeed;
    }
}
