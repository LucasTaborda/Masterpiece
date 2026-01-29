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

    public void WriteMessage(string message, float timeInterval) {
        panel.SetActive(true);
        var caracterCount = message.Length;
        var readingTime = caracterCount / cpsReadingTime;
        if(subtitleBehaviour){
            this.message.text = message;
        }
        else{
            StartCoroutine(TypeMessage(message, timeInterval));
        }
        Invoke("HidePanel", readingTime);
    }

    private IEnumerator TypeMessage(string text, float timeInterval) {
        message.text = "";
        foreach (char c in text.ToCharArray()) {
            message.text += c;
            yield return new WaitForSeconds(timeInterval);
        }
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }
}
