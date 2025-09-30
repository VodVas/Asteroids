using UnityEngine;

namespace AsteroidsClone
{
    public class AsteroidView : EntityView
    {
        [SerializeField] private float _visualScaleFactor = 0.2f;
        
        protected override void SetupInitialState()
        {
            base.SetupInitialState();
            
            if (_entity is Asteroid asteroid)
            {
                SetAsteroidScale(asteroid.Size);
            }
        }

        public override void LinkToEntity(IGameEntity entity)
        {
            if (entity.Type != EntityType.Asteroid)
            {
                Debug.LogError($"AsteroidView can only be linked to Asteroid entities, got {entity.Type}");
                return;
            }
            
            base.LinkToEntity(entity);
        }

        private void SetAsteroidScale(int size)
        {
            float scale = size * _visualScaleFactor;
            transform.localScale = Vector3.one * scale;
        }

        public void UpdateScale(float scaleFactor)
        {
            _visualScaleFactor = scaleFactor;
            if (_entity is Asteroid asteroid)
            {
                SetAsteroidScale(asteroid.Size);
            }
        }
    }
}