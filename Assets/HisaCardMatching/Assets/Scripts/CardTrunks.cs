using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardTrunks : MonoBehaviour
{
    public static CardTrunks instance;

    [Header("Cards Trunk Settings")]
    [Tooltip("Image for each card type, total of card images is half from total card object in card manager")]
    public Sprite[] cardImages;
    [Tooltip("Open status for each card, total of card opened status is same as total card object in card manager")]
    public bool[] cardOpened;

    [Header("Current Status (*no need to change)")]
    [Tooltip("Total of all card in trunk (no need to change in inspector)")]
    public int totalCard;
    [Tooltip("Total of opened card (no need to change in inspector)")]
    public int openedCard;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        openedCard = 0;
        totalCard = cardOpened.Length;
        for (int i = 0; i < totalCard; i++)
            cardOpened[i] = false;
    }

    public bool isAllOpened()
    {
        bool state = true;

        for (int i = 0; i < cardOpened.Length; i++)
        {
            if (!cardOpened[i])
            {
                state = false;
            }
        }
        return state;
    }
}