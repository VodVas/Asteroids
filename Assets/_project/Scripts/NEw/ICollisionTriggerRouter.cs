using UnityEngine;

namespace AsteroidsClone
{
    public interface ICollisionTriggerRouter
    {
        void HandlePlayerTrigger(Collider2D otherCollider);
        void HandleBulletTrigger(int bulletEntityId, Collider2D otherCollider);
    }
}