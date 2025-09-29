using System;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class Ufo : IGameEntity
    {
        private Vector2 _position;
        private readonly float _halfScreenWidth;
        private readonly float _halfScreenHeight;

        public event Action<Vector2, float> OnTransformChanged;
        public event Action OnDeactivated;

        public Vector2 Position => _position;
        public EntityType Type => EntityType.Ufo;

        public Vector2 Velocity { get; private set; }
        public bool IsActive { get; private set; }
        public float Rotation { get; private set; } = GameConstants.INITIAL_ROTATION;
        public int Id { get; }

        public Ufo(int id, Vector2 position, ScreenConfig screenConfig)
        {
            Id = id;
            _position = position;
            Velocity = Vector2.zero;
            IsActive = true;
            
            _halfScreenWidth = screenConfig.ScreenWidth / GameConstants.HALF_DIVISOR;
            _halfScreenHeight = screenConfig.ScreenHeight / GameConstants.HALF_DIVISOR;
        }

        public void UpdateTarget(Vector2 targetPosition, float speed)
        {
            if (!IsActive) return;

            var direction = (targetPosition - _position).normalized;
            Velocity = direction * speed;
        }

        public void Update(float deltaTime)
        {
            if (!IsActive) return;

            _position += Velocity * deltaTime;

            if (_position.x > _halfScreenWidth) _position.x = -_halfScreenWidth;
            else if (_position.x < -_halfScreenWidth) _position.x = _halfScreenWidth;

            if (_position.y > _halfScreenHeight) _position.y = -_halfScreenHeight;
            else if (_position.y < -_halfScreenHeight) _position.y = _halfScreenHeight;

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