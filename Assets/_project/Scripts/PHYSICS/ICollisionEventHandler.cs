namespace AsteroidsClone
{
    public interface ICollisionEventHandler
    {
        void OnScoreEarned(int points);
        void OnPlayerKilled();
        bool IsGameOver { get; }
    }
}
