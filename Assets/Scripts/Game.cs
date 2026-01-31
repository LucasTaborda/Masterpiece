using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
