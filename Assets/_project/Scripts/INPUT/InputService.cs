using UnityEngine;
using Zenject;

namespace AsteroidsClone
{
    public sealed class InputService : IInputService
    {
        private InputConfig _inputConfig;

        public bool IsThrusting => Input.GetKey(_inputConfig.ThrustKey);
        public float RotationInput => -Input.GetAxis(_inputConfig.RotationAxis);
        public bool FireBullet => Input.GetKeyDown(_inputConfig.BulletKey);
        public bool FireLaser => Input.GetKeyDown(_inputConfig.LaserKey);
        public bool RestartGame => Input.GetKeyDown(_inputConfig.RestartKey);

        [Inject]
        public void Construct(InputConfig inputConfig)
        {
            _inputConfig = inputConfig;
        }
    }
}