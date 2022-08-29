using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

namespace Immortals
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ECS__GravitationSystem))]
    public sealed class ECS__GravitationSystem : UpdateSystem
    {
        private Filter _entities;

        public override void OnAwake()
        {
            _entities = World.Filter
                .With<ECS__GravityComponent>()
                .With<ECS__CharacterControllerComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _entities)
            {
                ref ECS__GravityComponent gravitation = ref entity.GetComponent<ECS__GravityComponent>();
                ref ECS__CharacterControllerComponent cc = ref entity.GetComponent<ECS__CharacterControllerComponent>();

                cc.CharacterController.Move(gravitation.Gravity.normalized * gravitation.Gravity.magnitude * deltaTime);
            }
        }
    }
}