using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitialScreen : MonoBehaviour
{
    public static InitialScreen Instance { get; private set; }
    public TMP_Text title;
    public TMP_Text buttonText;
    public Image buttonImage;
    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            throw new System.Exception("No");
    }

    public void Hide()
    {
        LeanTween.value(gameObject, 1f, 0f, 1f)
        .setOnUpdate((float val) => {
            title.alpha = val;
            buttonText.alpha = val;
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, val);
        }).setOnComplete(() => {
            gameObject.SetActive(false);
        });
    }
}
