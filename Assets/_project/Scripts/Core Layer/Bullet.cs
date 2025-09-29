using System;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class Bullet : IGameEntity
    {
        private Vector2 _position;
        private float _lifetime;
        private readonly float _halfScreenWidth;
        private readonly float _halfScreenHeight;

        public event Action<Vector2, float> OnTransformChanged;
        public event Action OnDeactivated;

        public Vector2 Position => _position;
        public EntityType Type => EntityType.Bullet;

        public Vector2 Velocity { get; private set; }
        public float Rotation { get; private set; }
        public bool IsActive { get; private set; }
        public int Id { get; }

        public Bullet(int id, Vector2 position, Vector2 velocity, float rotation, float lifetime, ScreenConfig screenConfig)
        {
            Id = id;
            _position = position;
            Velocity = velocity;
            Rotation = rotation;
            _lifetime = lifetime;
            IsActive = true;
            
            _halfScreenWidth = screenConfig.ScreenWidth / GameConstants.HALF_DIVISOR;
            _halfScreenHeight = screenConfig.ScreenHeight / GameConstants.HALF_DIVISOR;
        }

        public void Update(float deltaTime)
        {
            if (!IsActive) return;

            _position += Velocity * deltaTime;
            _lifetime -= deltaTime;

            if (_lifetime <= 0)
            {
                IsActive = false;
                OnDeactivated?.Invoke();
                return;
            }

            if (_position.x > _halfScreenWidth) _position.x = -_halfScreenWidth;
            else if (_position.x < -_halfScreenWidth) _position.x = _halfScreenWidth;

            if (_position.y > _halfScreenHeight) _position.y = -_halfScreenHeight;
            else if (_position.y < -_halfScreenHeight) _position.y = _halfScreenHeight;

             OnTransformChanged?.Invoke(_position, Rotation);
        }
    }
}