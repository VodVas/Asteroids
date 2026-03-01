using UnityEngine;

namespace AsteroidsClone
{
    [CreateAssetMenu(fileName = "InputConfig", menuName = "Asteroids/Configs/InputConfig")]
    public sealed class InputConfig : ScriptableObject
    {
        [field: SerializeField] public KeyCode ThrustKey { get; private set; } = KeyCode.W;
        [field: SerializeField] public KeyCode BulletKey { get; private set; } = KeyCode.Mouse0;
        [field: SerializeField] public KeyCode LaserKey { get; private set; } = KeyCode.Space;
        [field: SerializeField] public KeyCode RestartKey { get; private set; } = KeyCode.R;
        [field: SerializeField] public string RotationAxis { get; private set; } = "Horizontal";
    }
}