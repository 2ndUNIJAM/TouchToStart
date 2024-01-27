using System;
using TouchToStart.Sound;
using UnityEngine;

namespace TouchToStart
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            StartButton.OnStartButtonClicked += () => AudioEvents.instance.PlaySound(SoundType.success);
            DeleteButton.OnDeleteButtonClicked += () => AudioEvents.instance.PlaySound(SoundType.fail);
            
        }
    }
}