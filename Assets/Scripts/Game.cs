using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }
    public static bool isActorLiberated = false;
    public static bool actorHasKnife = false;
    public static bool IsCrownBroken  { get; private set; }
    public static bool IsSunBroken    { get; private set; }    
    public static bool IsKnifeTaken   { get; private set; }
    public static int RopeDamageLevel { get; private set; }

    public enum RopeDamager { Knife, Sun, Crown }

    public static void DamageRope(RopeDamager damager)
    {
        if (damager == RopeDamager.Sun)
        {
            IsSunBroken = true;
        }
        else if (damager == RopeDamager.Crown){
            IsCrownBroken = true;
        }
        RopeDamageLevel++;

        //AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_ROPE], 1f);
    }

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
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_START], 1f);
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

    public static bool IsKeyInBrokenScenography(string key)
    {
        if(key == "CROWN" && IsCrownBroken) return true;
        else if(key == "SUN" && IsSunBroken) return true;
        else return false;
    }
}
