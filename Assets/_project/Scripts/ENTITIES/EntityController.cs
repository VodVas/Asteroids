namespace AsteroidsClone
{
    public sealed class EntityController
    {
        private readonly IEntityRegistry _entityRegistry;
        private readonly UfoConfig _ufoConfig;
        private readonly Player _player;

        public EntityController(IEntityRegistry entityManager, UfoConfig ufoConfig, Player player)
        {
            _entityRegistry = entityManager;
            _ufoConfig = ufoConfig;
            _player = player;
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