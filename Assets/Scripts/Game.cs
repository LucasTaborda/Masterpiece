using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }
    public static bool isActorLiberated = false;
    public static bool actorHasKnife = false;
    public static bool IsCrownBroken  { get; private set; }
    public static bool IsSunBroken    { get; private set; }    
    public static bool IsKnifeHooked;
    public static int RopeDamageLevel { get; private set; }
    private static int maxDamageLvl = 2;
    public CreditScreen creditScreen;

    public enum RopeDamager { Knife, Sun, Crown }
    public static List<UnityAction> onRopeDamaged = new();

    public static void AddRopeDamagedListener(UnityAction action)
    {
        onRopeDamaged.Add(action);
    }

    public static void RemoveRopeDamagedListener(UnityAction action)
    {
        onRopeDamaged.Remove(action);
    }

    public static void DamageRope(RopeDamager damager)
    {
        if(isActorLiberated) return;
        if (damager == RopeDamager.Sun)
        {
            IsSunBroken = true;
        }
        else if (damager == RopeDamager.Crown){
            IsCrownBroken = true;
        }
        RopeDamageLevel++;

        if(RopeDamageLevel >= maxDamageLvl)
            isActorLiberated = true;
        
        onRopeDamaged.ForEach(x => x.Invoke());
        //AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_ROPE], 1f);
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new System.Exception("Only one Game is allowed");
        
        RopeDamageLevel = 0;
    }

    void Start()
    {
        ButtonBoard.Instance.SetButtonsEnabled(false);
        ButtonBoard.Instance.SetInputActive(false);
    }

    public void Initialize()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_START]);
        InitialScreen.Instance.Hide();
        Invoke("TurnOnLights", 2f);
        Invoke("SpawnTV", 3.5f);
    }

    public void Restart()
    {
        isActorLiberated = false;
        actorHasKnife = false;
        IsCrownBroken = false;
        IsSunBroken = false;
        IsKnifeHooked = false;
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
        else if((key == "SWORD_1" || key == "SWORD_2" || key == "SWORD_3" || key == "SWORD_4") && IsKnifeHooked) return true;
        else if((key == "KILLER_1" || key == "KILLER_2" || key == "KILLER_3" || key == "KILLER_4") && actorHasKnife) return true;
        else return false;
    }

    public void ShowCredits()
    {
        creditScreen.gameObject.SetActive(true);
    }

    // void OnGUI()
    // {
    //     GUILayout.Label("Rope Damage Level: " + RopeDamageLevel);
    //     GUILayout.Label("Sun Broken: " + IsSunBroken);
    //     GUILayout.Label("Crown Broken: " + IsCrownBroken);
    //     GUILayout.Label("Knife Hooked: " + IsKnifeHooked);
    //     GUILayout.Label("Actor Has Knife: " + actorHasKnife);
    //     GUILayout.Label("Actor Liberated: " + isActorLiberated);
    // }
}
