using UnityEngine;

public class Curtain : MonoBehaviour
{
    public Animator animator;

    public static Curtain Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new System.Exception("Only one Curtain is allowed");
    }

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
