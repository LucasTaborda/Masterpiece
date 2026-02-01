using UnityEngine;

public class DionysusBody : MonoBehaviour
{
    private Animator animator;
    public Animator killerAnimator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnReverenceFinished()
    {
        if(!Game.actorHasKnife)
            animator.SetTrigger("ShowBomb");
        else
            killerAnimator.SetTrigger("Show");
    }
}
