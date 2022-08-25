using UnityEngine;

namespace Immortals
{
    [CreateAssetMenu(fileName = nameof(SpawnService), menuName = "Services/"+ nameof(SpawnService), order = 1)]
    public class SpawnService : ScriptableObject
    {
        public T Spawn<T>(T prefab, Transform parent) where T : Object
        {
            return Instantiate(prefab, parent);
        }
    }
}