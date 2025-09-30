using UnityEngine;

namespace AsteroidsClone
{
    public sealed class EntityFactory : IEntityFactory
    {
        private readonly ScreenConfig _screenConfig;
        private readonly AsteroidConfig _asteroidConfig;
        private readonly WeaponsConfig _weaponsConfig;
        private readonly SpawningConfig _spawningConfig;
        private readonly GameState _gameState;
        private readonly IRandomService _randomService;

        public EntityFactory(ScreenConfig screenConfig, AsteroidConfig asteroidConfig, 
                              WeaponsConfig weaponsConfig, SpawningConfig spawningConfig,
                              GameState gameState, IRandomService randomService)
        {
            _screenConfig = screenConfig;
            _asteroidConfig = asteroidConfig;
            _weaponsConfig = weaponsConfig;
            _spawningConfig = spawningConfig;
            _gameState = gameState;
            _randomService = randomService;
        }

        public Asteroid CreateAsteroid(Vector2? position = null, int size = 0)
        {
            var spawnPos = position ?? GetRandomEdgePosition();
            var asteroidSize = size == 0 ? _spawningConfig.DefaultAsteroidSize : size;
            var velocity = GetRandomVelocity(_asteroidConfig.AsteroidSpeeds[GameConstants.MAX_ASTEROID_SIZE_INDEX - asteroidSize]);
            var rotation = _randomService.Range(GameConstants.INITIAL_ROTATION, GameConstants.FULL_ROTATION_DEGREES);
            var rotationSpeed = _randomService.Range(GameConstants.MIN_ASTEROID_ROTATION_SPEED, GameConstants.MAX_ASTEROID_ROTATION_SPEED);
            
            return new Asteroid(_gameState.GetNextEntityId(), spawnPos, velocity, asteroidSize, rotation, rotationSpeed, _screenConfig);
        }

        public Ufo CreateUfo()
        {
            var position = GetRandomEdgePosition();
            return new Ufo(_gameState.GetNextEntityId(), position, _screenConfig);
        }

        public Bullet CreateBullet(Vector2 position, Vector2 direction, Vector2 playerVelocity)
        {
            var velocity = direction * _weaponsConfig.BulletSpeed + playerVelocity * _weaponsConfig.BulletInheritVelocityFactor;
            var rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + _weaponsConfig.VisualBulletRotationOffset;
            
            return new Bullet(_gameState.GetNextEntityId(), position, velocity, rotation, _weaponsConfig.BulletLifetime, _screenConfig);
        }

        private Vector2 GetRandomEdgePosition()
        {
            var side = _randomService.Range(0, GameConstants.SPAWN_SIDES_COUNT);
            var halfWidth = _screenConfig.ScreenWidth / GameConstants.HALF_DIVISOR;
            var halfHeight = _screenConfig.ScreenHeight / GameConstants.HALF_DIVISOR;

            return side switch
            {
                0 => new Vector2(-halfWidth - _spawningConfig.EdgeSpawnMargin, _randomService.Value * _screenConfig.ScreenHeight - halfHeight),
                1 => new Vector2(halfWidth + _spawningConfig.EdgeSpawnMargin, _randomService.Value * _screenConfig.ScreenHeight - halfHeight),
                2 => new Vector2(_randomService.Value * _screenConfig.ScreenWidth - halfWidth, -halfHeight - _spawningConfig.EdgeSpawnMargin),
                _ => new Vector2(_randomService.Value * _screenConfig.ScreenWidth - halfWidth, halfHeight + _spawningConfig.EdgeSpawnMargin)
            };
        }

        private Vector2 GetRandomVelocity(float speed)
        {
            //var angle = _randomService.Value * Mathf.PI * 2f;
            var angle = _randomService.Value * GameConstants.DOUBLE_PI;
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;
        }
    }
}