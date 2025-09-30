using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "SpawningConfig", menuName = "Asteroids/Configs/SpawningConfig")]
    public sealed class SpawningConfig : ScriptableObject
    {
        [field: SerializeField] public float InitialSpawnDelay { get; private set; } = 3f;
        [field: SerializeField] public float MinSpawnDelay { get; private set; } = 0.5f;
        [field: SerializeField] public float SpawnAcceleration { get; private set; } = 0.95f;
        [field: SerializeField] public float UfoSpawnDelayMultiplier { get; private set; } = 3f;
        [field: SerializeField] public int InitialAsteroidsCount { get; private set; } = 3;
        [field: SerializeField] public int DefaultAsteroidSize { get; private set; } = 3;
        [field: SerializeField] public float EdgeSpawnMargin { get; private set; } = 1f;
    }
}