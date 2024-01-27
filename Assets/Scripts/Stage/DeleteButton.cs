using System;
using System.Collections;
using TouchToStart.Sound;
using UnityEngine;

namespace TouchToStart
{
    public class DeleteButton : MonoBehaviour
    {
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

                if (PlayerPrefs.HasKey("numDelPress"))
                {
                    int s = PlayerPrefs.GetInt("numDelPress");
                    PlayerPrefs.SetInt("numDelPress", ++s);
                }
                else
                {
                    PlayerPrefs.SetInt("numDelPress", 1);
                }
                StovePCSDKManager.instance.RecordPressDel(PlayerPrefs.GetInt("numDelPress"));

                AudioEvents.instance.PlaySound(SoundType.fail);
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