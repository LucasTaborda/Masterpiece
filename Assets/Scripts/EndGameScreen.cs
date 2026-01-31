using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour
{
    public TMP_Text label;
    public TMP_Text buttonText;
    public string badEndingLabel = "No lograste liberar a nadie";
    public string incompleteEndingLabel = "Podrías haberte salvado";
    public string goodEndingLabel = "El Director ha sido asesinado";
    public Color badEndingColor;
    public Color incompleteEndingColor;
    public Color goodEndingColor;
    public Image backgroundImage;
    public Image buttonImage;
    private string selectedLabel;

    void Start()
    {
        if (Game.isActorLiberated && Game.actorHasKnife)
            selectedLabel = goodEndingLabel;
        else if (Game.isActorLiberated && !Game.actorHasKnife)
            selectedLabel = incompleteEndingLabel;
        else
            selectedLabel = badEndingLabel;

        label.text = selectedLabel;

        FadeInBackground();
    }

    private void FadeInBackground()
    {
        if(selectedLabel == goodEndingLabel)
            backgroundImage.color = goodEndingColor;
        else if(selectedLabel == incompleteEndingLabel)
            backgroundImage.color = incompleteEndingColor;
        else
            backgroundImage.color = badEndingColor;
    
        LeanTween.value(gameObject, 0f, 1f, 1f)
        .setOnUpdate((float val) => {
            label.alpha = val;
            buttonText.alpha = val;
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, val);
            backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, val);
        });
    }
}
