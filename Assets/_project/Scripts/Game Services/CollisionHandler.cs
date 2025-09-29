using System;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class CollisionHandler
    {
        private readonly AsteroidConfig _asteroidConfig;
        private readonly EntityRegistry _entityRegistry;
        private readonly IEntityFactory _entityFactory;

        public event Action<Player, IGameEntity> PlayerCollisionDetected;
        public event Action<Bullet, IGameEntity> BulletCollisionDetected;
        public event Action<IGameEntity> LaserHitDetected;

        public CollisionHandler(AsteroidConfig asteroidConfig, EntityRegistry entityManager, IEntityFactory entityFactory)
        {
            _asteroidConfig = asteroidConfig;
            _entityRegistry = entityManager;
            _entityFactory = entityFactory;
        }

        public void HandleBulletHit(Bullet bullet, IGameEntity target)
        {
            _entityRegistry.RemoveEntity(bullet);

            if (target is Asteroid asteroid)
            {
                HandleAsteroidDestruction(asteroid);
            }
            else if (target is Ufo ufo)
            {
                ufo.Deactivate();
                _entityRegistry.RemoveEntity(target);
            }

            BulletCollisionDetected?.Invoke(bullet, target);
        }

        public void HandleLaserHit(IGameEntity target)
        {
            if (target is Asteroid asteroid)
            {
                HandleAsteroidDestruction(asteroid);
            }
            else if (target is Ufo ufo)
            {
                ufo.Deactivate();
                _entityRegistry.RemoveEntity(target);
            }

            LaserHitDetected?.Invoke(target);
        }

        public void HandlePlayerHit(Player player, IGameEntity target)
        {
            PlayerCollisionDetected?.Invoke(player, target);
        }

        private void HandleAsteroidDestruction(Asteroid asteroid)
        {
            asteroid.Deactivate();
            _entityRegistry.RemoveEntity(asteroid);

            if (asteroid.Size > 1)
            {
                for (int i = 0; i < _asteroidConfig.AsteroidFragments; i++)
                {
                    var angle = i * (360f / _asteroidConfig.AsteroidFragments) * Mathf.Deg2Rad;
                    var offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _asteroidConfig.AsteroidFragmentOffsetDistance;
                    var fragmentAsteroid = _entityFactory.CreateAsteroid(asteroid.Position + offset, asteroid.Size - 1);
                    _entityRegistry.AddEntity(fragmentAsteroid);
                }
            }
        }
    }
}