using System;
using System.Collections;
using UnityEngine;

namespace TouchToStart
{
    public class StartButton : MonoBehaviour
    {
        public static event Action OnStartButtonClicked;

        public float Delay;

        [SerializeField]
        private Animator _animator;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out MetaMouse mouse))
            {
                _animator.SetBool("Click", true);
                StartCoroutine(Wait(Delay, () => OnStartButtonClicked?.Invoke()));
                GetComponent<Collider2D>().enabled = false;
            }
        }

        IEnumerator Wait(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
            Destroy(gameObject);
        }
    }
}