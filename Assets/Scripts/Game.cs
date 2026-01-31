using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }
    public static bool isActorLiberated = true;
    public static bool actorHasKnife = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new System.Exception("Only one Game is allowed");
    }

    void Start()
    {
        ButtonBoard.Instance.SetInputActive(false);
    }

    public void Initialize()
    {
        InitialScreen.Instance.Hide();
        Invoke("TurnOnLights", 2f);
        Invoke("SpawnTV", 3.5f);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void TurnOnLights()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_SPOTLIGHT], 1f);
        Curtain.Instance.TurnOnLights();
    }

    private void SpawnTV()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_TV_SWITCH], 1f);
        Dionysus.Instance.SpawnTV();
    }
}
