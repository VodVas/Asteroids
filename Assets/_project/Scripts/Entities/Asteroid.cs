using System;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class Asteroid : IGameEntity
    {
        private float _rotationSpeed;
        private Vector2 _position;
        private readonly float _halfScreenWidth;
        private readonly float _halfScreenHeight;

        public event Action<Vector2, float> OnTransformChanged;
        public event Action OnDeactivated;

        public Vector2 Position => _position;
        public EntityType Type => EntityType.Asteroid;

        public Vector2 Velocity { get; private set; }
        public float Rotation { get; private set; }
        public bool IsActive { get; private set; }
        public int Size { get; private set; }
        public int Id { get; }

        public Asteroid(int id, Vector2 position, Vector2 velocity, int size, float rotation, float rotationSpeed, ScreenConfig screenConfig)
        {
            Id = id;
            _position = position;
            Velocity = velocity;
            Size = size;
            Rotation = rotation;
            _rotationSpeed = rotationSpeed;
            IsActive = true;
            
            _halfScreenWidth = screenConfig.ScreenWidth / GameConstants.HALF_DIVISOR;
            _halfScreenHeight = screenConfig.ScreenHeight / GameConstants.HALF_DIVISOR;
        }

        public void Update(float deltaTime)
        {
            if (!IsActive) return;

            _position += Velocity * deltaTime;
            Rotation += _rotationSpeed * deltaTime;

            if (Position.x > _halfScreenWidth) _position.x = -_halfScreenWidth;
            else if (Position.x < -_halfScreenWidth) _position.x = _halfScreenWidth;

            if (Position.y > _halfScreenHeight) _position.y = -_halfScreenHeight;
            else if (Position.y < -_halfScreenHeight) _position.y = _halfScreenHeight;

            OnTransformChanged?.Invoke(_position, Rotation);
        }

        public void Deactivate()
        {
            if (!IsActive) return;
            IsActive = false;
            OnDeactivated?.Invoke();
        }
    }
}