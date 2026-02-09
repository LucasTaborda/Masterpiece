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
    private Vector2 noiseOffset;
    public float pulseStrength = 6f;
    public float pulseSpeed = 1.2f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cam = Camera.main;

        noiseOffset = new Vector3(
            UnityEngine.Random.value * 100f,
            UnityEngine.Random.value * 100f
        );

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
        
        float nx = Mathf.PerlinNoise(Time.time * pulseSpeed, noiseOffset.x) - 0.5f;
        float ny = Mathf.PerlinNoise(Time.time * pulseSpeed, noiseOffset.y) - 0.5f;

        Vector2 pulse = new Vector2(nx, ny) * pulseStrength;

        //rectTransform.anchoredPosition = localPoint;
        var lerp = Vector2.Lerp(
            rectTransform.localPosition,
            localPoint,
            speed * Time.deltaTime
        );

        rectTransform.localPosition = lerp + pulse;
        ///
    }
}
