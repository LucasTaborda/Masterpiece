using UnityEngine;
using UnityEngine.UI;

public class ButtonBoard : MonoBehaviour
{
    public ScenographyObject[] scenographyObjects;
    public ScenographyObject currentScenographyObject { get { return scenographyObjects[currentObject]; } }
    private int currentObject = 0;
    private bool isActive = false;
    private RectTransform rectTransform;
    private Vector3 originalPosition;
    public SpriteRenderer targetSpriteRenderer { get; private set; }
    public Color selectedColor = new Color(1f, 1f, 1f, 1f);
    public Color defaultColor = new Color(1f, 1f, 1f, 1f);
    public Button[] buttons;
    public Target target;
    public static ButtonBoard Instance { get; private set; }
    private Rail CurrentRail { get { return currentScenographyObject.rail; } }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            throw new System.Exception("Only one ButtonBoard is allowed");

        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = new Vector2(originalPosition.x, -300f);
    }

    public void SetButtonsEnabled(bool enabled) {
        foreach(Button button in buttons){
            button.interactable = enabled;
        }
    }

    public void SetTargetIlluminated(bool illuminate){
        if(targetSpriteRenderer == null) return;
        if(illuminate)
            targetSpriteRenderer.material.color = selectedColor;
        else
            targetSpriteRenderer.material.color = defaultColor;
    }
    
    public void ChangeScenographyObjectSelected()
    {
        scenographyObjects[currentObject].spriteRenderer.material.color = defaultColor;
        if(currentObject == scenographyObjects.Length - 1)
            currentObject = 0;
        else{
            currentObject++;
            while(scenographyObjects[currentObject] == null || !scenographyObjects[currentObject].isActiveAndEnabled){
                if(currentObject == scenographyObjects.Length - 1)
                    currentObject = 0;
                else currentObject++;
            }
        }
        targetSpriteRenderer = scenographyObjects[currentObject].spriteRenderer;
        targetSpriteRenderer.material.color = selectedColor;
    }

    public void ResetCurrentObject()
    {
        currentObject = 0;
        // targetSpriteRenderer = scenographyObjects[currentObject].GetComponent<SpriteRenderer>();
    }

    public void MoveHorizontal()
    {
        if(!isActive) return;
        scenographyObjects[currentObject].moveHorizontal = true;
    }

    public void PressHorizontalButton()
    {
        CurrentRail.TweenHorizontalToNextWaypoint(ActManager.Instance.AnalyzePhotography);
    }

    public void PressVerticalButton()
    {
        CurrentRail.TweenVerticalToNextLevel(ActManager.Instance.AnalyzePhotography);
    }

    public void StopMoving()
    {
        scenographyObjects[currentObject].moveHorizontal = false;
        scenographyObjects[currentObject].moveVertical = false;
    }

    public void MoveVertical()
    {
        if(!isActive) return;
        scenographyObjects[currentObject].moveVertical = true;
    }

    public void ChangeSkin()
    {
        if(!isActive) return;
        scenographyObjects[currentObject].ChangeSkin(ActManager.Instance.AnalyzePhotography);
    }

    public void SetInputActive(bool active)
    {
        isActive = active;
        //if(active)ChangeScenographyObjectSelected();
        if(active) targetSpriteRenderer = scenographyObjects[currentObject].GetComponent<SpriteRenderer>();
        target.gameObject.SetActive(active);
    }


    public void PlayButtonSound()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_BUTTON]);
    }

    public void PlayClaps()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_CLAPS]);
        Dionysus.Instance.MakeHappy();
    }

    public void PlayRailSound()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_RAIL]);
    }

    public void PlayRailUpSound()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_RAIL_UP]);
    }

    public void PlayChangeSound()
    {
        if(currentScenographyObject.isHuman) 
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_GRUNT]);
        else
            AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_CARDBOARD],1);
    }

    public void Spawn()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.sfxClips[AudioManager.SFX_BOARD_SPAWN]);
        LeanTween.moveY(rectTransform, originalPosition.y, 1f).setEase(LeanTweenType.easeOutBounce);
    }


}
