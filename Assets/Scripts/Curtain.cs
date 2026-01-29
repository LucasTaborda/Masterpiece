using UnityEngine;

public class Curtain : MonoBehaviour
{
    public Animator animator;

    public void DownUp()
    {
        animator.SetTrigger("DownUp");
    }

    public void Up()
    {
        animator.SetTrigger("Up");
    }

    public void Down()
    {
        animator.SetTrigger("Down");
    }
}
