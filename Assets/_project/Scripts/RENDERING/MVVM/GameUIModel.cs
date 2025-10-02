using UnityEngine;

namespace AsteroidsClone
{
    public class GameUIModel
    {
        public readonly ReactiveProperty<int> Score = new();
        public readonly ReactiveProperty<int> LaserCharges = new();
        public readonly ReactiveProperty<Vector2> PlayerPosition = new();
        public readonly ReactiveProperty<float> PlayerRotation = new();
        public readonly ReactiveProperty<float> PlayerSpeed = new();
        public readonly ReactiveProperty<float> LaserCooldown = new();
        public readonly ReactiveProperty<bool> IsGameOver = new();
    }
}