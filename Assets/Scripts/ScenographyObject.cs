using UnityEngine;

public class ScenographyObject : MonoBehaviour
{
    [HideInInspector] public Vector2 initialPos;
    public Vector2 finalPos;
    public float moveSpeed = 0.5f;

    void Awake()
    {
        initialPos = gameObject.transform.position;
    }

    public void MoveToFinalPos()
    {
        LeanTween.move(gameObject, finalPos, moveSpeed);
    }

    public void MoveToInitialPos()
    {
        LeanTween.move(gameObject, initialPos, moveSpeed);
    }
}
