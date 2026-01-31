using UnityEngine;
using UnityEngine.Events;

public class Curtain : MonoBehaviour
{
    public Animator animator;

    public static Curtain Instance { get; private set; }
    private UnityAction downCallback;
    private UnityAction upCallback;
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

    public void Up(UnityAction callback = null)
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_CURTAIN]);
        if(callback != null) upCallback = callback;
        animator.SetTrigger("Up");
    }

    public void Down(UnityAction callback = null)
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_CURTAIN]);
        if(callback != null) downCallback = callback;
        animator.SetTrigger("Down");
    }

    public void OnCurtainDown()
    {
        downCallback?.Invoke();
        downCallback = null;
    }

    public void OnCurtainUp()
    {
        upCallback?.Invoke();
        upCallback = null;
    }

    public void TurnOnLights()
    {
        shadow.SetActive(false);
    }
}
