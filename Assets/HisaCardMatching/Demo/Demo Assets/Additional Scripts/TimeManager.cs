using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [Header("Time Settings")]
    [Tooltip("Text of Current Time")]
    public Text textCurrentTime;
    [Tooltip("Maximum play time")]
    public float maxTime;
    [Tooltip("Game object of lose panel")]
    public GameObject losePanel;
    [HideInInspector]
    public float currentTime;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        losePanel.SetActive(false);
        currentTime = maxTime;
    }

    void Update()
    {
        if (currentTime <= 0)
        {
            if (LevelManager.instance != null)
                LevelManager.instance.SetStars(0);
            losePanel.SetActive(true);
        }
        else
        {
            if (!CardTrunks.instance.isAllOpened())
            {
                currentTime -= Time.deltaTime;
                textCurrentTime.text = (int)currentTime + "";
            }
        }
    }
}