using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "AsteroidConfig", menuName = "Asteroids/Configs/AsteroidConfig")]
    public sealed class AsteroidConfig : ScriptableObject
    {
        [field: SerializeField] public float[] AsteroidSpeeds { get; private set; } = { 2f, 3f, 4f };
        [field: SerializeField] public int[] AsteroidScores { get; private set; } = { 20, 50, 100 };
        [field: SerializeField] public int AsteroidFragments { get; private set; } = 2;
        [field: SerializeField] public float AsteroidColliderRadiusPerSize { get; private set; } = 0.3f;
        [field: SerializeField] public float AsteroidFragmentOffsetDistance { get; private set; } = 0.5f;
        [field: SerializeField] public float AsteroidVisualScaleFactor { get; private set; } = 0.2f;
    }
}