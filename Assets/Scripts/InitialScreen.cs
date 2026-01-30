using UnityEngine;

public class InitialScreen : MonoBehaviour
{
    public static InitialScreen Instance { get; private set; }

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            throw new System.Exception("No");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
