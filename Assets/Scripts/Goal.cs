using System;
using UnityEngine;

namespace TouchToStart
{
    public class Goal : MonoBehaviour
    {
        public event Action<int> OnStageClear;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            // if (other.TryGetComponent(out MetaMouse mouse))
            // {
                // OnStageClear?.Invoke();                         
            // }
        }
    }
}