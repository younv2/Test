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
            Debug.LogWarning("[StarAnimator] Animator�� ������� �ʾҽ��ϴ�: " + gameObject.name);
            return;
        }

        gameObject.SetActive(true); // Ȱ��ȭ�ϰ�
        animator.enabled = true;    // Animator �Ѱ�
        animator.ResetTrigger("Pop");
        animator.SetTrigger("Pop"); // Pop �ִϸ��̼� ����
    }
}
