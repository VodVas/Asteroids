using UnityEngine;

namespace AsteroidsClone
{
    public interface IEntitySpawner
    {
        void Update(float deltaTime);
        void Reset();
        void SpawnAsteroid();
        void SpawnUfo();
    }
}
