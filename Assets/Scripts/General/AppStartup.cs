using DG.Tweening;
using UnityEngine;

namespace Immortals
{
    public class AppStartup : MonoBehaviour
    {
        private void Start()
        {
            DOTween.Init();
            Application.targetFrameRate = 60;
        }
    }
}