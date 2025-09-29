using System;
using UnityEngine;

namespace AsteroidsClone
{
    public sealed class Player
    {
        private Vector2 _position;
        private readonly float _halfScreenWidth;
        private readonly float _halfScreenHeight;
        private readonly float _rotationSpeed;
        private readonly float _acceleration;
        private readonly float _maxSpeed;
        private readonly float _drag;
        private readonly int _maxLaserCharges;
        private readonly float _laserRechargeTime;

        public event Action<Player> OnDestroyed;
        public event Action<int> OnLaserChargesChanged;

        public Vector2 Position => _position;
        public float Speed => Velocity.magnitude;

        public Vector2 Velocity { get; private set; }
        public float Rotation { get; private set; }
        public int LaserCharges { get; private set; }
        public float LaserCooldown { get; private set; }
        public bool IsThrusting { get; private set; }
        public bool IsAlive { get; private set; }

        public Player(ScreenConfig screenConfig, PlayerConfig playerConfig, WeaponsConfig weaponsConfig)
        {
            _halfScreenWidth = screenConfig.ScreenWidth / GameConstants.HALF_DIVISOR;
            _halfScreenHeight = screenConfig.ScreenHeight / GameConstants.HALF_DIVISOR;
            _rotationSpeed = playerConfig.PlayerRotationSpeed;
            _acceleration = playerConfig.PlayerAcceleration;
            _maxSpeed = playerConfig.PlayerMaxSpeed;
            _drag = playerConfig.PlayerDrag;
            _maxLaserCharges = weaponsConfig.MaxLaserCharges;
            _laserRechargeTime = weaponsConfig.LaserRechargeTime;
            
            Reset();
        }

        public void Reset()
        {
            _position = Vector2.zero;
            Velocity = Vector2.zero;
            Rotation = GameConstants.INITIAL_ROTATION;
            LaserCharges = _maxLaserCharges;
            LaserCooldown = GameConstants.InitialLaserCooldown;
            IsThrusting = false;
            IsAlive = true;
        }

        public void Rotate(float input, float deltaTime)
        {
            Rotation += input * _rotationSpeed * deltaTime;
            Rotation = (Rotation % GameConstants.FULL_ROTATION_DEGREES + GameConstants.FULL_ROTATION_DEGREES) % GameConstants.FULL_ROTATION_DEGREES;
        }

        public void Thrust(bool isThrusting, float deltaTime)
        {
            IsThrusting = isThrusting;

            if (isThrusting)
            {
                var radians = Rotation * Mathf.Deg2Rad;
                var direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

                Velocity += direction * _acceleration * deltaTime;

                if (Velocity.magnitude > _maxSpeed)
                {
                    Velocity = Velocity.normalized * _maxSpeed;
                }
            }
            else
            {
                Velocity *= _drag;
            }
        }

        public void UpdatePosition(float deltaTime)
        {
            _position += Velocity * deltaTime;
            _position = WrapPosition(_position);
        }

        public void UpdateLaser(float deltaTime)
        {
            if (LaserCharges < _maxLaserCharges)
            {
                LaserCooldown += deltaTime;

                if (LaserCooldown >= _laserRechargeTime)
                {
                    LaserCharges++;
                    LaserCooldown = GameConstants.InitialLaserCooldown;
                    OnLaserChargesChanged?.Invoke(LaserCharges);
                }
            }
        }

        public bool TryFireLaser()
        {
            if (LaserCharges > 0)
            {
                LaserCharges--;
                LaserCooldown = GameConstants.InitialLaserCooldown;
                OnLaserChargesChanged?.Invoke(LaserCharges);

                return true;
            }

            return false;
        }

        public void Kill()
        {
            if (IsAlive)
            {
                IsAlive = false;
                OnDestroyed?.Invoke(this);
            }
        }

        private Vector2 WrapPosition(Vector2 position)
        {
            if (position.x > _halfScreenWidth)
                position.x = -_halfScreenWidth;
            else if (position.x < -_halfScreenWidth)
                position.x = _halfScreenWidth;

            if (position.y > _halfScreenHeight)
                position.y = -_halfScreenHeight;
            else if (position.y < -_halfScreenHeight)
                position.y = _halfScreenHeight;

            return position;
        }
    }
}