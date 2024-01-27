using DG.Tweening;
using UnityEngine;

namespace TouchToStart
{
    public static class CameraExtensions
    {
        public static void DoRect(this Camera cam, Rect rect, float duration)
        {
            DOTween.To(() => cam.rect, (r) => cam.rect = r, rect, duration);
        }
    }
}