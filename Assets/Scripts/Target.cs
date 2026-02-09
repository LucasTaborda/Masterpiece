using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public ButtonBoard buttonBoard;
    private RectTransform rectTransform;
    private Camera cam;
    public Canvas canvas;
    public RectTransform canvasRect;
    public float speed = 5f;

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
        
        //rectTransform.anchoredPosition = localPoint;
        rectTransform.localPosition = Vector3.Lerp(
            rectTransform.localPosition,
            localPoint,
            speed * Time.deltaTime
        );
        ///


    }

}
