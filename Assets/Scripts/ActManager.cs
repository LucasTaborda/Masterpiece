using UnityEngine;
using UnityEngine.Events;

public class ActManager : MonoBehaviour
{    
    public Act[] acts;
    private int currentAct = 0;
    private int currentScene = 0;
    public ButtonBoard buttonBoard;
    public float minDistanceScenography = 3f;
    public ScenographyObject[] scenographyObjects = new ScenographyObject[5];
    public static ActManager Instance { get; private set; }
    private bool interrupted = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new System.Exception("Only one ActManager is allowed");
    }

    // void Start()
    // {
    //     StartAct();
    // }

    public void StartMasterPiece()
    {
        Curtain.Instance.Up();
        StartAct();
    }

    public void StartAct()
    {
        ChangeScenography();
        Curtain.Instance.Up();
        StartScene();
    }

    private void StartCronometer()
    {
        Debug.Log("Cronometer Start?");
        var time = acts[currentAct].scenes[currentScene].time;
        if(time < 0) return;
        Cronometer.Instance.SetAndStart(time, LoseScene);
    }

    private void StartDecision()
    {
        StartCronometer();
        buttonBoard.SetInputActive(true);
    }

    private void StartScene()
    {
        Debug.Log("Act:" + currentAct + " Scene:" + currentScene);
        var key = acts[currentAct].scenes[currentScene].dialogKey;
        var message = Dionysus.Instance.dialogs[key];
        DialogBox.Instance.WriteMessage(message, Dionysus.Instance.talkingSpeed, StartDecision);
    }

    public void NextScene()
    {
        buttonBoard.SetInputActive(false);
        Cronometer.Instance.Stop();
        if(currentScene == acts[currentAct].scenes.Length - 1) NextAct();
        else{
            currentScene++;
            StartScene();
        }
    }

    private void LoseScene()
    {
        Dionysus.Instance.MakeSad();
        if(!interrupted) NextScene();
    }

    private void EndAct()
    {
        var dialogKey = acts[currentAct].lastActDialogKey;
        if(!string.IsNullOrEmpty(dialogKey))
            DialogBox.Instance.WriteMessage(Dionysus.Instance.dialogs[dialogKey], Dionysus.Instance.talkingSpeed, SetNextActOrEndGame);
        else SetNextActOrEndGame();
    }

    private void SetNextActOrEndGame()
    {
        if(currentAct == acts.Length - 1) EndGame();
        else{
            currentAct++;
            currentScene = 0;
            StartAct();
        }
    }

    private void NextAct()
    {
        Curtain.Instance.Down(EndAct);
    }

    private void ChangeScenography()
    {
        ScenographyObject[] previousScenography;
        if(currentAct != 0) previousScenography = acts[currentAct - 1].scenographyObjects;
        else previousScenography = null;
        var currentScenography = acts[currentAct].scenographyObjects;
        for(int i = 0; i < currentScenography.Length; i++) {
            if(currentScenography[i] != null){
                currentScenography[i].gameObject.SetActive(true);
                buttonBoard.scenographyObjects[i] = currentScenography[i];
            }
        }
        if(previousScenography != null){
            for(int i = 0; i < previousScenography.Length; i++) {
                if(previousScenography[i] != null){
                    previousScenography[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void AnalyzePhotography()
    {
        Invoke("AnalyzeRetarded", 1f);
    }

    private void AnalyzeRetarded()
    {
        Debug.Log("============");
        Debug.Log("ANALYZE PHOTOGRAPHY");
        Debug.Log("Analyze transforms:");
        var photo = acts[currentAct].scenes[currentScene].scenographyPosition;
        for(int i = 0; i < photo.transforms.Length; i++)
        {
            if(photo.transforms[i] == null) {
                Debug.Log("photo.transforms[" + i + "] is null");
                continue;
            }
            if(i >= buttonBoard.scenographyObjects.Length || buttonBoard.scenographyObjects[i] == null) {
                Debug.Log("buttonBoard.scenographyObjects[" + i + "] is null");
                continue;
            }
            if(Vector2.Distance(photo.transforms[i].position, buttonBoard.scenographyObjects[i].transform.position) > minDistanceScenography) {
                Debug.Log("Distance too big");
                return;
            }
        }
        Debug.Log("Analyze keys:");
        for(int i = 0; i < photo.keys.Length; i++)
        {
            if(string.IsNullOrEmpty(photo.keys[i])) {
                Debug.Log("photo.keys[" + i + "] is null");
                continue;
            }
            if(i >= buttonBoard.scenographyObjects.Length || buttonBoard.scenographyObjects[i] == null) {
                Debug.Log("buttonBoard.scenographyObjects[" + i + "] is null");
                continue;
            }

            if(photo.keys[i] != buttonBoard.scenographyObjects[i].GetKey()) {
                Debug.Log("Key " + photo.keys[i] + " does not match " + buttonBoard.scenographyObjects[i].GetKey());
                return;
            }
        }
        Dionysus.Instance.MakeHappy();
        NextScene();

    }
    // void Update()
    // {
    //     AnalyzePhotography();
    // }

    public void Interrupt()
    {
        interrupted = true;
        buttonBoard.SetInputActive(false);
        Curtain.Instance.Down(GameOverLine);
    }

    private void GameOverLine()
    {
        Dionysus.Instance.SayGameOver();
    }

    private void EndGame()
    {
        Debug.Log("Game Over");
    }
}
