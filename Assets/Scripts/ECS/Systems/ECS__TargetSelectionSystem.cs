using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Immortals
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ECS__TargetSelectionSystem))]
    public sealed class ECS__TargetSelectionSystem : UpdateSystem
    {
        private Filter _enemies;
        private Filter _hero;
        public override void OnAwake()
        {
            _enemies = World.Filter
                .With<ECS__EnemyTag>()
                .With<ECS__TransformComponent>();

            _hero = World.Filter
                .With<ECS__HeroTag>()
                .With<ECS__TransformComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref ECS__TransformComponent heroTransform = ref _hero.First().GetComponent<ECS__TransformComponent>();

            foreach (var enemy in _enemies)
            {
                ref Transform enemyTransform = ref enemy.GetComponent<ECS__TransformComponent>().Transform;

                if (_hero.First().Has<ECS__TargetComponent>()==false)
                {
                    _hero.First().AddComponent<ECS__TargetComponent>();
                    _hero.First().SetComponent(new ECS__TargetComponent() { Transform = enemyTransform });
                }
                else
                {
                    ref ECS__TargetComponent heroTarget = ref _hero.First().GetComponent<ECS__TargetComponent>();

                    if (Vector3.Distance(enemyTransform.position, heroTransform.Transform.position) <
                        Vector3.Distance(heroTransform.Transform.position, heroTarget.Transform.position))
                        heroTarget.Transform = enemyTransform.transform;
                }

            }
        }
    }
}