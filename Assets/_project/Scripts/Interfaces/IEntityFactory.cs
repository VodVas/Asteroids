using UnityEngine;

namespace AsteroidsClone
{
    public interface IEntityFactory
    {
        Asteroid CreateAsteroid(Vector2? position = null, int size = 0);
        Ufo CreateUfo();
        Bullet CreateBullet(Vector2 position, Vector2 direction, Vector2 playerVelocity);
    }
}
