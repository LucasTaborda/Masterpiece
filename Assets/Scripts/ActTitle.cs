using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ActTitle : MonoBehaviour
{
    public TMP_Text title;

    void OnEnable()
    {
        title.alpha = 0f;
    }

    public void Show(UnityAction callback = null)
    {
        LeanTween.value(gameObject, 0f, 1f, 1f)
        .setOnUpdate((float val) => {
            title.alpha = val;
        }).setOnComplete(() => {
            callback?.Invoke();
        });
    }

    public void Hide(UnityAction callback = null)
    {
        LeanTween.value(gameObject, 1f, 0f, 1f)
        .setOnUpdate((float val) => {
            title.alpha = val;
        }).setOnComplete(() => {
            callback?.Invoke();
        });
    }
}
