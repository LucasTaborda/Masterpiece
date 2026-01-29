using UnityEngine;
using UnityEngine.EventSystems;


public class HorizontalButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ButtonBoard buttonBoard;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonBoard.MoveHorizontal();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonBoard.StopMoving();
    }

}
