using UnityEngine;

namespace AsteroidsClone
{
    public sealed class RandomService : IRandomService
    {
        public float Range(float min, float max) => Random.Range(min, max);
        public int Range(int min, int max) => Random.Range(min, max);
        public float Value => Random.value;
    }
}