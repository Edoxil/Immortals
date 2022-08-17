using Morpeh;
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Immortals
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ECS__AnimationSystem))]
    public sealed class ECS__AnimationSystem : UpdateSystem
    {
        [SerializeField] private ECS__AnimationRequestEvent _AnimationRequestEvent;

        private Filter _entities;

        public override void OnAwake()
        {
            _entities = World.Filter
                .With<ECS__AnimatorComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            if (_AnimationRequestEvent.IsPublished)
            {
                while (_AnimationRequestEvent.BatchedChanges.Count > 0)
                {
                    AnimationData data = _AnimationRequestEvent.BatchedChanges.Pop();
                 
                    ref ECS__AnimatorComponent animator = ref World.GetEntity(in data.EntityID).GetComponent<ECS__AnimatorComponent>();
                    ApplayAnimation(data, ref animator);
                }
            }

        }

        private void ApplayAnimation(AnimationData data, ref ECS__AnimatorComponent animator)
        {
            switch (data.ParameterType)
            {
                case ParameterType.Float:
                    animator.Animator.SetFloat(data.Key, data.FloatData);
                    break;
                case ParameterType.Bool:
                    animator.Animator.SetBool(data.Key, data.BoolData);
                    break;
                case ParameterType.Int:
                    animator.Animator.SetInteger(data.Key, data.IntData);
                    break;
                case ParameterType.Trigger:
                    if (data.ResetTrigger)
                        animator.Animator.ResetTrigger(data.Key);
                    else
                        animator.Animator.SetTrigger(data.Key);
                    break;
                case ParameterType.None:
                    throw new ArgumentException($"Invalid {nameof(data.ParameterType)}: {data.ParameterType} ");
                default:
                    throw new ArgumentException($"Invalid {nameof(data.ParameterType)}: {data.ParameterType} ");
            }
        }

    }
}