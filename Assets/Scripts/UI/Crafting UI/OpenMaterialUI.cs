using UnityEngine;
using System.Collections;

public class OpenMaterialUI : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Toggle()
    {
        if (isOpen)
            StartCoroutine(CloseMaterial());
        else
            StartCoroutine(OpenMaterials());

        isOpen = !isOpen;
    }
    public IEnumerator CloseMaterial()
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(0.1f);
        animator.ResetTrigger("close");
    }
    public IEnumerator OpenMaterials()
    {
        animator.SetTrigger("open");
        yield return new WaitForSeconds(0.1f);
        animator.ResetTrigger("open");
    }
}
