using System.Collections.Generic;
using UnityEngine;

namespace AsteroidsClone
{
    public class EntityViewManager
    {
        private readonly ObjectPoolManager _poolManager;
        private readonly AsteroidConfig _asteroidConfig;
        private readonly ICollisionTriggerRouter _collisionRouter;
        private readonly Dictionary<int, EntityView> _entityViews = new Dictionary<int, EntityView>();
        private readonly Dictionary<int, EntityType> _entityTypes = new Dictionary<int, EntityType>();

        public EntityViewManager(
            ObjectPoolManager poolManager,
            AsteroidConfig asteroidConfig,
            ICollisionTriggerRouter collisionRouter)
        {
            _poolManager = poolManager;
            _asteroidConfig = asteroidConfig;
            _collisionRouter = collisionRouter;
        }

        public void CreateEntityView(IGameEntity entity)
        {
            if (entity == null || _entityViews.ContainsKey(entity.Id))
                return;

            var viewGameObject = _poolManager.GetFromPool(entity.Type);
            if (viewGameObject == null)
            {
                Debug.LogWarning($"Failed to get GameObject from pool for entity type {entity.Type}");
                return;
            }

            if (viewGameObject.TryGetComponent(out CollisionProxy2D proxy))
            {
                proxy.Initialize(_collisionRouter);
            }

            EntityView view = GetOrAddEntityView(viewGameObject, entity.Type);

            if (view != null)
            {
                if (entity.Type == EntityType.Asteroid && view is AsteroidView asteroidView)
                {
                    asteroidView.UpdateScale(_asteroidConfig.AsteroidVisualScaleFactor);
                }

                view.LinkToEntity(entity);

                _entityViews[entity.Id] = view;
                _entityTypes[entity.Id] = entity.Type;

                viewGameObject.SetActive(true);
            }
        }

        private EntityView GetOrAddEntityView(GameObject viewObj, EntityType entityType)
        {
            return entityType switch
            {
                EntityType.Asteroid => viewObj.GetComponent<AsteroidView>() ?? viewObj.AddComponent<AsteroidView>(),
                _ => viewObj.GetComponent<EntityView>() ?? viewObj.AddComponent<EntityView>()
            };
        }

        public void RemoveEntityView(int entityId)
        {
            if (!_entityViews.TryGetValue(entityId, out var view))
                return;

            view.UnlinkFromEntity();

            if (_entityTypes.TryGetValue(entityId, out var entityType))
            {
                _poolManager.ReturnToPool(view.gameObject, entityType);
            }

            _entityViews.Remove(entityId);
            _entityTypes.Remove(entityId);
        }

        public void ClearAllViews()
        {
            foreach (var kvp in _entityViews)
            {
                var view = kvp.Value;
                view.UnlinkFromEntity();
                view.gameObject.SetActive(false);
            }

            _entityViews.Clear();
            _entityTypes.Clear();
        }

        public bool HasView(int entityId) => _entityViews.ContainsKey(entityId);

        public void CleanupInactiveViews(IReadOnlyList<IGameEntity> activeEntities)
        {
            var activeIds = new HashSet<int>();

            foreach (var entity in activeEntities)
            {
                if (entity.IsActive)
                    activeIds.Add(entity.Id);
            }

            var toRemove = new List<int>();

            foreach (var viewId in _entityViews.Keys)
            {
                if (!activeIds.Contains(viewId))
                    toRemove.Add(viewId);
            }

            foreach (var id in toRemove)
            {
                RemoveEntityView(id);
            }
        }
    }
}