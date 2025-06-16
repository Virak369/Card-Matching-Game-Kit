using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [Header("LevelButton Settings")]
    [Tooltip("Button id that will be set automatically based on level id (no need to change in inspector)")]
    [HideInInspector] public int id;
    [Tooltip("Total star that already got on this level id (no need to change in inspector)")]
    [HideInInspector] public int totalStar;
    [Tooltip("Based name of scene, when levelButton clicked it will going to scene : levelSceneString + id")]
    [SerializeField] string levelSceneString;

    public void OnLevelButtonClicked()
    {
        if(id <= PlayerPrefs.GetInt("UnlockedLevel"))
        {
            PlayerPrefs.SetInt("SelectedLevel", id);
            JumpToOtherScene.quickGoToScene(levelSceneString + id);
        }
    }
}
