using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Immortals
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ECS__ReadInputSystem))]
    public sealed class ECS__ReadInputSystem : UpdateSystem
    {
        [SerializeField] private ECS_JoystickInputEvent _joystickInputEvent;
        private Filter _entities;
        private Vector2 _input;

        public override void OnAwake()
        {
            _entities = World.Filter
                .With<ECS__HeroTag>()
                .With<ECS__DirectionComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            _input = _joystickInputEvent.IsPublished ? _joystickInputEvent.BatchedChanges.Pop() : Vector2.zero;

            ref ECS__DirectionComponent direction = ref _entities.First().GetComponent<ECS__DirectionComponent>();

            direction.Direction = new Vector3(_input.x, 0f, _input.y);

        }
    }
}