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
    public bool hidePanelAfterMessage = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new System.Exception("Only one DialogBox is allowed");
    }
    

    bool endWriteFlag = false;
    public void WriteMessage(string message, float timeInterval, UnityAction onMessageFinish = null, bool waitForReadingTime = false, Color color = default)
    {
        if(color == default) color = Color.white;
        this.message.color = color;
        panel.SetActive(true);
        var caracterCount = message.Length;
        var readingTime = caracterCount / cpsReadingTime;
        if(subtitleBehaviour){
            this.message.text = message;
        }
        else{
            var time = waitForReadingTime ? 2 : 0;
            StartCoroutine(TypeMessage(message, timeInterval, onMessageFinish, time));
        }
        if (hidePanelAfterMessage)
            Invoke("HidePanel", readingTime);
    }

    bool isWriting = false;
    private IEnumerator TypeMessage(string text, float timeInterval, UnityAction onMessageFinish = null, float readingTime = 0) {
        isWriting = true;
        message.text = "";
        foreach (char c in text.ToCharArray()) {
            if(endWriteFlag){
                endWriteFlag = false;
                message.text = text;
                break;
            }
            message.text += c;
            yield return new WaitForSeconds(timeInterval);
        }
        isWriting = false;
        if(readingTime > 0)
        {
            yield return new WaitForSeconds(readingTime);
        }
        onMessageFinish?.Invoke();
        
    }

    public void Clean()
    {
        message.text = "";
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (isWriting)
                endWriteFlag = true;
        }
    }
}
