using UnityEngine;

namespace AsteroidsClone
{
    public sealed class PlayerViewRenderer
    {
        private ViewConfig _viewConfig;
        private GameObject _playerPrefab;
        private GameObject _playerView;
        private ThrusterToggler _thrusterToggler;
        private float _playerRotateThreshold;

        public void Initialize(ViewConfig viewConfig, GameObject playerPrefab)      
        {
            _viewConfig = viewConfig ?? throw new System.ArgumentNullException(nameof(viewConfig));
            _playerPrefab = playerPrefab ?? throw new System.ArgumentNullException(nameof(playerPrefab));
            _playerRotateThreshold = viewConfig.PlayerViewRotationOffset;
        }

        public void CreatePlayerView()
        {
            if (_playerView == null)
            {
                _playerView = Object.Instantiate(_playerPrefab);
            }
            _playerView.SetActive(true);

            _thrusterToggler = _playerView.GetComponent<ThrusterToggler>();
        }

        public void UpdatePlayerView(Player player)
        {
            if (_playerView != null && player.IsAlive)
            {
                _playerView.transform.position = player.Position;
                _playerView.transform.rotation = Quaternion.Euler(0, 0, player.Rotation + _playerRotateThreshold);

                if (_thrusterToggler != null)
                {
                    _thrusterToggler.SetThrusterActive(player.IsThrusting);
                }
            }
        }

        public void SetPlayerActive(bool active)
        {
            if (_playerView != null)
            {
                _playerView.SetActive(active);
            }
        }
    }
}