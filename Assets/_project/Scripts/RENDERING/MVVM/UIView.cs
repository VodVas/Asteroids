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

        private GameUIViewModel _viewModel;

        [Inject]
        public void Construct(GameState gameState, Player player)
        {
            _viewModel = new GameUIViewModel(gameState, player);

            _viewModel.ScoreText.OnChanged += UpdateScoreText;
            _viewModel.LaserChargesText.OnChanged += UpdateLaserChargesText;
            _viewModel.PositionText.OnChanged += UpdatePositionText;
            _viewModel.RotationText.OnChanged += UpdateRotationText;
            _viewModel.SpeedText.OnChanged += UpdateSpeedText;
            _viewModel.CooldownText.OnChanged += UpdateCooldownText;
            _viewModel.GameOverPanelVisible.OnChanged += UpdateGameOverPanel;
            _viewModel.FinalScoreText.OnChanged += UpdateFinalScoreText;

            _gameOverPanel.SetActive(false);
        }

        private void Update()
        {
            if (_viewModel != null)
            {
                _viewModel.UpdatePlayerStats();
            }
        }

        private void UpdateScoreText(string text)
        {
            _scoreText.text = text;
        }

        private void UpdateLaserChargesText(string text)
        {
            _laserChargesText.text = text;
        }

        private void UpdatePositionText(string text)
        {
            _coordinatesText.text = text;
        }

        private void UpdateRotationText(string text)
        {
            _rotationText.text = text;
        }

        private void UpdateSpeedText(string text)
        {
            _speedText.text = text;
        }

        private void UpdateCooldownText(string text)
        {
            _laserCooldownText.text = text;
        }

        private void UpdateGameOverPanel(bool visible)
        {
            _gameOverPanel.SetActive(visible);

            if (visible)
            {
                _finalScoreText.text = $"Final Score: {_viewModel.ScoreText.Value}";
            }
        }

        private void UpdateFinalScoreText(string text)
        {
            _finalScoreText.text = text;
        }

        private void OnDestroy()
        {
            if (_viewModel != null)
            {
                _viewModel.ScoreText.OnChanged -= UpdateScoreText;
                _viewModel.LaserChargesText.OnChanged -= UpdateLaserChargesText;
                _viewModel.PositionText.OnChanged -= UpdatePositionText;
                _viewModel.RotationText.OnChanged -= UpdateRotationText;
                _viewModel.SpeedText.OnChanged -= UpdateSpeedText;
                _viewModel.CooldownText.OnChanged -= UpdateCooldownText;
                _viewModel.GameOverPanelVisible.OnChanged -= UpdateGameOverPanel;
                _viewModel.FinalScoreText.OnChanged -= UpdateFinalScoreText;

                _viewModel.Dispose();
                _viewModel = null;
            }
        }
    }
}