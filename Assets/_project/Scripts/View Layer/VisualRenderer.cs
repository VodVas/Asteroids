using UnityEngine;
using Zenject;

namespace AsteroidsClone
{
    public sealed class VisualRenderer : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _asteroidPrefab;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private GameObject _ufoPrefab;
        [SerializeField] private ParticleSystem _laserParticlePrefab;

        private GameState _gameState;
        private Player _player;
        private EntityRegistry _entityRegistry;
        private WeaponController _weaponController;
        private ViewConfig _viewConfig;
        private AsteroidConfig _asteroidConfig;
        private WeaponsConfig _weaponsConfig;
        private ObjectPoolManager _poolManager;
        private PlayerViewRenderer _playerViewRenderer;
        private EntityViewManager _entityViewManager;
        private LaserParticleBeamManager _laserParticleBeamManager;

        [Inject]
        public void Construct(GameState gameState, Player player, EntityRegistry entityRegistry,
            WeaponController weaponController, ViewConfig viewConfig, AsteroidConfig asteroidConfig, WeaponsConfig weaponsConfig,
            ObjectPoolManager poolManager, PlayerViewRenderer playerViewManager,
            LaserParticleBeamManager laserEffectService)
        {
            _gameState = gameState;
            _player = player;
            _entityRegistry = entityRegistry;
            _weaponController = weaponController;
            _viewConfig = viewConfig;
            _asteroidConfig = asteroidConfig;
            _weaponsConfig = weaponsConfig;
            _poolManager = poolManager;
            _playerViewRenderer = playerViewManager;
            _laserParticleBeamManager = laserEffectService;
            
            _entityViewManager = new EntityViewManager(_poolManager, _asteroidConfig);
        }

        private void Start()
        {
            if (_gameState == null || _player == null || _entityRegistry == null || _weaponController == null)
            {
                Debug.LogError("One of the required dependencies is null! Check Zenject setup.");
                enabled = false;
                return;
            }

            InitializeServices();
            CreateViews();
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
            _poolManager?.ClearPools();
        }

        private void LateUpdate()
        {
            if (_gameState == null || _player == null || _entityRegistry == null) return;

            _playerViewRenderer.UpdatePlayerView(_player);
            
            UpdateEntityViews();
            
            _laserParticleBeamManager?.Update(Time.deltaTime);
        }

        private void UpdateEntityViews()
        {
            var entities = _entityRegistry.Entities;
            
            foreach (var entity in entities)
            {
                if (!entity.IsActive) continue;
                
                if (!_entityViewManager.HasView(entity.Id))
                {
                    _entityViewManager.CreateEntityView(entity);
                }
            }
            
            _entityViewManager.CleanupInactiveViews(entities);
        }

        private void InitializeServices()
        {
            _poolManager.Initialize(_viewConfig, _asteroidPrefab, _bulletPrefab, _ufoPrefab);
            _poolManager.InitializePools();
            _playerViewRenderer.Initialize(_viewConfig, _playerPrefab);
        }

        private void CreateViews()
        {
            _playerViewRenderer.CreatePlayerView();

            var laserParticle = Instantiate(_laserParticlePrefab);
            _laserParticleBeamManager.Initialize(laserParticle, _weaponsConfig);
        }

        private void SubscribeToEvents()
        {
            _player.OnDestroyed += OnPlayerDestroyed;
            _gameState.OnGameRestarted += OnGameRestarted;
            _weaponController.OnLaserFired += _laserParticleBeamManager.FireLaser;
        }

        private void UnsubscribeFromEvents()
        {
            if (_player != null)
                _player.OnDestroyed -= OnPlayerDestroyed;
                
            if (_gameState != null)
                _gameState.OnGameRestarted -= OnGameRestarted;
                
            if (_weaponController != null)
                _weaponController.OnLaserFired -= _laserParticleBeamManager.FireLaser;
        }

        private void OnPlayerDestroyed(Player player)
        {
            _playerViewRenderer.SetPlayerActive(false);
        }

        private void OnGameRestarted()
        {
            _playerViewRenderer.SetPlayerActive(true);
            _entityViewManager.ClearAllViews();
        }
    }
}