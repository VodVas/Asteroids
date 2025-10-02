using System;

namespace AsteroidsClone
{
    public sealed class GameState
    {
        private readonly AsteroidConfig _asteroidConfig;
        private readonly UfoConfig _ufoConfig;
        private int _score;
        private bool _isGameOver;
        private int _nextEntityId;

        public event Action<int> OnScoreChanged;
        public event Action OnGameOver;
        public event Action OnGameRestarted;

        public int Score
        {
            get => _score;
            private set
            {
                if (_score != value)
                {
                    _score = value;
                    OnScoreChanged?.Invoke(_score);
                }
            }
        }

        public bool IsGameOver
        {
            get => _isGameOver;
            private set
            {
                if (_isGameOver != value)
                {
                    _isGameOver = value;

                    if (_isGameOver)
                    {
                        OnGameOver?.Invoke();
                    }
                }
            }
        }

        public GameState(AsteroidConfig asteroidConfig, UfoConfig ufoConfig)
        {
            _asteroidConfig = asteroidConfig;
            _ufoConfig = ufoConfig;
        }

        public void AddScore(int points)
        {
            Score += points;
        }

        public void SetGameOver()
        {
            IsGameOver = true;
        }

        public void Reset()
        {
            Score = 0;
            IsGameOver = false;
            _nextEntityId = 1;
            OnGameRestarted?.Invoke();
            OnScoreChanged?.Invoke(Score);
        }

        public int GetNextEntityId() => _nextEntityId++;

        public void SubscribeToCollisions(CollisionHandler collisionHandler)
        {
            collisionHandler.PlayerCollisionDetected -= OnPlayerCollisionDetected;
            collisionHandler.BulletCollisionDetected -= OnBulletCollisionDetected;
            collisionHandler.LaserHitDetected -= OnLaserHitDetected;

            collisionHandler.PlayerCollisionDetected += OnPlayerCollisionDetected;
            collisionHandler.BulletCollisionDetected += OnBulletCollisionDetected;
            collisionHandler.LaserHitDetected += OnLaserHitDetected;
        }

        private void OnPlayerCollisionDetected(Player player, IGameEntity target)
        {
            player.Kill();
            SetGameOver();
        }

        private void OnBulletCollisionDetected(Bullet bullet, IGameEntity target)
        {
            if (target is Asteroid asteroid)
            {
                AddScore(_asteroidConfig.AsteroidScores[3 - asteroid.Size]);
            }
            else if (target is Ufo)
            {
                AddScore(_ufoConfig.UfoScore);
            }
        }

        private void OnLaserHitDetected(IGameEntity target)
        {
            if (target is Asteroid asteroid)
            {
                AddScore(_asteroidConfig.AsteroidScores[3 - asteroid.Size]);
            }
            else if (target is Ufo)
            {
                AddScore(_ufoConfig.UfoScore);
            }
        }
    }
}