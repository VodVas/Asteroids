using TMPro;
using UnityEngine;
using Zenject;

namespace AsteroidsClone
{
    public sealed class UIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _laserChargesText;
        [SerializeField] private TextMeshProUGUI _coordinatesText;
        [SerializeField] private TextMeshProUGUI _rotationText;
        [SerializeField] private TextMeshProUGUI _speedText;
        [SerializeField] private TextMeshProUGUI _laserCooldownText;
        [SerializeField] private TextMeshProUGUI _finalScoreText;
        [SerializeField] private GameObject _gameOverPanel;

        private GameState _gameState;
        private Player _player;

        [Inject]
        public void Construct(GameState gameState, Player player)
        {
            _gameState = gameState;
            _player = player;
        }

        private void Start()
        {
            if (_gameState == null || _player == null)
            {
                Debug.LogError("GameState or Player is null! Check Zenject setup.");
                enabled = false;
                return;
            }

            _gameState.OnScoreChanged += UpdateScore;
            _gameState.OnGameOver += ShowGameOver;
            _gameState.OnGameRestarted += HideGameOver;
            _player.OnLaserChargesChanged += UpdateLaserCharges;

            _gameOverPanel.SetActive(false);
        }

        private void OnDestroy()
        {
            if (_gameState != null)
            {
                _gameState.OnScoreChanged -= UpdateScore;
                _gameState.OnGameOver -= ShowGameOver;
                _gameState.OnGameRestarted -= HideGameOver;
            }
            
            if (_player != null)
            {
                _player.OnLaserChargesChanged -= UpdateLaserCharges;
            }
        }

        private void Update()
        {
            UpdatePlayerInfo();
        }

        private void UpdateScore(int score)
        {
            _scoreText.text = $"Score: {score}";
        }

        private void UpdateLaserCharges(int charges)
        {
            _laserChargesText.text = $"Laser: {charges}";
        }

        private void UpdatePlayerInfo()
        {
            if (_player == null) return;

            _coordinatesText.text = $"Position: ({_player.Position.x:F1}, {_player.Position.y:F1})";
            _rotationText.text = $"Rotation: {_player.Rotation:F0}°";
            _speedText.text = $"Speed: {_player.Speed:F1}";
            _laserCooldownText.text = $"Cooldown: {_player.LaserCooldown:F1}s";
        }

        private void ShowGameOver()
        {
            _gameOverPanel.SetActive(true);

            if (_gameState != null)
            {
                _finalScoreText.text = $"Final Score: {_gameState.Score}";
            }
        }

        private void HideGameOver()
        {
            _gameOverPanel.SetActive(false);
        }
    }
}