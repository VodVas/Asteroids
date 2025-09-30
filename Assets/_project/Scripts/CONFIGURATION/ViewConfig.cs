using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "ViewConfig", menuName = "Asteroids/Configs/ViewConfig")]
    public sealed class ViewConfig : ScriptableObject
    {
        [field: SerializeField] public int AsteroidPoolInitial { get; private set; } = 20;
        [field: SerializeField] public int BulletPoolInitial { get; private set; } = 30;
        [field: SerializeField] public int UfoPoolInitial { get; private set; } = 5;
        [field: SerializeField] public float PlayerViewRotationOffset { get; private set; } = 270f;
    }
}