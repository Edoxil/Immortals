using UnityEngine;

namespace Immortals
{
    public class SpawnService : MonoBehaviour
    {
        public T Spawn<T>(T prefab, Transform parent) where T : Object
        {
            return Instantiate(prefab, parent);
        }
    }
}