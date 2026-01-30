using UnityEngine;
using UnityEngine.Events;

public class ActManager : MonoBehaviour
{    
    public Act[] acts;
    private int currentAct = 0;
    private int currentScene = 0;
    public ButtonBoard buttonBoard;
    public float minDistanceScenography = 3f;

    void Start()
    {
        StartAct();
    }

    public void StartAct()
    {
        StartScene();
    }

    private void StartCronometer()
    {
        Debug.Log("Cronometer Start?");
        Cronometer.Instance.SetAndStart(acts[currentAct].scenes[currentScene].time, LoseScene);
    }

    private void StartScene()
    {
        Debug.Log("Act:" + currentAct + " Scene:" + currentScene);
        var key = acts[currentAct].scenes[currentScene].dialogKey;
        var message = Dionysus.Instance.dialogs[key];
        DialogBox.Instance.WriteMessage(message, Dionysus.Instance.talkingSpeed, StartCronometer);
    }

    public void NextScene()
    {
        if(currentScene == acts[currentAct].scenes.Length - 1) EndAct();
        currentScene++;
        Cronometer.Instance.Stop();
        StartScene();
    }

    private void LoseScene()
    {
        Dionysus.Instance.MakeSad();
        NextScene();
    }

    private void EndAct()
    {
        Curtain.Instance.DownUp();
        currentAct++;
        currentScene = 0;
        StartAct();
    }

    public void AnalyzePhotography()
    {
        var photo = acts[currentAct].scenes[currentScene].scenographyPosition;
        for(int i = 0; i < photo.transforms.Length; i++)
        {
            if(photo.transforms[i] == null) continue;
            if(i >= buttonBoard.scenographyObjects.Length) continue;
            if(Vector2.Distance(photo.transforms[i].position, buttonBoard.scenographyObjects[i].transform.position) > minDistanceScenography)
                return;
        }
        for(int i = 0; i < photo.keys.Length; i++)
        {
            if(string.IsNullOrEmpty(photo.keys[i])) continue;
            if(i >= buttonBoard.scenographyObjects.Length) continue;

            if(photo.keys[i] != buttonBoard.scenographyObjects[i].GetKey())
                return;
        }
        Dionysus.Instance.MakeHappy();
        NextScene();
    }

    void Update()
    {
        AnalyzePhotography();
    }
}
