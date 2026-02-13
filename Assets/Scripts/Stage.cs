using UnityEngine;
using UnityEngine.Events;

public class Stage : MonoBehaviour
{
    public Curtain curtain;
    public GameObject shadow;
    public Dionysus dionysus;

    public void DropCurtain(UnityAction callback = null)
    {
        curtain.Down(callback);
    }

    public void RaiseCurtain(UnityAction callback = null)
    {
        curtain.Up(callback);
    }

    public void TurnOnLights()
    {
        if(!shadow.activeSelf) return;
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_SPOTLIGHT]);
        shadow.SetActive(false);
    }

    public void TurnOffLights()
    {
        if(shadow.activeSelf) return;
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_LIGHT_SWITCH]);
        shadow.SetActive(true);
    }

    public void SpawnTV()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_TV_SWITCH]);
        dionysus.SpawnTV();
    }
}
