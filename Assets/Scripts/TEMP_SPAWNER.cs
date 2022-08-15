using Morpeh;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Immortals
{
    public class TEMP_SPAWNER : MonoBehaviour
    {
        [SerializeField] private Installer _installer;
        [SerializeField] private ECS__HeroTagProvider _hero;

        [Button]
        private void SpawnHero()
        {
            var hero = Instantiate(_hero, transform);
        }

        [Button]
        private void RunWorld()
        {
            var inst = Instantiate(_installer, transform);
        }
    }
}