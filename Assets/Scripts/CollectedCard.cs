using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectedCard : MonoBehaviour
{
    public Animator animator;
    public GameObject Front;
    public GameObject Back;
    public GameObject Locked;
    public int index;
    
    private HashSet<int> collectedCards;
    private string collectionKey;
    private bool isLocked = true;
    private bool isFlipping = false;
    private bool isBackShowing = false;

    private void Awake()
    {
        // Call this method to play Animation upon entering the CollectionScene
        animator = GetComponent<Animator>();
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("IdleFront"))
        {
            animator.Play("IdleFront");
        }
        collectionKey = GameManager.COLLECTION_KEY;
        LoadCardCollection();
        if(IsCollected())
        {
            isLocked = false;
            Locked.SetActive(false);
        }
    }

    public void Update()
    {
        // If locked images are set active, cards cannot be clicked
        if (Locked.activeSelf)
        {
            isLocked = true;
        }
        else if (!Locked.activeSelf)
        {
            isLocked = false;
        }
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
            animator.SetTrigger("FlipToBack");
        }
        else
        {
            animator.SetTrigger("FlipToFront");
        }
    }

    // Call this in the middle of flip animation (e.g., frame 10)
    public void SwitchSides()
    {
        if (!isBackShowing)
        {
            Front.SetActive(false);
            Back.SetActive(true);
        }
        else
        {
            Back.SetActive(false);
            Front.SetActive(true);
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
        
        if (!string.IsNullOrEmpty(collectionData))
        {
            string[] cardIds = collectionData.Split(',');
            foreach (string cardId in cardIds)
            {
                if(int.TryParse(cardId, out int parsedId))
                {
                    collectedCards.Add(parsedId);
                }
            }
        }
    }

    private bool IsCollected()
    {
        return collectedCards.Contains(index);
    }
}
