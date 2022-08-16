using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Immortals
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ECS_JoystickInputSystem))]
    public sealed class ECS_JoystickInputSystem : UpdateSystem
    {
        [SerializeField] private ECS_JoystickInputEvent _joystickInputEvent;
        private Vector2 _input;

        public override void OnAwake()
        {
        }

        public override void OnUpdate(float deltaTime)
        {
            _input = Vector2.zero;

            _input.x = SimpleInput.GetAxis("Horizontal");
            _input.y = SimpleInput.GetAxis("Vertical");

            _joystickInputEvent.Publish(_input);
        }
    }
}