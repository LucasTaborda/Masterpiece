using UnityEngine;
using UnityEngine.UI;

public class Pamphlet : MonoBehaviour
{
    public void SwitchView()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        ButtonBoard.Instance.SetButtonsEnabled(!gameObject.activeSelf);
        // ButtonBoard.Instance.SetInputActive(!gameObject.activeSelf);
    }
}
