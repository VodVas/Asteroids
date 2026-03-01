using System.Collections.Generic;

namespace AsteroidsClone
{
    public interface IEntityRegistry
    {
        IReadOnlyList<IGameEntity> Entities { get; }
        void AddEntity(IGameEntity entity);
        void RemoveEntity(IGameEntity entity);
        void ProcessChanges();
        void Clear();
    }
}