using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class CollisionDetector : ICollisionDetector
    {
        private readonly CollisionMath _collisionMath;
        private readonly CollisionHandler _collisionHandler;
        private readonly EntityRegistry _entityRegistry;
        private readonly GameState _gameState;
        private readonly Player _player;

        public CollisionDetector(CollisionMath collisionMath, CollisionHandler collisionHandler,
                               EntityRegistry entityManager, GameState gameState, Player player)
        {
            _collisionMath = collisionMath;
            _collisionHandler = collisionHandler;
            _entityRegistry = entityManager;
            _gameState = gameState;
            _player = player;
        }

        public void CheckCollisions()
        {
            if (_gameState.IsGameOver) return;

            var entities = _entityRegistry.Entities;

            CheckPlayerCollisions(entities);
            CheckBulletCollisions(entities);
        }

        public void HandleLaserFire(Vector2 origin, Vector2 direction)
        {
            foreach (var entity in _entityRegistry.Entities)
            {
                if (!entity.IsActive) continue;

                if (entity.Type == EntityType.Asteroid || entity.Type == EntityType.Ufo)
                {
                    if (_collisionMath.CheckLaserHit(origin, direction, entity.Position, _collisionMath.GetCollisionRadius(entity)))
                    {
                        _collisionHandler.HandleLaserHit(entity);
                    }
                }
            }
        }

        private void CheckPlayerCollisions(IReadOnlyList<IGameEntity> entities)
        {
            if (!_player.IsAlive) return;

            foreach (var entity in entities)
            {
                if (!entity.IsActive) continue;

                if (entity.Type == EntityType.Asteroid || entity.Type == EntityType.Ufo)
                {
                    if (_collisionMath.CheckCollision(_player.Position, entity.Position, _collisionMath.GetCollisionRadius(entity)))
                    {
                        _collisionHandler.HandlePlayerHit(_player, entity);
                        return;
                    }
                }
            }
        }

        private void CheckBulletCollisions(IReadOnlyList<IGameEntity> entities)
        {
            var bullets = new List<Bullet>();

            foreach (var entity in entities)
            {
                if (entity is Bullet bullet && bullet.IsActive)
                    bullets.Add(bullet);
            }

            foreach (var bullet in bullets)
            {
                foreach (var entity in entities)
                {
                    if (!entity.IsActive || entity == bullet) continue;

                    if (entity.Type == EntityType.Asteroid || entity.Type == EntityType.Ufo)
                    {
                        if (_collisionMath.CheckCollision(bullet.Position, entity.Position, _collisionMath.GetCollisionRadius(entity)))
                        {
                            _collisionHandler.HandleBulletHit(bullet, entity);
                            break;
                        }
                    }
                }
            }
        }
    }
}