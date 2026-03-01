using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "UfoConfig", menuName = "Asteroids/Configs/UfoConfig")]
    public sealed class UfoConfig : ScriptableObject
    {
        [field: SerializeField] public float UfoSpeed { get; private set; } = 3f;
        [field: SerializeField] public int UfoScore { get; private set; } = 200;
    }
}