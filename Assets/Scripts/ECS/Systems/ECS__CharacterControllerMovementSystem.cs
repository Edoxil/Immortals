using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Immortals
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ECS__CharacterControllerMovementSystem))]
    public sealed class ECS__CharacterControllerMovementSystem : UpdateSystem
    {
        [SerializeField] private ECS__AnimationRequestEvent _AnimationRequestEvent;
        private Filter _entities;

        private Vector3 _moveDirection;
        private AnimationData _animationData;

        public override void OnAwake()
        {
            _entities = World.Filter
                .With<ECS__SpeedComponent>()
                .With<ECS__DirectionComponent>()
                .With<ECS__CharacterControllerComponent>();

            _animationData = new AnimationData();
            _animationData.ParameterType = ParameterType.Bool;
            _animationData.Key = AnimationID.RunID;
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _entities)
            {
                ref ECS__SpeedComponent speed = ref entity.GetComponent<ECS__SpeedComponent>();
                ref ECS__CharacterControllerComponent cc = ref entity.GetComponent<ECS__CharacterControllerComponent>();
                ref ECS__DirectionComponent direction = ref entity.GetComponent<ECS__DirectionComponent>();

                _moveDirection = direction.Direction;

                if (_moveDirection.magnitude > 0f)
                {
                    if (entity.Has<ECS__IsMovingTag>() == false)
                        entity.AddComponent<ECS__IsMovingTag>();

                    _animationData.BoolData = true;
                }
                else
                {
                    if (entity.Has<ECS__IsMovingTag>())
                        entity.RemoveComponent<ECS__IsMovingTag>();

                    _animationData.BoolData = false;
                }


                _animationData.EntityID = entity.ID;
                _AnimationRequestEvent.Publish(_animationData);


                cc.CharacterController.Move((_moveDirection) * speed.Speed * deltaTime);
            }
        }
    }
}