using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class CollisionDetector : ICollisionDetector, ICollisionTriggerRouter
    {
        private readonly CollisionHandler _collisionHandler;
        private readonly IEntityRegistry _entityRegistry;
        private readonly GameState _gameState;
        private readonly Player _player;
        private readonly WeaponsConfig _weaponsConfig;

        private readonly Dictionary<int, IGameEntity> _entitiesById = new Dictionary<int, IGameEntity>(GameConstants.ENTITY_DICTIONARY_CAPACITY);
        private readonly RaycastHit2D[] _laserHits = new RaycastHit2D[GameConstants.MAX_LASER_HITS];
        private readonly HashSet<int> _processedLaserTargets = new HashSet<int>();

        public CollisionDetector(
            CollisionHandler collisionHandler,
            IEntityRegistry entityRegistry,
            GameState gameState,
            Player player,
            WeaponsConfig weaponsConfig)
        {
            _collisionHandler = collisionHandler;
            _entityRegistry = entityRegistry;
            _gameState = gameState;
            _player = player;
            _weaponsConfig = weaponsConfig;
        }

        public void CheckCollisions()
        {
            if (_gameState.IsGameOver) return;
            RebuildEntityIndex();
        }

        public void HandleLaserFire(Vector2 origin, Vector2 direction)
        {
            if (_gameState.IsGameOver) return;
            EnsureIndex();

            _processedLaserTargets.Clear();

            int hitCount = Physics2D.RaycastNonAlloc(
                origin,
                direction.normalized,
                _laserHits,
                _weaponsConfig.LaserRange);

            for (int i = 0; i < hitCount; i++)
            {
                if (!TryGetHitEntity(_laserHits[i].collider, _entitiesById, out var entity)) continue;
                if (!IsHostile(entity)) continue;
                if (!_processedLaserTargets.Add(entity.Id)) continue;

                _collisionHandler.HandleLaserHit(entity);
            }
        }

        public void HandlePlayerTrigger(Collider2D otherCollider)
        {
            if (_gameState.IsGameOver || !_player.IsAlive) return;
            EnsureIndex();

            if (!TryGetHitEntity(otherCollider, _entitiesById, out var target)) return;
            if (!IsHostile(target)) return;

            _collisionHandler.HandlePlayerHit(_player, target);
        }

        public void HandleBulletTrigger(int bulletEntityId, Collider2D otherCollider)
        {
            if (_gameState.IsGameOver || bulletEntityId <= 0) return;
            EnsureIndex();

            if (!TryGetEntityById(bulletEntityId, out var bulletEntity)) return;
            if (!(bulletEntity is Bullet bullet) || !bullet.IsActive) return;

            if (!TryGetHitEntity(otherCollider, _entitiesById, out var target)) return;
            if (target.Id == bullet.Id) return;
            if (!IsHostile(target)) return;

            _collisionHandler.HandleBulletHit(bullet, target);
        }

        private void EnsureIndex()
        {
            if (_entitiesById.Count == 0)
            {
                RebuildEntityIndex();
            }
        }

        private void RebuildEntityIndex()
        {
            _entitiesById.Clear();

            var entities = _entityRegistry.Entities;
            for (int i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                if (!entity.IsActive) continue;
                _entitiesById[entity.Id] = entity;
            }
        }

        private bool TryGetEntityById(int entityId, out IGameEntity entity)
        {
            if (_entitiesById.TryGetValue(entityId, out entity))
            {
                return true;
            }

            var entities = _entityRegistry.Entities;
            for (int i = 0; i < entities.Count; i++)
            {
                var candidate = entities[i];
                if (!candidate.IsActive) continue;
                if (candidate.Id != entityId) continue;

                _entitiesById[entityId] = candidate;
                entity = candidate;
                return true;
            }

            entity = null;
            return false;
        }

        private bool TryGetHitEntity(
            Collider2D collider,
            IReadOnlyDictionary<int, IGameEntity> entitiesById,
            out IGameEntity entity)
        {
            entity = null;

            if (collider != null && collider.TryGetComponent(out EntityIdLink entityIdLink))
            {
                return entitiesById.TryGetValue(entityIdLink.EntityId, out entity);
            }

            return false;
        }

        private static bool IsHostile(IGameEntity entity)
        {
            return entity != null &&
                   entity.IsActive &&
                   (entity.Type == EntityType.Asteroid || entity.Type == EntityType.Ufo);
        }
    }
}