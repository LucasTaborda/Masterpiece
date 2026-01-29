using UnityEngine;

public class ButtonBoard : MonoBehaviour
{
    public ScenographyController scenographyController;

    private void Awake()
    {
        scenographyController = FindFirstObjectByType<ScenographyController>();
    }

    public void BringTreeIn()
    {
        scenographyController.MoveObject(ScenographyController.TREE);
    }

    public void BringSunIn()
    {
        scenographyController.MoveObject(ScenographyController.SUN);
    }

    // public void BringTreeIn()
    // {
    //     scenographyController.MoveObject(ScenographyController.TREE, new Vector2(-3.58f, -0.46f));
    // }

    // public void BringSunIn()
    // {
    //     scenographyController.MoveObject(ScenographyController.SUN, new Vector2(4.31f, 0.88f));
    // }
}
