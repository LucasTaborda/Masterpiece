using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogBox : MonoBehaviour
{
    public TMP_Text message;
    public GameObject panel;
    public int cpsReadingTime = 12;
    public bool subtitleBehaviour = false;
    public static DialogBox Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new System.Exception("Only one DialogBox is allowed");
    }
    

    public void WriteMessage(string message, float timeInterval, UnityAction onMessageFinish = null) {
        panel.SetActive(true);
        var caracterCount = message.Length;
        var readingTime = caracterCount / cpsReadingTime;
        if(subtitleBehaviour){
            this.message.text = message;
        }
        else{
            StartCoroutine(TypeMessage(message, timeInterval, onMessageFinish));
        }
        Invoke("HidePanel", readingTime);
    }

    private IEnumerator TypeMessage(string text, float timeInterval, UnityAction onMessageFinish = null) {
        message.text = "";
        foreach (char c in text.ToCharArray()) {
            message.text += c;
            yield return new WaitForSeconds(timeInterval);
        }
        onMessageFinish?.Invoke();
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }
}
