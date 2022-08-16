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

        private Quaternion _lookDirection;

        public override void OnAwake()
        {
            _entities = World.Filter
                .With<ECS__HeroTag>()
                .With<ECS__RotationSpeedComponent>()
                .With<ECS__DirectionComponent>()
                .With<ECS__TransformComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref ECS__RotationSpeedComponent speed = ref _entities.First().GetComponent<ECS__RotationSpeedComponent>();
            ref ECS__TransformComponent transform = ref _entities.First().GetComponent<ECS__TransformComponent>();
            ref ECS__DirectionComponent direction = ref _entities.First().GetComponent<ECS__DirectionComponent>();

            if (direction.Direction != Vector3.zero)
            {
                _lookDirection = Quaternion.LookRotation(direction.Direction, Vector3.up);
                _lookDirection.x = 0f;
                _lookDirection.z = 0f;
                transform.Transform.rotation = Quaternion.RotateTowards(transform.Transform.rotation, _lookDirection,
                    speed.RotationSpeed * Time.deltaTime);
            }
        }
    }
}