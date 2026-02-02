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

    void LateUpdate()
    {
        var centerPosition = buttonBoard.targetSpriteRenderer.bounds.center;
        
        var screenPoint = cam.WorldToScreenPoint(centerPosition);
            Vector2 localPoint;
    
        // Convertir el punto de pantalla a coordenadas locales del canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect, 
            screenPoint, 
            canvas.worldCamera, // Usa la cámara del canvas
            out localPoint
        );
        
        // Aplicar la posición
        rectTransform.anchoredPosition = localPoint;
    }

}
