using UnityEngine;

namespace AsteroidsClone
{
    public sealed class CollisionProxy2D : MonoBehaviour
    {
        private enum ProxyType
        {
            Bullet = 0,
            Player = 1
        }

        [SerializeField] private ProxyType _proxyType = ProxyType.Bullet;

        private ICollisionTriggerRouter _router;
        private EntityIdLink _entityIdLink;

        private void Awake()
        {
            TryGetComponent(out _entityIdLink);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_router == null || other == null) return;

            if (_proxyType == ProxyType.Player)
            {
                _router.HandlePlayerTrigger(other);
                return;
            }

            if (_entityIdLink == null || _entityIdLink.EntityId <= 0) return;
            _router.HandleBulletTrigger(_entityIdLink.EntityId, other);
        }

        public void Initialize(ICollisionTriggerRouter router)
        {
            _router = router;

            if (_entityIdLink == null)
            {
                TryGetComponent(out _entityIdLink);
            }
        }
    }
}