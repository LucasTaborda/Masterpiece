using UnityEngine;
using UnityEngine.Events;

public class Curtain : MonoBehaviour
{
    public Animator animator;

    public static Curtain Instance { get; private set; }
    private UnityAction downCallback;
    public GameObject shadow;
    
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
        Debug.Log("Curtain up");
        animator.SetTrigger("Up");
    }

    public void Down(UnityAction callback = null)
    {
        if(callback != null) downCallback = callback;
        animator.SetTrigger("Down");
    }

    public void OnCurtainDown()
    {
        downCallback?.Invoke();
        downCallback = null;
    }

    public void TurnOnLights()
    {
        shadow.SetActive(false);
    }
}
