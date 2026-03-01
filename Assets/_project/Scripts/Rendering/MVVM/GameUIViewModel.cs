using R3;

namespace AsteroidsClone
{
    public class GameUIViewModel
    {
        private readonly GameState _gameState;
        private readonly Player _player;

        public ReactiveProperty<string> ScoreText { get; } = new();
        public ReactiveProperty<string> LaserChargesText { get; } = new();
        public ReactiveProperty<string> PositionText { get; } = new();
        public ReactiveProperty<string> RotationText { get; } = new();
        public ReactiveProperty<string> SpeedText { get; } = new();
        public ReactiveProperty<string> CooldownText { get; } = new();
        public ReactiveProperty<bool> GameOverPanelVisible { get; } = new();
        public ReactiveProperty<string> FinalScoreText { get; } = new();

        public GameUIViewModel(GameState gameState, Player player)
        {
            _gameState = gameState;
            _player = player;

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

        private void OnScoreChanged(int score) => ScoreText.Value = $"{score}";
        private void OnGameRestarted() => GameOverPanelVisible.Value = false;


        private void OnGameOver()
        {
            GameOverPanelVisible.Value = true;
            FinalScoreText.Value = $"Final Score: {_gameState.Score}";
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