using UnityEngine;

namespace AsteroidsClone
{
    public class EntityView : MonoBehaviour
    {
        private bool _isLinked;
        private EntityIdLink _idLink;

        protected IGameEntity _entity { get; private set; }

        public virtual void LinkToEntity(IGameEntity entity)
        {
            UnlinkFromEntity();

            _entity = entity;
            if (_entity == null)
            {
                return;
            }

            _entity.OnTransformChanged += OnEntityTransformChanged;
            _entity.OnDeactivated += OnEntityDeactivated;
            _isLinked = true;

            if (TryGetComponent(out _idLink))
            {
                _idLink.Bind(_entity);
            }

            OnEntityTransformChanged(_entity.Position, _entity.Rotation);
            SetupInitialState();
        }

        public virtual void UnlinkFromEntity()
        {
            if (_entity != null && _isLinked)
            {
                _entity.OnTransformChanged -= OnEntityTransformChanged;
                _entity.OnDeactivated -= OnEntityDeactivated;
            }

            if (_idLink != null)
            {
                _idLink.Unbind();
            }

            _entity = null;
            _isLinked = false;
        }

        protected virtual void SetupInitialState()
        {
            gameObject.SetActive(true);
        }

        private void OnEntityTransformChanged(Vector2 position, float rotation)
        {
            if (!_isLinked)
            {
                return;
            }

            transform.position = position;
            transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        }

        private void OnEntityDeactivated()
        {
            if (!_isLinked)
            {
                return;
            }

            gameObject.SetActive(false);
            UnlinkFromEntity();
        }

        private void OnDestroy()
        {
            UnlinkFromEntity();
        }
    }
}
