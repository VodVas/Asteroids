using System;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class WeaponController
    {
        private readonly Player _player;
        private readonly WeaponsConfig _weaponsConfig;
        private readonly IInputService _inputService;
        private readonly IEntityFactory _entityFactory;
        private readonly EntityRegistry _entityRegistry;
        private readonly CollisionDetector _collisionDetector;

        private float _bulletCooldown;

        public event Action<Vector2, Vector2> OnLaserFired;

        public WeaponController(Player player, WeaponsConfig weaponsConfig, IInputService inputService,
            IEntityFactory entityFactory, EntityRegistry entityRegistry, CollisionDetector collisionService)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _weaponsConfig = weaponsConfig ?? throw new ArgumentNullException(nameof(weaponsConfig));
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
            _entityFactory = entityFactory ?? throw new ArgumentNullException(nameof(entityFactory));
            _entityRegistry = entityRegistry ?? throw new ArgumentNullException(nameof(entityRegistry));
            _collisionDetector = collisionService ?? throw new ArgumentNullException(nameof(collisionService));
        }

        public void Update(float deltaTime)
        {
            if (!_player.IsAlive) return;

            UpdateBulletCooldown(deltaTime);
            HandleShooting();
        }

        private void UpdateBulletCooldown(float deltaTime)
        {
            if (_bulletCooldown > 0)
                _bulletCooldown -= deltaTime;
        }

        private void HandleShooting()
        {
            if (_inputService.FireBullet && _bulletCooldown <= 0)
            {
                FireBullet();
                _bulletCooldown = _weaponsConfig.BulletCooldown;
            }

            if (_inputService.FireLaser && _player.TryFireLaser())
            {
                FireLaser();
            }
        }

        private void FireBullet()
        {
            var direction = CalculateDirection();
            var position = _player.Position + direction * _weaponsConfig.BulletPositionOffset;
            var bullet = _entityFactory.CreateBullet(position, direction, _player.Velocity);
            _entityRegistry.AddEntity(bullet);
        }

        private void FireLaser()
        {
            var direction = CalculateDirection();
            _collisionDetector.HandleLaserFire(_player.Position, direction);
            OnLaserFired?.Invoke(_player.Position, direction);
        }

        private Vector2 CalculateDirection()
        {
            var radians = _player.Rotation * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        }
    }
}