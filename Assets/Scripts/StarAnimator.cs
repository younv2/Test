using UnityEngine;

public class StarAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayPop()
    {
        if (animator == null)
        {
            Debug.LogWarning("[StarAnimator] Animator가 연결되지 않았습니다: " + gameObject.name);
            return;
        }

        gameObject.SetActive(true); // 활성화하고
        animator.enabled = true;    // Animator 켜고
        animator.ResetTrigger("Pop");
        animator.SetTrigger("Pop"); // Pop 애니메이션 실행
    }
}
