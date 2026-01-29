using UnityEngine;
using UnityEngine.EventSystems;


public class VerticalButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ButtonBoard buttonBoard;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonBoard.MoveVertical();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonBoard.StopMoving();
    }

}
