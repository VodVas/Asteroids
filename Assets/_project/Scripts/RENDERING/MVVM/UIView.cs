using R3;
using System;
using System.Collections.Generic;
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
        private readonly List<IDisposable> _subscriptions = new();

        [Inject]
        public void Construct(GameUIViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void OnEnable()
        {
            if (_viewModel == null) return;

            SubscribeToViewModel();
            ApplyCurrentValues();
        }

        private void Update()
        {
            _viewModel?.UpdatePlayerStats();
        }

        private void OnDisable()
        {
            DisposeSubscriptions();
        }

        private void OnDestroy()
        {
            DisposeSubscriptions();

            _viewModel?.Dispose();
            _viewModel = null;
        }

        private void SubscribeToViewModel()
        {
            if (_subscriptions.Count > 0) return;

            _subscriptions.Add(_viewModel.ScoreText.Subscribe(UpdateScoreText));
            _subscriptions.Add(_viewModel.LaserChargesText.Subscribe(UpdateLaserChargesText));
            _subscriptions.Add(_viewModel.PositionText.Subscribe(UpdatePositionText));
            _subscriptions.Add(_viewModel.RotationText.Subscribe(UpdateRotationText));
            _subscriptions.Add(_viewModel.SpeedText.Subscribe(UpdateSpeedText));
            _subscriptions.Add(_viewModel.CooldownText.Subscribe(UpdateCooldownText));
            _subscriptions.Add(_viewModel.GameOverPanelVisible.Subscribe(UpdateGameOverPanel));
            _subscriptions.Add(_viewModel.FinalScoreText.Subscribe(UpdateFinalScoreText));
        }

        private void ApplyCurrentValues()
        {
            UpdateScoreText(_viewModel.ScoreText.Value);
            UpdateLaserChargesText(_viewModel.LaserChargesText.Value);
            UpdatePositionText(_viewModel.PositionText.Value);
            UpdateRotationText(_viewModel.RotationText.Value);
            UpdateSpeedText(_viewModel.SpeedText.Value);
            UpdateCooldownText(_viewModel.CooldownText.Value);
            UpdateGameOverPanel(_viewModel.GameOverPanelVisible.Value);
            UpdateFinalScoreText(_viewModel.FinalScoreText.Value);
        }

        private void DisposeSubscriptions()
        {
            for (int i = 0; i < _subscriptions.Count; i++)
            {
                _subscriptions[i]?.Dispose();
            }

            _subscriptions.Clear();
        }

        private void UpdateScoreText(string text) => _scoreText.text = text;
        private void UpdateLaserChargesText(string text) => _laserChargesText.text = text;
        private void UpdatePositionText(string text) => _coordinatesText.text = text;
        private void UpdateRotationText(string text) => _rotationText.text = text;
        private void UpdateSpeedText(string text) => _speedText.text = text;
        private void UpdateCooldownText(string text) => _laserCooldownText.text = text;
        private void UpdateFinalScoreText(string text) => _finalScoreText.text = text;

        private void UpdateGameOverPanel(bool visible)
        {
            _gameOverPanel.SetActive(visible);

            if (visible)
            {
                _finalScoreText.text = $"Final Score: {_viewModel.ScoreText.Value}";
            }
        }
    }
}