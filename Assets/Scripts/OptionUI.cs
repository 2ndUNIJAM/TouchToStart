using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TouchToStart
{
    public class OptionUI : MonoBehaviour
    {
        public GameObject UIObject;
        public GameObject ESCToReset;
        public TextMeshProUGUI StageText;

        public Slider VolumeSlider;
        public Slider MouseSpeedSlider;

        private bool isOpened => UIObject.activeSelf;

        private void Start()
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
            float savedMouseSpeed = PlayerPrefs.GetFloat("MouseSpeed", 25);
            OnVolumeChange(savedVolume);
            OnMouseSpeedChange(savedMouseSpeed);
            VolumeSlider.value = savedVolume;
            MouseSpeedSlider.value = savedMouseSpeed;
            VolumeSlider.onValueChanged.AddListener(OnVolumeChange);
            MouseSpeedSlider.onValueChanged.AddListener(OnMouseSpeedChange);

            Cursor.visible = false;         //마우스 포인터 안 보이게 함
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !isOpened)
            {
                UIObject.SetActive(true);
                ESCToReset.SetActive(true);
                StageText.text = $"스테이지 {Stage.instance.CurrentStage}";
                Time.timeScale = 0;

                FollowMouse.instance.enabled = false;
            }
        }

        public void Close()
        {
            UIObject.SetActive(false);
            ESCToReset.SetActive(false);
            Time.timeScale = 1;

            FollowMouse.instance.enabled = true;
        }

        public void ExitGame()
        {
            // StovePCSDKManager.instance.ButtonUninitialize_Click();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void OnVolumeChange(float value)
        {
            AudioListener.volume = value;
            PlayerPrefs.SetFloat("Volume", value);
        }

        public void OnMouseSpeedChange(float value)
        {
            FollowMouse.instance.Speed = value;
            PlayerPrefs.SetFloat("MouseSpeed", value);
        }
    }
}