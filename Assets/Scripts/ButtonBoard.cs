using UnityEngine;
using UnityEngine.UI;

public class ButtonBoard : MonoBehaviour
{
    public ScenographyController scenographyController;
    public Button[] buttons;

    private void Awake()
    {
        scenographyController = FindFirstObjectByType<ScenographyController>();
    }

    private void Start()
    {
        SetActOneButtons();
    }

    public void BringTreeIn()
    {
        scenographyController.MoveObject(ScenographyController.TREE);
    }

    public void BringSunIn()
    {
        scenographyController.MoveObject(ScenographyController.SUN);
    }

    public void SetActOneButtons()
    {
        UnsetButtons();
        buttons[0].onClick.AddListener(BringTreeIn);
        buttons[1].onClick.AddListener(BringSunIn);
    }

    public void UnsetButtons()
    {
        foreach(Button button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    public void PlayButtonSound()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_BUTTON]);
    }

}
