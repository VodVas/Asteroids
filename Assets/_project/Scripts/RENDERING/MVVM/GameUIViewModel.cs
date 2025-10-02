using System;

namespace AsteroidsClone
{
    public class GameUIViewModel
    {
        private readonly GameState _gameState;
        private readonly Player _player;

        public readonly ReactiveProperty<string> ScoreText = new();
        public readonly ReactiveProperty<string> LaserChargesText = new();
        public readonly ReactiveProperty<string> PositionText = new();
        public readonly ReactiveProperty<string> RotationText = new();
        public readonly ReactiveProperty<string> SpeedText = new();
        public readonly ReactiveProperty<string> CooldownText = new();
        public readonly ReactiveProperty<bool> GameOverPanelVisible = new();
        public readonly ReactiveProperty<string> FinalScoreText = new();

        public GameUIViewModel(GameState gameState, Player player)
        {
            _gameState = gameState ?? throw new ArgumentNullException(nameof(gameState));
            _player = player ?? throw new ArgumentNullException(nameof(player));

            _gameState.OnScoreChanged += OnScoreChanged;
            _gameState.OnGameOver += OnGameOver;
            _gameState.OnGameRestarted += OnGameRestarted;
            _player.OnLaserChargesChanged += OnLaserChargesChanged;

            UpdateInitialValues();
        }

        private void UpdateInitialValues()
        {
            OnScoreChanged(_gameState.Score);
            OnLaserChargesChanged(_player.LaserCharges);
            UpdatePlayerStats();
            GameOverPanelVisible.Value = _gameState.IsGameOver;
        }

        public void UpdatePlayerStats()
        {
            PositionText.Value = $"Position: ({_player.Position.x:F1}, {_player.Position.y:F1})";
            RotationText.Value = $"Rotation: {_player.Rotation:F0}°";
            SpeedText.Value = $"Speed: {_player.Speed:F1}";

            if (_player.LaserCooldown > 0)
            {
                CooldownText.Value = $"Cooldown: {_player.LaserCooldown:F1}s";
            }
            else
            {
                CooldownText.Value = "Ready";
            }
        }

        private void OnScoreChanged(int score)
        {
            ScoreText.Value = $"{score}";
        }

        private void OnGameOver()
        {
            GameOverPanelVisible.Value = true;
            FinalScoreText.Value = $"Final Score: {_gameState.Score}";
        }

        private void OnGameRestarted()
        {
            GameOverPanelVisible.Value = false;
        }

        private void OnLaserChargesChanged(int charges)
        {
            LaserChargesText.Value = $"Laser: {charges}";
        }

        public void Dispose()
        {
            if (_gameState != null)
            {
                _gameState.OnScoreChanged -= OnScoreChanged;
                _gameState.OnGameOver -= OnGameOver;
                _gameState.OnGameRestarted -= OnGameRestarted;
            }

            if (_player != null)
            {
                _player.OnLaserChargesChanged -= OnLaserChargesChanged;
            }
        }
    }
}