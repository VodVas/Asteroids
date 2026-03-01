using UnityEngine;

namespace AsteroidsClone
{
    public sealed class EntityIdLink : MonoBehaviour
    {
        public int EntityId { get; private set; }

        public void Bind(IGameEntity entity)
        {
            if (entity == null)
            {
                EntityId = 0;
                return;
            }

            EntityId = entity.Id;
        }

        public void Unbind()
        {
            EntityId = 0;
        }
    }
}