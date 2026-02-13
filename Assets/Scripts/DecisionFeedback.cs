using UnityEngine;
using UnityEngine.UI;

public class DecisionFeedback : MonoBehaviour
{
    public Color successColor;
    public Color failureColor;
    public Image feedbackImage;
    public Animator animator;
    public static DecisionFeedback Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new System.Exception("Only one DecisionFeedback is allowed");
    }

    public void SetFeedback(bool success)
    {
        feedbackImage.color = success ? successColor : failureColor;
        animator.SetTrigger("Feedback");
    }

}
