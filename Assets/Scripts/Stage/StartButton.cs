using System;
using System.Collections;
using UnityEngine;

namespace TouchToStart
{
    public class StartButton : MonoBehaviour
    {
        public static event Action OnStartButtonClicked;
        public event Action OnButtonClicked;

        public float Delay;

        [SerializeField]
        private Animator _animator;

        private bool _triggered;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_triggered) return;
            
            if (other.TryGetComponent(out MetaMouse mouse))
            {
                _animator.SetBool("Click", true);
                StartCoroutine(DestroyAfter(Delay));
                GetComponent<Collider2D>().enabled = false;
                _triggered = true;
                OnStartButtonClicked?.Invoke();
                OnButtonClicked?.Invoke();
            }
        }

        IEnumerator DestroyAfter(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}