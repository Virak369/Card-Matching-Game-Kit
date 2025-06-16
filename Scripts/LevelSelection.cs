using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [Header("LevelSelection Settings")]
    [Tooltip("Level buttons gameobject")]
    [SerializeField] GameObject[] levelButtons;
    [Tooltip("Text that contain total star that already collected by user (no need to change in inspector)")]
    [SerializeField] Text totalCollectedStarText;
    [Tooltip("Gameobject panel that will be showed when all level clear (no need to change in inspector)")]
    [SerializeField] GameObject gameClearPanel;
    [Tooltip("Maximum stars that can be get on one level")]
    const int starsPerLevel = 3;

    [Header("LevelSelection UI Settings")]
    [Tooltip("Sprite of button that already get perfect stars")]
    [SerializeField] Sprite panelCompleted;
    [Tooltip("Sprite of background button that already get stars but still not perfect")]
    [SerializeField] Sprite panelActive;
    [Tooltip("Sprite of background button that still not get any stars")]
    [SerializeField] Sprite panelNotActive;
    [Tooltip("Sprite of active stars")]
    [SerializeField] Sprite starActive;
    [Tooltip("Sprite of not active stars")]
    [SerializeField] Sprite starNotActive;

    void Start()
    {
        InitiateLevelData();
    }

    void InitiateLevelData()
    {
        if (!PlayerPrefs.HasKey("UnlockedLevel"))
        {
            PlayerPrefs.SetInt("UnlockedLevel", 1);
        }

        int currentUnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel");
        int tempTotalCollectedStars = 0;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].transform.Find("LevelText").GetComponent<Text>().text = (i + 1).ToString();
            levelButtons[i].GetComponent<LevelButton>().id = i + 1;

            int tempStar = PlayerPrefs.GetInt("Level" + (i + 1));
            tempTotalCollectedStars += tempStar;
            levelButtons[i].GetComponent<LevelButton>().totalStar = tempStar;

            if (i < currentUnlockedLevel)
            {
                if (tempStar == 0)
                    levelButtons[i].GetComponent<Image>().sprite = panelActive;
                else
                    levelButtons[i].GetComponent<Image>().sprite = panelCompleted;
            }
            else
            {
                levelButtons[i].GetComponent<Image>().sprite = panelNotActive;
            }

            for (int j = 0; j < starsPerLevel; j++)
            {
                if (levelButtons[i].transform.Find("Stars").Find("Star" + (j + 1)) != null)
                {
                    if (j < tempStar)
                        levelButtons[i].transform.Find("Stars").Find("Star" + (j + 1)).GetComponent<Image>().sprite = starActive;
                    else
                        levelButtons[i].transform.Find("Stars").Find("Star" + (j + 1)).GetComponent<Image>().sprite = starNotActive;
                }
            }
        }

        totalCollectedStarText.text = tempTotalCollectedStars.ToString();
        if (tempTotalCollectedStars >= levelButtons.Length * starsPerLevel)
            gameClearPanel.SetActive(true);
        else
            gameClearPanel.SetActive(false);

        PlayerPrefs.SetInt("MaxLevel", levelButtons.Length);
    }
}
