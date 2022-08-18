using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

namespace Immortals
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public struct ECS__OffsetComponent : IComponent
    {
        public Vector3 Offset;
    }
}