using UnityEngine;

namespace AsteroidsClone
{
    public interface IEntitySpawner
    {
        void Update(float deltaTime);
        void Reset();
        void SpawnAsteroid(Vector2? position = null, int size = 0);
        void SpawnUfo();
    }
}
