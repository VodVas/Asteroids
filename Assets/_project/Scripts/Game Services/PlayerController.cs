using System;

namespace AsteroidsClone
{
    public sealed class PlayerController : IDisposable
    {
        private readonly Player _player;
        private readonly IInputService _inputService;

        public Player Player => _player;
        public event Action<Player> OnPlayerDestroyed;

        public PlayerController(Player player, IInputService inputService)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));

            _player.OnDestroyed += OnPlayerDestroyed;
        }

        public void Initialize()
        {
            _player.Reset();
        }

        public void Update(float deltaTime)
        {
            if (!_player.IsAlive) return;

            _player.Rotate(_inputService.RotationInput, deltaTime);
            _player.Thrust(_inputService.IsThrusting, deltaTime);
            _player.UpdatePosition(deltaTime);
            _player.UpdateLaser(deltaTime);
        }

        public void Dispose()
        {
            if (_player != null)
            {
                _player.OnDestroyed -= OnPlayerDestroyed;
            }
        }
    }
}