using UnityEngine;

public class Target : MonoBehaviour
{
    public ButtonBoard buttonBoard;
    private RectTransform rectTransform;
    private Camera cam;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cam = Camera.main;
    }

    void LateUpdate()
    {
        var centerPosition = buttonBoard.targetSpriteRenderer.bounds.center;
        rectTransform.position = cam.WorldToScreenPoint(centerPosition);
    }

}
