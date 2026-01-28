using UnityEngine;

public class ScenographyController : MonoBehaviour
{
    public GameObject[] scenographyObjects;
    public float moveSpeed = 0.5f;
    public const int SUN = 0;
    public const int TREE = 1;

    public void MoveObject(int index, Vector2 position)
    {
        LeanTween.move(scenographyObjects[index], position, moveSpeed);
    }
}
