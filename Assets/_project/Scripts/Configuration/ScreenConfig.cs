using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "ScreenConfig", menuName = "Asteroids/Configs/ScreenConfig")]
    public sealed class ScreenConfig : ScriptableObject
    {
        [field: SerializeField] public float ScreenWidth { get; private set; } = 26f;
        [field: SerializeField] public float ScreenHeight { get; private set; } = 15f;
    }
}