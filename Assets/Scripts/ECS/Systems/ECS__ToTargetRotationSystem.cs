using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Immortals
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ECS__ToTargetRotationSystem))]
    public sealed class ECS__ToTargetRotationSystem : UpdateSystem
    {
        private Filter _entities;
        private Quaternion _lookDirection;

        public override void OnAwake()
        {
            _entities = World.Filter
                .With<ECS__RotationSpeedComponent>()
                .With<ECS__TargetComponent>()
                .With<ECS__TransformComponent>()
                .Without<ECS__IsMovingTag>();
            
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _entities)
            {
                ref ECS__RotationSpeedComponent speed = ref entity.GetComponent<ECS__RotationSpeedComponent>();
                ref ECS__TransformComponent transform = ref entity.GetComponent<ECS__TransformComponent>();
                ref ECS__TargetComponent target = ref entity.GetComponent<ECS__TargetComponent>();

                Vector3 direction = (target.Transform.position - transform.Transform.position).normalized;

                _lookDirection = Quaternion.LookRotation(direction, Vector3.up);
                _lookDirection.x = 0f;
                _lookDirection.z = 0f;
                transform.Transform.rotation = Quaternion.RotateTowards(transform.Transform.rotation, _lookDirection,
                    speed.RotationSpeed * deltaTime);
            }
        }
    }
}