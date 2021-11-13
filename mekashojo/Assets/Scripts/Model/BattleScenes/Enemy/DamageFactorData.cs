using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Model
{
    public class DamageFactorData
    {
        public static DamageFactorData damageFactorData = new DamageFactorData();

        public IReadOnlyDictionary<damageFactorType, float> collisionDamage { get; private set; }

        private Dictionary<damageFactorType, float> _collisionDamage { get; set; }

        public enum damageFactorType
        {
            FiringNormalEnemy, // 自爆型以外の雑魚敵
            NormalEnemy__SelfDestruct, // 自爆型の敵
            Boss, // ボス
        }

        private DamageFactorData()
        {
            _collisionDamage = new Dictionary<damageFactorType, float>()
            {
                { damageFactorType.FiringNormalEnemy, 10 },
                { damageFactorType.NormalEnemy__SelfDestruct, 80 },
                { damageFactorType.Boss, 200 },
            };

            collisionDamage = new ReadOnlyDictionary<damageFactorType, float>(_collisionDamage);
        }
    }
}
