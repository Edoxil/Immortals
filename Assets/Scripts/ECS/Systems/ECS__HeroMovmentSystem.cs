using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Immortals
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ECS__HeroMovmentSystem))]
    public sealed class ECS__HeroMovmentSystem : UpdateSystem
    {
        [SerializeField] private ECS__AnimationRequestEvent _AnimationRequestEvent;
        private Filter _entities;

        private Vector3 _moveDirection;
        private AnimationData _animationData;

        public override void OnAwake()
        {
            _entities = World.Filter
                .With<ECS__HeroTag>()
                .With<ECS__SpeedComponent>()
                .With<ECS__DirectionComponent>()
                .With<ECS__CharacterControllerComponent>()
                .With<ECS__GravityComponent>();

            _animationData = new AnimationData();
            _animationData.ParameterType = ParameterType.Bool;
            _animationData.Key = AnimationID.RunID;
            _animationData.EntityID = _entities.First().ID;
        }

        public override void OnUpdate(float deltaTime)
        {
            ref ECS__GravityComponent gravity = ref _entities.First().GetComponent<ECS__GravityComponent>();
            ref ECS__SpeedComponent speed = ref _entities.First().GetComponent<ECS__SpeedComponent>();
            ref ECS__CharacterControllerComponent cc = ref _entities.First().GetComponent<ECS__CharacterControllerComponent>();
            ref ECS__DirectionComponent direction = ref _entities.First().GetComponent<ECS__DirectionComponent>();

            _moveDirection = direction.Direction;

            if (_moveDirection.magnitude > 0f)
                _animationData.BoolData = true;
            else
                _animationData.BoolData = false;

            _AnimationRequestEvent.Publish(_animationData);

            _moveDirection += gravity.Gravity;

            cc.CharacterController.Move((_moveDirection) * speed.Speed * deltaTime);
        }
    }
}