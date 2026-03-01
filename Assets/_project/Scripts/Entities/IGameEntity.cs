using System;
using UnityEngine;

namespace AsteroidsClone
{
    public interface IGameEntity
    {
        int Id { get; }
        Vector2 Position { get; }
        Vector2 Velocity { get; }
        float Rotation { get; }
        bool IsActive { get; }
        EntityType Type { get; }
        
        event Action<Vector2, float> OnTransformChanged;
        event Action OnDeactivated;
        
        void Update(float deltaTime);
    }
}