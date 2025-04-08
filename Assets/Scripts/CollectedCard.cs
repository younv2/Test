using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedCard : MonoBehaviour
{
    public Animator animator;
    public GameObject Front;
    public GameObject Back;

    private bool isFlipping = false;
    private bool isBackShowing = false;

    private void OnMouseDown()
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

    // Call this at the END of the animation
    public void FinishFlip()
    {
        isBackShowing = !isBackShowing;
        isFlipping = false;
    }
}
