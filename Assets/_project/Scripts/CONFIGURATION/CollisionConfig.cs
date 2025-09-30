using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "CollisionConfig", menuName = "Asteroids/Configs/CollisionConfig")]
    public sealed class CollisionConfig : ScriptableObject
    {
        [field: SerializeField] public float DefaultColliderRadius { get; private set; } = 0.3f;
    }
}