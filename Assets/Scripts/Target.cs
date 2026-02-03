using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public ButtonBoard buttonBoard;
    private RectTransform rectTransform;
    private Camera cam;
    public Canvas canvas;
    public RectTransform canvasRect;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cam = Camera.main;
    }

    void OnEnable()
    {
        buttonBoard.SetTargetIlluminated(true);
    }

    void OnDisable()
    {
        buttonBoard.SetTargetIlluminated(false);
    }

    void LateUpdate()
    {
        var centerPosition = buttonBoard.targetSpriteRenderer.bounds.center;
        
        var screenPoint = cam.WorldToScreenPoint(centerPosition);
            Vector2 localPoint;
    
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect, 
            screenPoint, 
            canvas.worldCamera,
            out localPoint
        );
        
        rectTransform.anchoredPosition = localPoint;
    }

}
