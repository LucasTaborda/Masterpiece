using UnityEngine;

public class Killer : MonoBehaviour
{
    public void OnShowFinished()
    {
        Curtain.Instance.Down(ActManager.Instance.EndWithKnife);
        // ActManager.Instance.
    }
}
