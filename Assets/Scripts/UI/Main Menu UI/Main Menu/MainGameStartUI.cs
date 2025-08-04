using System.Collections;
using UnityEngine;

public class MainGameStartUI : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        animator.ResetTrigger("close");
    }
}
