using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Asteroids/Configs/PlayerConfig")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float PlayerAcceleration { get; private set; } = 10f;
        [field: SerializeField] public float PlayerMaxSpeed { get; private set; } = 8f;
        [field: SerializeField] public float PlayerRotationSpeed { get; private set; } = 180f;
        [field: SerializeField] public float PlayerDrag { get; private set; } = 0.99f;
    }
}