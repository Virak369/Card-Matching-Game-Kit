using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    [Header("Cards Manager Settings")]
    [Tooltip("The main object in the game, the number of Card Objects must always an even number")]
    public Cards[] cardObject;
    [Tooltip("Delay time before card closing")]
    public float delayClosingCard;

    [Header("Win Scene Settings")]
    [Tooltip("The panel scene that will be shown when the player successfully opens all cards")]
    public GameObject winScene;
    [Tooltip("Delay time before win scene shown")]
    public float delayWinSceneShowing;

    [Header("Audio Settings")]
    [Tooltip("SFX when first card opened")]
    public AudioSource sfxFirstOpen;
    [Tooltip("SFX when second card opened")]
    public AudioSource sfxSecondOpen;

    [Header("Current Status (*no need to change)")]
    [Tooltip("Total current card opened, the value will be 0, 1 or 2 (no need to change in inspector)")]
    public int openedCard;
    [Tooltip("Id from first card opened (no need to change in inspector)")]
    public int idFirstCard;
    [Tooltip("Id from second card opened (no need to change in inspector)")]
    public int idSecondCard;
    [Tooltip("Status from process of card closing (no need to change in inspector)")]
    public bool inCardClosingProcess;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        closeAllSleveCard();
        openedCard = 0;
        getRandomCardID();
        inCardClosingProcess = false;

        for (int i = 0; i < cardObject.Length; i++)
        {
            cardObject[i].coverId = i;
        }

        if (winScene != null)
            winScene.SetActive(false);
    }

    private void LateUpdate()
    {
        if (openedCard == 2)
        {
            inCardClosingProcess = true;
            Invoke("closeOpenedSleveCard", delayClosingCard);
        }
    }

    void getRandomCardID()
    {
        for (int i = 0; i < cardObject.Length; i++)
        {
            cardObject[i].cardId = Random.Range(0, CardTrunks.instance.cardImages.Length);
            cardObject[i].transform.GetChild(0).GetComponent<Image>().sprite =
                CardTrunks.instance.cardImages[cardObject[i].cardId];

            int totalSameCardId = 0;
            for (int j = 0; j < i; j++)
            {
                if (cardObject[i].cardId == cardObject[j].cardId)
                    totalSameCardId += 1;

                if (totalSameCardId >= 2)
                    i -= 1;
            }
        }
    }

    public void OpenCard(int cardId)
    {
        if (!inCardClosingProcess)
        {
            if (openedCard == 0)
                idFirstCard = cardId;
            else if (openedCard == 1)
                idSecondCard = cardId;

            //cardObject[cardId].transform.GetChild(1).gameObject.SetActive(false);
            cardObject[cardId].cardAnimator.SetBool("isOpen", true);
            openedCard += 1;
            if (sfxFirstOpen != null)
                sfxFirstOpen.PlayOneShot(sfxFirstOpen.clip);
        }
    }

    void closeOpenedSleveCard()
    {
        if (openedCard == 2)
        {
            if (cardObject[idFirstCard].cardId == cardObject[idSecondCard].cardId)
            {
                Debug.Log("MATCH, 1st card (" + idFirstCard + ") and 2nd card ("
                    + idSecondCard + ") is matching");

                if (sfxSecondOpen != null)
                    sfxSecondOpen.PlayOneShot(sfxSecondOpen.clip);

                cardObject[idFirstCard].isOpened = true;
                cardObject[idSecondCard].isOpened = true;
                CardTrunks.instance.cardOpened[idFirstCard] = true;
                CardTrunks.instance.cardOpened[idSecondCard] = true;

                CardTrunks.instance.openedCard += 2;

                if (CardTrunks.instance.isAllOpened())
                {
                    Invoke("WinSceneOpened", delayWinSceneShowing);
                }
            }
            else
            {
                Debug.Log("1st card (" + idFirstCard + ") and 2nd card ("
                    + idSecondCard + ") is not match");

                //cardObject[idFirstCard].transform.GetChild(1).gameObject.SetActive(true);
                //cardObject[idSecondCard].transform.GetChild(1).gameObject.SetActive(true);
                cardObject[idFirstCard].cardAnimator.SetBool("isOpen", false);
                cardObject[idSecondCard].cardAnimator.SetBool("isOpen", false);
            }

            idFirstCard = -1;
            idSecondCard = -1;
            openedCard = 0;
            inCardClosingProcess = false;
        }
    }

    void closeAllSleveCard()
    {
        inCardClosingProcess = true;
        for (int i = 0; i < cardObject.Length; i++)
        {
            //cardObject[i].transform.GetChild(1).gameObject.SetActive(true);
            //cardObject[i].cardAnimator.SetBool("isOpen", false);
            cardObject[i].isOpened = false;
        }
        for (int i = 0; i < CardTrunks.instance.cardOpened.Length; i++)
            CardTrunks.instance.cardOpened[i] = false;
    }

    public void WinSceneOpened()
    {
        if (winScene != null)
            winScene.SetActive(true);

        if (TimeManager.instance != null && LevelManager.instance != null)
        {
            int tempStar = (int)((TimeManager.instance.currentTime - 1) / TimeManager.instance.maxTime * 3) + 1;
            LevelManager.instance.SetStars(tempStar);
        }
    }
}