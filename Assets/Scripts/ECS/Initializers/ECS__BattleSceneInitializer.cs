using Morpeh;
using System.Threading.Tasks;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Immortals
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(ECS__BattleSceneInitializer))]
    public sealed class ECS__BattleSceneInitializer : Initializer
    {
        [SerializeField] private SpawnService _spawnService;
        [Space(10)]
        [SerializeField] private ECS__HeroTagProvider _heroPrefab;
        [SerializeField] private GameObject _levelPrefab;

        private GameObject _level;
        private ECS__HeroTagProvider _hero;

        public override async void OnAwake()
        {
            await SpawnLevel();
            await SpawnHero();
        }

        public override void Dispose()
        {
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