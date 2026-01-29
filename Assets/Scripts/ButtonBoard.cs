using UnityEngine;
using UnityEngine.UI;

public class ButtonBoard : MonoBehaviour
{
    public ScenographyController scenographyController;
    public ScenographyObject[] scenographyObjects;
    public ScenographyObject currentScenographyObject { get { return scenographyObjects[currentObject]; } }
    private int currentObject = 0;

    public Button[] buttons;

    private void Awake()
    {
        scenographyController = FindFirstObjectByType<ScenographyController>();
    }

    private void Start()
    {
        // SetActOneButtons();
    }

    public void ChangeScenographyObjectSelected()
    {
        if(currentObject == scenographyObjects.Length - 1)
            currentObject = 0;
        else
            currentObject++;
    }

    public void MoveHorizontal()
    {
        scenographyObjects[currentObject].moveHorizontal = true;
    }

    public void StopMoving()
    {
        scenographyObjects[currentObject].moveHorizontal = false;
        scenographyObjects[currentObject].moveVertical = false;
    }

    public void MoveVertical()
    {
        scenographyObjects[currentObject].moveVertical = true;
    }

    public void ChangeSkin()
    {
        scenographyObjects[currentObject].ChangeSkin();
    }

    // public void BringTreeIn()
    // {
    //     scenographyController.MoveObject(ScenographyController.TREE);
    // }

    // public void BringSunIn()
    // {
    //     scenographyController.MoveObject(ScenographyController.SUN);
    // }

    // public void SetActOneButtons()
    // {
    //     UnsetButtons();
    //     buttons[0].onClick.AddListener(BringTreeIn);
    //     buttons[1].onClick.AddListener(BringSunIn);
    // }

    // public void UnsetButtons()
    // {
    //     foreach(Button button in buttons)
    //     {
    //         button.onClick.RemoveAllListeners();
    //     }
    // }

    public void PlayButtonSound()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_BUTTON]);
    }

    public void PlayClaps()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_CLAPS]);
        Dionysus.Instance.MakeHappy();
    }

}
