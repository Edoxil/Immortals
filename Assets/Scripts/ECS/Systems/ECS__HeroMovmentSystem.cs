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
        [SerializeField] ECS_JoystickInputEvent _joystickInputEvent;

        public override void OnAwake()
        {
        }

        public override void OnUpdate(float deltaTime)
        {
            Debug.Log(_joystickInputEvent.BatchedChanges.Pop());

        }
    }
}