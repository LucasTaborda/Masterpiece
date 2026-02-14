using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ActManager : MonoBehaviour
{    
    public Act[] acts;
    private int currentAct = 0;
    private int currentScene = 0;
    public ButtonBoard buttonBoard;
    public float minDistanceScenography = 3f;
    // public ScenographyObject[] scenographyObjects = new ScenographyObject[5];
    public static ActManager Instance { get; private set; }
    public GameObject lastScene;
    private bool interrupted = false;
    public GameObject endGameScreen;
    public int initialAct = 0;
    public bool runIntro = true;
    public ActTitle actTitle;
    public int actOffset;
    public StageScene CurrentScene { get { return acts[currentAct].scenes[currentScene]; } }
    public Stage stage;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new System.Exception("Only one ActManager is allowed");

        currentAct = initialAct;
    }

    public void StartMasterPiece()
    {
        stage.RaiseCurtain();
        ButtonBoard.Instance.Spawn();
        StartAct();
    }

    public void StartAct()
    {
        ChangeScenography();
        ChangeNotPlayableScenography();
        ButtonBoard.Instance.ResetCurrentObject();
        stage.RaiseCurtain();
        actTitle.gameObject.SetActive(false);
        StartScene();
    }

    private void SetActTitle()
    {
        if(string.IsNullOrEmpty(acts[currentAct].title)) {
            StartAct();
            return;
        }
        DialogBox.Instance.Clean();
        var act = "Acto " + (currentAct + actOffset);
        actTitle.title.text = act + "\n" + acts[currentAct].title;
        actTitle.gameObject.SetActive(true);
        actTitle.Show(WaitAndHideTitle);
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_ACT_START]);
    }

    private void WaitAndHideTitle()
    {
        Invoke("HideActTitle", 3f);
    }

    private void HideActTitle()
    {
        actTitle.Hide(StartAct);
    }

    private void StartCronometer()
    {
        var time = acts[currentAct].scenes[currentScene].time;
        if(time < 0) return;
        Cronometer.Instance.SetAndStart(time, LoseScene);
    }

    private void StartDecision()
    {
        StartCronometer();
        buttonBoard.SetInputActive(true);
        buttonBoard.SetButtonsEnabled(true);
    }

    private void StartScene()
    {
        if(currentScene != 0) {
            SpawnDynamicScenography();
            CheckDisabledScenography();
        }
        if(string.IsNullOrEmpty(CurrentScene.openingDialogKey)) StartSceneGame();
        else
            DialogBox.Instance.WriteMessage(Dionysus.Instance.dialogs[CurrentScene.openingDialogKey], Dionysus.Instance.talkingSpeed, StartSceneGame, true, default, true);
    }

    private void CheckDisabledScenography()
    {
        foreach(var obj in acts[currentAct].scenographyObjects){
            if(obj == null) continue;
            if(obj.sceneWhenObjectIsDisabled == currentScene) {
                obj.SetDisabled();
            }
        }
    }

    private void StartSceneGame()
    {
        // Curtain.Instance.shadow.SetActive(false);
        stage.TurnOnLights();
        var key = acts[currentAct].scenes[currentScene].dialogKey;
        var message = Dionysus.Instance.dialogs[key];
        DialogBox.Instance.WriteMessage(message, Dionysus.Instance.talkingSpeed, StartDecision, false, Color.yellow);
    }

    private void SpawnDynamicScenography()
    {
        bool spawned = false;
        foreach(var obj in acts[currentAct].scenographyObjects){
            if(obj == null) continue;
            if(obj.firstSceneIndex == currentScene) {
                obj.gameObject.SetActive(true);
                obj.rail.SetScenographyObject(obj, 1f);
                spawned = true;
            }
        }
        if(spawned)
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_RAIL_AUTO]);
    }

    public void NextScene()
    {
        if(currentScene == acts[currentAct].scenes.Length - 1) NextAct();
        else{
            currentScene++;
            StartScene();
        }
    }

    private void EndScene()
    {
        buttonBoard.SetInputActive(false);
        buttonBoard.SetButtonsEnabled(false);
        Cronometer.Instance.Stop();
        if(CurrentScene.turnOffLightsOnEnding) stage.TurnOffLights();
        Invoke("WaitForNextScene", 1f);
    }

    private void WaitForNextScene()
    {
        if(!string.IsNullOrEmpty(CurrentScene.endingDialogKey)){
            DialogBox.Instance.WriteMessage(Dionysus.Instance.dialogs[CurrentScene.endingDialogKey], Dionysus.Instance.talkingSpeed, NextScene, true, default, true);
        }
        else NextScene();
    }


    private void LoseScene()
    {
        Dionysus.Instance.MakeSad();
        if(!interrupted) EndScene();
    }

    private void EndAct()
    {
        var dialogKey = acts[currentAct].lastActDialogKey;
        if(!string.IsNullOrEmpty(dialogKey))
            DialogBox.Instance.WriteMessage(Dionysus.Instance.dialogs[dialogKey], Dionysus.Instance.talkingSpeed, SetNextActOrEndGameRetarded);
        else SetNextActOrEndGame();
    }

    private void SetNextActOrEndGameRetarded()
    {
        Invoke("SetNextActOrEndGame", 3f);
    }

    private void SetNextActOrEndGame()
    {
        if(currentAct == acts.Length - 1) EndGame();
        else{
            currentAct++;
            currentScene = 0;
            SetActTitle();
            // StartAct();
        }
    }

    private void NextAct()
    {
        DialogBox.Instance.Clean();
        stage.DropCurtain(EndAct);
    }

    private void ChangeScenography()
    {
        ScenographyObject[] previousScenography;
        if(currentAct != 0) previousScenography = acts[currentAct - 1].scenographyObjects;
        else previousScenography = null;
        var currentScenography = acts[currentAct].scenographyObjects;
        for(int i = 0; i < currentScenography.Length; i++) {
            if(currentScenography[i] != null){
                buttonBoard.scenographyObjects[i] = currentScenography[i];
                if(currentScenography[i].firstSceneIndex == 0)
                    currentScenography[i].gameObject.SetActive(true);
            }
            else{
                buttonBoard.scenographyObjects[i] = null;
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

    private void ChangeNotPlayableScenography()
    {
        GameObject[] previousScenography;
        if(currentAct != 0) previousScenography = acts[currentAct - 1].notPlayableScenography;
        else previousScenography = null;
        var currentScenography = acts[currentAct].notPlayableScenography;
        for(int i = 0; i < currentScenography.Length; i++) {
            currentScenography[i].gameObject.SetActive(true);
        }
        if(previousScenography != null){
            for(int i = 0; i < previousScenography.Length; i++) {
                previousScenography[i].gameObject.SetActive(false);
            }
        }

    }

    public void AnalyzePhotography()
    {
        AnalyzeRetarded();
        // Invoke("AnalyzeRetarded", 1f);
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
                Debug.Log("Distance too big. Transform ID: " + i);
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
        for(var i = 0; i < photo.closeTop.Length; i++){
            if(photo.closeTop[i] == false) continue;
            float margin = 1f;
            var obj = buttonBoard.scenographyObjects[i];
            if (Mathf.Abs(obj.transform.position.y - obj.railLimit.topLimitTransform.position.y) > margin) return;
        }
        for(var i = 0; i < photo.closeBottom.Length; i++){
            if(photo.closeBottom[i] == false) continue;
            float margin = 0.5f;
            var obj = buttonBoard.scenographyObjects[i];
            if (Mathf.Abs(obj.transform.position.y - obj.railLimit.bottomLimitTransform.position.y) > margin) return;
        }
        Dionysus.Instance.MakeHappy();
        EndScene();

    }



    public void Interrupt()
    {
        interrupted = true;
        buttonBoard.SetInputActive(false);
        stage.DropCurtain(GameOverLine);
    }

    private void GameOverLine()
    {
        Dionysus.Instance.SayGameOver();
    }

    private void EndGame()
    {
        RemoveScenography();
        Dionysus.Instance.HideTV();
        lastScene.SetActive(true);
        stage.RaiseCurtain(SayEndMasterpiece);
        AudioManager.Instance.CrossFadeMusic(AudioManager.Instance.musicClips[AudioManager.MUSIC_CLIMAX]);
    }

    private void SayEndMasterpiece()
    {
        Dionysus.Instance.SayEndMasterpiece();
    }

    public void ChooseEnd()
    {
        if(Game.isActorLiberated && !Game.actorHasKnife) stage.DropCurtain(EndWithExplosion);
        else if(Game.isActorLiberated && Game.actorHasKnife) stage.DropCurtain(EndWithKnife);
        else stage.DropCurtain(EndWithExplosion);
    }

    public void EndWithExplosion()
    {
        Debug.Log("EXPLOSION");
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_EXPLOSION]);
        ShowEndGameScreen();
    }

    public void EndWithKnife()
    {
        Debug.Log("KNIFE");
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_STAB], 1f);
        Curtain.Instance.PlayKillAnimation();
        Invoke("ShowCredits", 3f);
    }

    private void ShowCredits()
    {
        Game.Instance.ShowCredits();
    }

    private void ShowEndGameScreen()
    {
        endGameScreen.SetActive(true);
    }

    private void RemoveScenography()
    {
        var scenography = acts[currentAct].scenographyObjects;

        foreach(var obj in scenography) {
            if(obj != null)
                obj.gameObject.SetActive(false);
        } 
    }
}
