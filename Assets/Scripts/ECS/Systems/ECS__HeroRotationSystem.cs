using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Immortals
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ECS__HeroRotationSystem))]
    public sealed class ECS__HeroRotationSystem : UpdateSystem
    {
        private Filter _entities;
        private Filter _enemies;

        private Quaternion _lookDirection;

        public override void OnAwake()
        {
            _entities = World.Filter
                .With<ECS__HeroTag>()
                .With<ECS__RotationSpeedComponent>()
                .With<ECS__DirectionComponent>()
                .With<ECS__TransformComponent>();

            _enemies = World.Filter
                .With<ECS__EnemyTag>()
                .With<ECS__TransformComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref ECS__DirectionComponent direction = ref _entities.First().GetComponent<ECS__DirectionComponent>();

            if (direction.Direction != Vector3.zero)
                RotateToMoveDiratction(direction.Direction);
            else
                RotateToAttackTarget();
        }

        private void RotateToMoveDiratction(Vector3 direction)
        {
            ref ECS__RotationSpeedComponent speed = ref _entities.First().GetComponent<ECS__RotationSpeedComponent>();
            ref ECS__TransformComponent transform = ref _entities.First().GetComponent<ECS__TransformComponent>();

            _lookDirection = Quaternion.LookRotation(direction, Vector3.up);
            _lookDirection.x = 0f;
            _lookDirection.z = 0f;
            transform.Transform.rotation = Quaternion.RotateTowards(transform.Transform.rotation, _lookDirection,
                speed.RotationSpeed * Time.deltaTime);
        }

        private void RotateToAttackTarget()
        {
            ref ECS__RotationSpeedComponent speed = ref _entities.First().GetComponent<ECS__RotationSpeedComponent>();
            ref ECS__TransformComponent transform = ref _entities.First().GetComponent<ECS__TransformComponent>();

            ref ECS__TransformComponent enemyTransform = ref _enemies.First().GetComponent<ECS__TransformComponent>();

            Vector3 direction = (enemyTransform.Transform.position - transform.Transform.position).normalized;


            _lookDirection = Quaternion.LookRotation(direction, Vector3.up);
            _lookDirection.x = 0f;
            _lookDirection.z = 0f;
            transform.Transform.rotation = Quaternion.RotateTowards(transform.Transform.rotation, _lookDirection,
                speed.RotationSpeed * Time.deltaTime);
        }

    }
}