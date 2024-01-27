using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ESCToReset : MonoBehaviour
{
    public Image circle;
    public Image skipText;

    public float SkipTime;
    private float pressedTime;

    void Start()
    {
        pressedTime = 0;
    }
    void Update()
    {
        // ESC키가 눌리는 중이면 눌린 시간을 증가시킴
        if (Input.GetKey(KeyCode.Escape))
        {
            pressedTime += Time.unscaledDeltaTime;
            if (pressedTime > SkipTime)
            {
                SceneReset();
            }
        } else
        {
            pressedTime = 0;
        }
        circle.fillAmount = pressedTime / SkipTime;
        // skipText.fillAmount = pressedTime * 2 / SkipTime + 0.39f;

        // ESC키가 때지면 눌린시간을 리셋

        // ESC키가 눌린시간이 SkipTime보다 크면 Skip을 실행
    }

    void SceneReset()
    {
        // 현재 씬을 그대로 다시 로드
        MetaMouse.MouseList.Clear();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        pressedTime = 0;
        Time.timeScale = 1;
        // PlayerPrefs로 PlayingStage 키에 0 값을 저장하기
        PlayerPrefs.SetInt("PlayingStage", 0);
    }
}
