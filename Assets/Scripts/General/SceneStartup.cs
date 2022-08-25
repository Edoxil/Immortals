using Morpeh;
using System.Threading.Tasks;
using UnityEngine;

namespace Immortals
{
    public class SceneStartup : MonoBehaviour
    {
        [SerializeField] private SpawnService _spawnService;
        [Space(10)]
        [SerializeField] private ECS__HeroTagProvider _heroPrefab;
        [SerializeField] private GameObject _levelPrefab;
        [SerializeField] private Installer _installerPrefab;

        private GameObject _level;
        private ECS__HeroTagProvider _hero;
        private Installer _installer;

        private async void Awake()
        {
            await SpawnLevel();
            await SpawnHero();
            await SpawnEcsInstaller();
        }

        private async Task SpawnEcsInstaller()
        {
            // Async loading prefab from adressables
            _installer = _spawnService.Spawn(_installerPrefab, transform);
            await Task.CompletedTask;
        }

        private async Task SpawnLevel()
        {
            // Async loading prefab from adressables
            _level = _spawnService.Spawn(_levelPrefab, null);
            await Task.CompletedTask;
        }

        private async Task SpawnHero()
        {
            // Async loading prefab from adressables
            _hero = _spawnService.Spawn(_heroPrefab, _level.transform);
            await Task.CompletedTask;
        }

    }
}