using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour
{
    public int idx = 0;

    [SerializeField]private GameObject front;
    [SerializeField]private GameObject back;

    private Animator anim;
    private SpriteRenderer frontImage;

    public float previewTime = 2f;

    public void Setting(int num)
    {
        frontImage = front.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        StartCoroutine(PreviewCardAtStart());
        idx = num;
        frontImage.sprite = Resources.Load<Sprite>($"Images/ProfileCard/Card{num + 1}");
    }
    public void OpenCard()
    {
        anim.SetBool(Global.AnimId.IS_OPEN, true);
        front.SetActive(true);
        back.SetActive(false);
        if(GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;
        }
        else
        {
            GameManager.instance.secondCard = this;
            GameManager.instance.Matched();
        }
    }
    public void CloseCard()
    {
        Invoke(nameof(CloseCardInvoke), 0.5f);
    }

    public void CloseCardInvoke()
    {
        anim.SetBool(Global.AnimId.IS_OPEN, false);
        front.SetActive(false);
        back.SetActive(true);
    }

    public void DestroyCard()
    {
        Invoke(nameof(DestroyCardInvoke), 0.5f);
    }
    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    private IEnumerator PreviewCardAtStart()
    {
        // 카드 앞면 보여주기
        front.SetActive(true);
        back.SetActive(false);
        anim.SetBool(Global.AnimId.IS_OPEN, true);
        
        // 타이머 멈추기
        GameManager.instance.enabled = false;

        // 지정된 시간 동안 기다리기
        yield return new WaitForSeconds(previewTime);

        // 카드 뒤집기
        CloseCardInvoke();
        
        // 타이머 다시 시작
        GameManager.instance.enabled = true;
    }
}
