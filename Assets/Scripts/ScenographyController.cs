using UnityEngine;

public class ScenographyController : MonoBehaviour
{
    public ScenographyObject[] scenographyObjects;
    public float moveSpeed = 0.5f;
    public const int SUN = 0;
    public const int TREE = 1;

    public void MoveObject(int index)
    {
        print("object: " + scenographyObjects[index]);
        scenographyObjects[index].MoveToFinalPos();
    }

    public void ResetObject(int index)
    {
        scenographyObjects[index].MoveToInitialPos();
    }
}
