using UnityEngine;

public class Game : MonoBehaviour
{
    void Start()
    {
        ButtonBoard.Instance.SetInputActive(false);
    }

    public void Initialize()
    {
        InitialScreen.Instance.Hide();
        Dionysus.Instance.SpawnTV();
    }
}
