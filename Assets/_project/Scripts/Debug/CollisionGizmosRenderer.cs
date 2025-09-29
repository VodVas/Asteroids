using UnityEngine;
using Zenject;

namespace AsteroidsClone
{
    public sealed class CollisionGizmosRenderer : MonoBehaviour
    {
        private EntityRegistry _entityRegistry;
        private Player _player;
        private AsteroidConfig _asteroidConfig;
        private CollisionConfig _collisionConfig;
        private UfoConfig _ufoConfig;

        [Inject]
        public void Construct(EntityRegistry entityRegistry, Player player, AsteroidConfig asteroidConfig, 
                            CollisionConfig collisionConfig, UfoConfig ufoConfig)
        {
            _entityRegistry = entityRegistry;
            _player = player;
            _asteroidConfig = asteroidConfig;
            _collisionConfig = collisionConfig;
            _ufoConfig = ufoConfig;
        }

        private void OnDrawGizmos()
        {
            if (_entityRegistry == null) return;

            Gizmos.color = Color.red;

            foreach (var entity in _entityRegistry.Entities)
            {
                if (!entity.IsActive) continue;

                if (entity is Asteroid asteroid)
                {
                    var radius = asteroid.Size * _asteroidConfig.AsteroidColliderRadiusPerSize;
                    Gizmos.DrawWireSphere(asteroid.Position, radius);
                }
            }

            if (_player != null && _player.IsAlive)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(_player.Position, _collisionConfig.DefaultColliderRadius);
            }

            Gizmos.color = Color.blue;

            foreach (var entity in _entityRegistry.Entities)
            {
                if (!entity.IsActive) continue;

                if (entity is Ufo)
                {
                    Gizmos.DrawWireSphere(entity.Position, _ufoConfig.UfoColliderRadius);
                }
            }

            Gizmos.color = Color.yellow;

            foreach (var entity in _entityRegistry.Entities)
            {
                if (!entity.IsActive) continue;

                if (entity is Bullet)
                {
                    Gizmos.DrawWireSphere(entity.Position, _collisionConfig.DefaultColliderRadius);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_entityRegistry == null) return;

            foreach (var entity in _entityRegistry.Entities)
            {
                if (!entity.IsActive) continue;

                if (entity is Asteroid asteroid)
                {
                    var radius = asteroid.Size * _asteroidConfig.AsteroidColliderRadiusPerSize;
                    var visualSize = asteroid.Size * _asteroidConfig.AsteroidVisualScaleFactor;

                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(asteroid.Position, radius);

                    Gizmos.color = Color.white;
                    Gizmos.DrawWireSphere(asteroid.Position, visualSize);

#if UNITY_EDITOR
                    UnityEditor.Handles.Label(asteroid.Position + Vector2.up * 0.5f,
                        $"Size: {asteroid.Size}\nCollider: {radius:F2}\nVisual: {visualSize:F2}");
#endif
                }
            }
        }
    }
}