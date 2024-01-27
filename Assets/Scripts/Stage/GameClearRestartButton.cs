using UnityEngine;
using UnityEngine.SceneManagement;

namespace TouchToStart
{
    public class GameClearRestartButton : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out MetaMouse mouse))
            {
                StovePCSDKManager.instance.RecordLastStart(1);
                MetaMouse.MouseList.Clear();
                SceneManager.LoadScene(0);
            }
        }
    }
}