using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Immortals
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ECS__CameraMovingSystem))]
    public sealed class ECS__CameraMovingSystem : LateUpdateSystem
    {
        private Filter _camera;
        private Filter _target;

        public override void OnAwake()
        {
            _camera = World.Filter
                .With<ECS__CameraTag>()
                .With<ECS__TransformComponent>()
                .With<ECS__SpeedComponent>()
                .With<ECS__OffsetComponent>();

            _target = World.Filter
                .With<ECS_CameraTargetTag>()
                .With<ECS__TransformComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref ECS__TransformComponent target = ref _target.First().GetComponent<ECS__TransformComponent>();

            if (target.Transform == null)
                return;

            ref ECS__TransformComponent cameraTransform = ref _camera.First().GetComponent<ECS__TransformComponent>();
            ref ECS__SpeedComponent lerpSpeed = ref _camera.First().GetComponent<ECS__SpeedComponent>();
            ref ECS__OffsetComponent offset = ref _camera.First().GetComponent<ECS__OffsetComponent>();

            cameraTransform.Transform.position =
                Vector3.Lerp(cameraTransform.Transform.position, target.Transform.position + offset.Offset,
                lerpSpeed.Speed * deltaTime);
        }
    }
}