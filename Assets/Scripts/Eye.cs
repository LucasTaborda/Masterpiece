using UnityEngine;

public class Eye : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Open(bool closeAfter = true)
    {
        animator.SetTrigger("Open");

        if (closeAfter)
            Invoke("Close", 1f);
    }

    public void Close()
    {
        animator.SetTrigger("Close");
    }

    public void Shine()
    {
        animator.SetTrigger("Shine");
    }
}
