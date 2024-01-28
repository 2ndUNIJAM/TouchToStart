using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchToStart.Utility;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Runtime.InteropServices;

public class AchievementUI : Singleton<AchievementUI>
{
    [SerializeField]
    public List<Sprite> sprites;
    public List<string> achieveTitles;
    public List<string> achieveDescriptions;
    public Sprite nullsprite;



    public bool useIngameAchievements; // StovePCSDK의 업적이 잘 안되면 true (인게임에서 업적 보이게 하기), SDK Stove 업적이 성공하면 false.
    

    private int MAX_LEVEL_CLEARED, NUM_PRESS_START, NUM_PRESS_DEL, NUM_PRESS_LAST;

    private Image icon;
    private TextMeshProUGUI titleText, descriptionText;

    void Start() {
        MAX_LEVEL_CLEARED = PlayerPrefs.GetInt("MAX_LEVEL_CLEARED");
        NUM_PRESS_START = PlayerPrefs.GetInt("NUM_PRESS_START");
        NUM_PRESS_DEL = PlayerPrefs.GetInt("NUM_PRESS_DEL");
        NUM_PRESS_LAST = PlayerPrefs.GetInt("NUM_PRESS_LAST");

        icon = this.transform.Find("Icon").GetComponent<Image>();
        titleText = this.transform.Find("title").GetComponent<TextMeshProUGUI>();
        descriptionText = this.transform.Find("description").GetComponent<TextMeshProUGUI>();
    }

    public void AchieveVarUpdate() {

        int new_mlc = PlayerPrefs.GetInt("MAX_LEVEL_CLEARED"), new_nps = PlayerPrefs.GetInt("NUM_PRESS_START"),
            new_npd = PlayerPrefs.GetInt("NUM_PRESS_DEL"), new_npl = PlayerPrefs.GetInt("NUM_PRESS_LAST");
        int a = AchieveNumber(new_mlc, new_nps, new_npd, new_npl);
        MAX_LEVEL_CLEARED = new_mlc;
        NUM_PRESS_START = new_nps;
        NUM_PRESS_DEL = new_npd;
        NUM_PRESS_LAST = new_npl;
        if (a != -1) {
            AchieveShow(a);
        }
    }

    private int AchieveNumber(int new_mlc, int new_nps, int new_npd, int new_npl) {
        Debug.Log(new_nps.ToString());
        int achieveNumber = -1;
        if (new_mlc != MAX_LEVEL_CLEARED) {
            switch (new_mlc) {
                case 3:
                    achieveNumber = 1;
                    break;
                case 6:
                    achieveNumber = 2;
                    break;
                case 11:
                    achieveNumber = 3;
                    break;
                case 13:
                    achieveNumber = 4;
                    break;
                case 15:
                    achieveNumber = 5;
                    break;
                case 17:
                    achieveNumber = 6;
                    break;
                case 20:
                    achieveNumber = 7;
                    break;
                case 23:
                    achieveNumber = 8;
                    break;
                case 25:
                    achieveNumber = 9;
                    break;
                case 28:
                    achieveNumber = 10;
                    break;
                case 30:
                    achieveNumber = 11;
                    break;
            }
        } else if (new_nps != NUM_PRESS_START) {
            if (new_nps == 1) {
                achieveNumber = 0;
            }
        } else if (new_npd != NUM_PRESS_DEL) {
            if (new_npd == 1) {
                achieveNumber = 13;
            } else if (new_npd >= 20) {
                achieveNumber = 14;
            }
        } else if (new_npl != NUM_PRESS_LAST) {
            if (new_npl == 0) {
                achieveNumber = 12;
            }
        } else {
            // do nothing
        }
        return achieveNumber;
    }

    private void AchieveShow(int a)
    {
        Sprite img;
        string title, description;
        if (a < sprites.Count) {
            img = sprites[a];
            title = achieveTitles[a];
            description = achieveDescriptions[a];
        } else {
            img = nullsprite;
            title = "Error";
            description = "Error";
        }
        icon.sprite = img;
        titleText.text = title;
        descriptionText.text = description;

        float prevY = transform.localPosition.y;
        
        // Animate
        transform.DOLocalMoveY(prevY - 240.0f, 0.5f, false).onComplete += (
            () => transform.DOLocalMoveY(prevY - 240.0f, 1.5f, false).onComplete += (
                () => transform.DOLocalMoveY(prevY, 0.5f, false)
            ));
    }
}
