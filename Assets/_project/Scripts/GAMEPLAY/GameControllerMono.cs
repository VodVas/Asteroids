using UnityEngine;
using Zenject;

namespace AsteroidsClone
{
    public sealed class GameControllerMono : MonoBehaviour
    {
        private EntryPoint _gameOrchestrator;

        [Inject]
        public void Construct(EntryPoint gameOrchestrator)
        {
            _gameOrchestrator = gameOrchestrator;
        }

        private void Start()
        {
            _gameOrchestrator.Initialize();
        }

        private void Update()
        {
            _gameOrchestrator.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _gameOrchestrator?.Dispose();
        }
    }
}