using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class NormalEnemyData_scr : MonoBehaviour
{
    public static NormalEnemyData_scr normalEnemyData = null;

    // 通常の敵のステータス一覧
    public IReadOnlyDictionary<normalEnemyType, Dictionary<normalEnemyParameter, float>> normalEnemyStatus { get; private set; }

    private Dictionary<normalEnemyType, Dictionary<normalEnemyParameter, float>> _normalEnemyStatus__Data { get; set; }

    public enum normalEnemyType
    {
        SpreadBullet__SmallDrone,
        SingleBullet__SmallDrone,
        StunBullet__SmallDrone,
        FastBullet__SmallDrone,
        SlowBullet__SmallDrone,
        Missile__MiddleDrone,
        RepeatedFire__MiddleDrone,
        WideBeam__MiddleDrone,
        GuidedBullet__MiddleDrone,
        WidespreadBullet__MiddleDrone,
        SelfDestruct__MiddleDrone,
    }

    public enum normalEnemyParameter
    {
        HP,
        DamageAmount,
        FiringCount,
        FiringInterval,
        StunDuration,
        BulletSpeed,
    }

    private void Awake()
    {
        if (normalEnemyData == null)
        {
            normalEnemyData = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        _normalEnemyStatus__Data = new Dictionary<normalEnemyType, Dictionary<normalEnemyParameter, float>>()
        {
            {
                normalEnemyType.SpreadBullet__SmallDrone,
                new Dictionary<normalEnemyParameter, float>()
                {
                    { normalEnemyParameter.HP, 5 },
                    { normalEnemyParameter.DamageAmount, 10 },
                    { normalEnemyParameter.FiringCount, 5 },
                    { normalEnemyParameter.FiringInterval, 3 },
                    { normalEnemyParameter.StunDuration, -1 },
                    { normalEnemyParameter.BulletSpeed, 5 },
                }
            },
            {
                normalEnemyType.SingleBullet__SmallDrone,
                new Dictionary<normalEnemyParameter, float>()
                {
                    { normalEnemyParameter.HP, 5 },
                    { normalEnemyParameter.DamageAmount, 20 },
                    { normalEnemyParameter.FiringCount, 1 },
                    { normalEnemyParameter.FiringInterval, 2 },
                    { normalEnemyParameter.StunDuration, -1 },
                    { normalEnemyParameter.BulletSpeed, 6 },
                }
            },
            {
                normalEnemyType.StunBullet__SmallDrone,
                new Dictionary<normalEnemyParameter, float>()
                {
                    { normalEnemyParameter.HP, 5 },
                    { normalEnemyParameter.DamageAmount, 20 },
                    { normalEnemyParameter.FiringCount, 1 },
                    { normalEnemyParameter.FiringInterval, 10 },
                    { normalEnemyParameter.StunDuration, 3 },
                    { normalEnemyParameter.BulletSpeed, 6 },
                }
            },
            {
                normalEnemyType.FastBullet__SmallDrone,
                new Dictionary<normalEnemyParameter, float>()
                {
                    { normalEnemyParameter.HP, 5 },
                    { normalEnemyParameter.DamageAmount, 10 },
                    { normalEnemyParameter.FiringCount, 1 },
                    { normalEnemyParameter.FiringInterval, 1 },
                    { normalEnemyParameter.StunDuration, -1 },
                    { normalEnemyParameter.BulletSpeed, 12 },
                }
            },
            {
                normalEnemyType.SlowBullet__SmallDrone,
                new Dictionary<normalEnemyParameter, float>()
                {
                    { normalEnemyParameter.HP, 5 },
                    { normalEnemyParameter.DamageAmount, 50 },
                    { normalEnemyParameter.FiringCount, 1 },
                    { normalEnemyParameter.FiringInterval, 3 },
                    { normalEnemyParameter.StunDuration, -1 },
                    { normalEnemyParameter.BulletSpeed, 4 },
                }
            },
            {
                normalEnemyType.Missile__MiddleDrone,
                new Dictionary<normalEnemyParameter, float>()
                {
                    { normalEnemyParameter.HP, 20 },
                    { normalEnemyParameter.DamageAmount, 10 },
                    { normalEnemyParameter.FiringCount, 3 },
                    { normalEnemyParameter.FiringInterval, 3 },
                    { normalEnemyParameter.StunDuration, -1 },
                    { normalEnemyParameter.BulletSpeed, -1 },
                }
            },
            {
                normalEnemyType.RepeatedFire__MiddleDrone,
                new Dictionary<normalEnemyParameter, float>()
                {
                    { normalEnemyParameter.HP, 10 },
                    { normalEnemyParameter.DamageAmount, 5 },
                    { normalEnemyParameter.FiringCount, 5 },
                    { normalEnemyParameter.FiringInterval, 3 },
                    { normalEnemyParameter.StunDuration, -1 },
                    { normalEnemyParameter.BulletSpeed, 5 },
                }
            },
            {
                normalEnemyType.WideBeam__MiddleDrone,
                new Dictionary<normalEnemyParameter, float>()
                {
                    { normalEnemyParameter.HP, 20 },
                    { normalEnemyParameter.DamageAmount, 10 },
                    { normalEnemyParameter.FiringCount, 6 },
                    { normalEnemyParameter.FiringInterval, 7 },
                    { normalEnemyParameter.StunDuration, -1 },
                    { normalEnemyParameter.BulletSpeed, 100 },
                }
            },
            {
                normalEnemyType.GuidedBullet__MiddleDrone,
                new Dictionary<normalEnemyParameter, float>()
                {
                    { normalEnemyParameter.HP, 10 },
                    { normalEnemyParameter.DamageAmount, 10 },
                    { normalEnemyParameter.FiringCount, 1 },
                    { normalEnemyParameter.FiringInterval, 5 },
                    { normalEnemyParameter.StunDuration, -1 },
                    { normalEnemyParameter.BulletSpeed, 3 },
                }
            },
            {
                normalEnemyType.WidespreadBullet__MiddleDrone,
                new Dictionary<normalEnemyParameter, float>()
                {
                    { normalEnemyParameter.HP, 20 },
                    { normalEnemyParameter.DamageAmount, 10 },
                    { normalEnemyParameter.FiringCount, 8 },
                    { normalEnemyParameter.FiringInterval, 5 },
                    { normalEnemyParameter.StunDuration, -1 },
                    { normalEnemyParameter.BulletSpeed, 6 },
                }
            },
            {
                normalEnemyType.SelfDestruct__MiddleDrone,
                new Dictionary<normalEnemyParameter, float>()
                {
                    { normalEnemyParameter.HP, 10 },
                    { normalEnemyParameter.DamageAmount, 80 },
                    { normalEnemyParameter.FiringCount, -1 },
                    { normalEnemyParameter.FiringInterval, -1 },
                    { normalEnemyParameter.StunDuration, -1 },
                    { normalEnemyParameter.BulletSpeed, -1 },
                }
            }
        };

        normalEnemyStatus = new ReadOnlyDictionary<normalEnemyType, Dictionary<normalEnemyParameter, float>>(_normalEnemyStatus__Data);
    }
}
