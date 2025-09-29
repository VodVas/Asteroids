using UnityEngine;

namespace AsteroidsClone
{
    public sealed class CollisionMath
    {
        private readonly AsteroidConfig _asteroidConfig;
        private readonly UfoConfig _ufoConfig;
        private readonly WeaponsConfig _weaponsConfig;
        private readonly CollisionConfig _collisionConfig;

        public CollisionMath(AsteroidConfig asteroidConfig, UfoConfig ufoConfig, 
                           WeaponsConfig weaponsConfig, CollisionConfig collisionConfig)
        {
            _asteroidConfig = asteroidConfig;
            _ufoConfig = ufoConfig;
            _weaponsConfig = weaponsConfig;
            _collisionConfig = collisionConfig;
        }

        public bool CheckCollision(Vector2 pos1, Vector2 pos2, float radius)
        {
            return Vector2.Distance(pos1, pos2) < radius;
        }

        public bool CheckLaserHit(Vector2 origin, Vector2 direction, Vector2 targetPos, float targetRadius)
        {
            var toTarget = targetPos - origin;
            var distance = Vector2.Dot(toTarget, direction);

            if (distance < 0 || distance > _weaponsConfig.LaserRange) return false;

            var closest = origin + direction * distance;
            return Vector2.Distance(closest, targetPos) < targetRadius;
        }

        public float GetCollisionRadius(IGameEntity entity)
        {
            return entity switch
            {
                Asteroid asteroid => asteroid.Size * _asteroidConfig.AsteroidColliderRadiusPerSize,
                Ufo => _ufoConfig.UfoColliderRadius,
                _ => _collisionConfig.DefaultColliderRadius
            };
        }
    }
}
