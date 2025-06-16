using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("LevelManager Settings")]
    [Tooltip("Selected level id (no need to change in inspector)")]
    public int selectedLevel;
    [Tooltip("Gameobject of the star image that will be showed in the end of game (no need to change in inspector)")]
    [SerializeField] Image[] starObjects;
    [Tooltip("Total star that get on current selected level (no need to change in inspector)")]
    [SerializeField] int totalStar;

    [Tooltip("Level text that showed on main game scene (no need to change in inspector)")]
    public Text levelText;
    [Tooltip("Level text that showed on win panel (no need to change in inspector)")]
    public Text levelWinPanelText;
    [Tooltip("Level text that showed on lose panel (no need to change in inspector)")]
    public Text levelLosePanelText;

    [Tooltip("Button next level that will be showed in the end of game, if you win (no need to change in inspector)")]
    [SerializeField] GameObject nextLevelButton;
    [Tooltip("Based name of scene, must be same with that set on LevelButton")]
    public string levelSceneString;

    [Header("LevelManager UI")]
    [Tooltip("Sprite of active stars")]
    [SerializeField] Sprite starActive;
    [Tooltip("Sprite of not active stars")]
    [SerializeField] Sprite starNotActive;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitiateLevel();
    }

    void InitiateLevel()
    {
        if (!PlayerPrefs.HasKey("SelectedLevel"))
            PlayerPrefs.SetInt("SelectedLevel", 1);
        selectedLevel = PlayerPrefs.GetInt("SelectedLevel");
        if (PlayerPrefs.GetInt("MaxLevel") == selectedLevel)
            nextLevelButton.SetActive(false);

        levelText.text = "Level " + selectedLevel.ToString();
        levelWinPanelText.text = selectedLevel.ToString();
        levelLosePanelText.text = selectedLevel.ToString();
    }

    public void SetStars(int totalStar)
    {
        this.totalStar = totalStar;
        for (int i = 0; i < starObjects.Length; i++)
        {
            if (i < totalStar)
            {
                starObjects[i].sprite = starActive;
            }
            else
            {
                starObjects[i].sprite = starNotActive;
            }
        }
        PlayerPrefs.SetInt("Level" + selectedLevel, this.totalStar);
        
        int currentUnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel");
        if (selectedLevel == currentUnlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentUnlockedLevel + 1);
        }
    }
    public void OnNextLevelButtonClicked()
    {
        int tempId = PlayerPrefs.GetInt("SelectedLevel") + 1;
        PlayerPrefs.SetInt("SelectedLevel", tempId);
        JumpToOtherScene.quickGoToScene(levelSceneString + tempId);
    }
}