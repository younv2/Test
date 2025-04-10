using System.Collections.Generic;
using UnityEngine;

public class CollectedCard : MonoBehaviour
{
    [SerializeField] private GameObject front;
    [SerializeField] private GameObject back;
    [SerializeField] private GameObject locked;
    [SerializeField] private int index;
    private Animator animator;
    
    private HashSet<int> collectedCards;
    private string collectionKey;
    private bool isLocked = true;
    private bool isFlipping = false;
    private bool isBackShowing = false;

    private void Awake()
    {
        // Call this method to play Animation upon entering the CollectionScene
        animator = GetComponent<Animator>();
        collectionKey = GameManager.COLLECTION_KEY;
        LoadCardCollection();
        if(IsCollected())
        {
            locked.SetActive(false);
        }

        // If locked images are set active, cards cannot be clicked
        isLocked = locked.activeSelf;
    }

    // This method is called when the card is clicked
    private void OnMouseDown()
    {
        // If locked images are set active, cards cannot be clicked
        if (isLocked)
        {
            return;
        }
        else if (!isLocked)
        {
            FlipCard();
        }
    }

    // Call this method to flip the card
    public void FlipCard()
    {
        if (isFlipping) return;

        isFlipping = true;

        if (!isBackShowing)
        {
            animator.SetTrigger(Global.AnimId.FLIP_TO_BACK);
        }
        else
        {
            animator.SetTrigger(Global.AnimId.FLIP_TO_FRONT);
        }
    }

    // Call this in the middle of flip animation (e.g., frame 10)
    public void SwitchSides()
    {
        if (!isBackShowing)
        {
            front.SetActive(false);
            back.SetActive(true);
        }
        else
        {
            back.SetActive(false);
            front.SetActive(true);
        }
    }

    // Call this at the END of the animation (e.g., frame 20)
    public void FinishFlip()
    {
        isBackShowing = !isBackShowing;
        isFlipping = false;
    }

    
    // collection scene에서 카드 컬렉션 로드
    private void LoadCardCollection()
    {
        collectedCards = new HashSet<int>();
        string collectionData = PlayerPrefs.GetString(collectionKey, "");
        if (string.IsNullOrEmpty(collectionData))
            return;
        string[] cardIds = collectionData.Split(',');
        foreach (string cardId in cardIds)
        {
            if(int.TryParse(cardId, out int parsedId))
            {
                collectedCards.Add(parsedId);
            }
        }
    }
    private bool IsCollected()
    {
        return collectedCards.Contains(index);
    }
}
