using System;

namespace AsteroidsClone
{
    public sealed class EntityController
    {
        private readonly EntityRegistry _entityRegistry;
        private readonly UfoConfig _ufoConfig;
        private readonly Player _player;

        public EntityRegistry EntityRegistry => _entityRegistry;

        public EntityController(EntityRegistry entityManager, UfoConfig ufoConfig, Player player)
        {
            _entityRegistry = entityManager ?? throw new ArgumentNullException(nameof(entityManager));
            _ufoConfig = ufoConfig ?? throw new ArgumentNullException(nameof(ufoConfig));
            _player = player ?? throw new ArgumentNullException(nameof(player));
        }

        public void Initialize()
        {
            _entityRegistry.Clear();
        }

        public void Update(float deltaTime)
        {
            UpdateEntities(deltaTime);
            _entityRegistry.ProcessChanges();
        }

        private void UpdateEntities(float deltaTime)
        {
            foreach (var entity in _entityRegistry.Entities)
            {
                entity.Update(deltaTime);
                UpdateUfoTarget(entity);
            }
        }

        private void UpdateUfoTarget(IGameEntity entity)
        {
            if (entity is Ufo ufo && _player.IsAlive)
            {
                ufo.UpdateTarget(_player.Position, _ufoConfig.UfoSpeed);
            }
        }
    }
}