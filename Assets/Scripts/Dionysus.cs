using System.Collections.Generic;
using UnityEngine;

public class Dionysus : MonoBehaviour
{
    public Eye happyEye;
    public Eye sadEye;
    public int currentHappiness = 3;
    public int maxHappiness = 5;
    public Dictionary<string, string> dialogs = new Dictionary<string, string>();
    public DialogBox dialogBox;
    public float talkingSpeed = 0.05f;
    public static Dionysus Instance { get; private set; }

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            throw new System.Exception("Only one Dionysus is allowed");
        
        InitializeDialogs();
    }

    private void InitializeDialogs()
    {
        dialogs.Add("HELLO", "Antes pensaba que la vida era una tragedia. Finalmente me di cuenta que es una comedia.");
    }


    void Start()
    {
        dialogBox.WriteMessage(dialogs["HELLO"], talkingSpeed);
    }

    public void MakeHappy()
    {
        happyEye.Open();
        if(currentHappiness < maxHappiness)
            currentHappiness++;
    }

    public void MakeSad()
    {
        sadEye.Open();
        if (currentHappiness > 0)
            currentHappiness--;
        else
            Debug.Log("You Lose");
    }

    void OnGUI(){
        GUILayout.Label("Happiness: " + currentHappiness);
    }
}
