using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Cronometer : MonoBehaviour
{
    public TMP_Text timeText;
    public static Cronometer Instance { get; private set; }
    private float currentTime;

    private void Awake(){
        if(Instance == null)
            Instance = this;
        else
            throw new System.Exception("Only one Cronometer is allowed");
    }

    public void SetAndStart(float time, UnityAction onFinish = null)
    {
        currentTime = time;
        timeText.text = currentTime.ToString("0");
        StartCoroutine(CountDown(onFinish));
    }

    private IEnumerator CountDown(UnityAction onFinish = null)
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime -= 1;
            timeText.text = currentTime.ToString("0");
        }

        timeText.text = "0";
        onFinish?.Invoke();
    }

    public void Stop()
    {
        StopAllCoroutines();
    }
}
