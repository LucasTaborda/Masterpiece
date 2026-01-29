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
        rectTransform.position = cam.WorldToScreenPoint(buttonBoard.currentScenographyObject.transform.position);
    }

}
