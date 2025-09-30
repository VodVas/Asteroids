using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "WeaponsConfig", menuName = "Asteroids/Configs/WeaponsConfig")]
    public sealed class WeaponsConfig : ScriptableObject
    {
        [field: SerializeField] public float BulletSpeed { get; private set; } = 15f;
        [field: SerializeField] public float BulletLifetime { get; private set; } = 2f;
        [field: SerializeField] public float BulletCooldown { get; private set; } = 0.25f;
        [field: SerializeField] public float BulletPositionOffset { get; private set; } = 0.5f;
        [field: SerializeField] public float BulletInheritVelocityFactor { get; private set; } = 0.5f;
        [field: SerializeField] public float VisualBulletRotationOffset { get; private set; } = -90f;
        [field: SerializeField] public int MaxLaserCharges { get; private set; } = 3;
        [field: SerializeField] public float LaserRechargeTime { get; private set; } = 5f;
        [field: SerializeField] public float LaserRange { get; private set; } = 50f;
        [field: SerializeField] public float LaserVisualActiveTime { get; private set; } = 1f;
    }
}