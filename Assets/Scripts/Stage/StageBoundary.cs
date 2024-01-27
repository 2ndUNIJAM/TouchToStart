using System;
using TouchToStart.Sound;
using UnityEngine;

namespace TouchToStart
{
    public class StageBoundary : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out MetaMouse mouse))
            {
                mouse.MouseReset();
                if (AudioEvents.instance)
                    AudioEvents.instance.PlaySound(SoundType.edgedenied);
            }
        }
    }
}