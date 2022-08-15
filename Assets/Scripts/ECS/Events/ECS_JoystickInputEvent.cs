using UnityEngine;
using Morpeh.Globals;
using Unity.IL2CPP.CompilerServices;

namespace Immortals
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Globals/Custom/" + nameof(ECS_JoystickInputEvent))]
    public sealed class ECS_JoystickInputEvent : BaseGlobalEvent<Vector2>
    {
        public override string LastToString()
        {
            return string.Empty;
        }
    }
}