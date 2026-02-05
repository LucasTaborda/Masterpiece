using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditScreen : MonoBehaviour
{
    public float initialY = -1080;
    public float finalY = 1080f;
    public float duration = 30f;
    public RectTransform textTransform;
    public Image image;

    private void OnEnable()
    {
        image.color = new Color(0f, 0f, 0f, 0f);
        LeanTween.value(gameObject, 0f, 1f, 1f)
        .setOnUpdate((float val) => {
            image.color = new Color(0f, 0f, 0f, val);
        });

        textTransform.anchoredPosition = new Vector2(0f, initialY);
        LeanTween.moveY(textTransform, finalY, duration).setEase(LeanTweenType.linear)
        .setOnComplete(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
