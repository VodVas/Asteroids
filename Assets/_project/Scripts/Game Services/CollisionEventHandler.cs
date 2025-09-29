namespace AsteroidsClone
{
    public sealed class CollisionEventHandler : ICollisionEventHandler
    {
        private readonly GameState _gameState;

        public CollisionEventHandler(GameState gameState)
        {
            _gameState = gameState;
        }

        public bool IsGameOver => _gameState.IsGameOver;

        public void OnScoreEarned(int points) => _gameState.AddScore(points);
        public void OnPlayerKilled() => _gameState.GameOver();
    }
}
