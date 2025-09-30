using UnityEngine;

namespace AsteroidsClone
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField] private EntityType _entityType;
        
        protected IGameEntity _entity;
        protected bool _isLinked;

        public virtual void LinkToEntity(IGameEntity entity)
        {
            UnlinkFromEntity();
            
            _entity = entity;
            
            if (_entity != null)
            {
                _entity.OnTransformChanged += OnEntityTransformChanged;
                _entity.OnDeactivated += OnEntityDeactivated;
                _isLinked = true;
                
                OnEntityTransformChanged(_entity.Position, _entity.Rotation);
                SetupInitialState();
            }
        }

        public virtual void UnlinkFromEntity()
        {
            if (_entity != null && _isLinked)
            {
                _entity.OnTransformChanged -= OnEntityTransformChanged;
                _entity.OnDeactivated -= OnEntityDeactivated;
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
            if (!_isLinked) return;
            
            transform.position = position;
            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }

        private void OnEntityDeactivated()
        {
            if (!_isLinked) return;
            
            gameObject.SetActive(false);
            UnlinkFromEntity();
        }

        private void OnDestroy()
        {
            UnlinkFromEntity();
        }
    }
}