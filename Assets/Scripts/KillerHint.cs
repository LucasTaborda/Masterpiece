using UnityEngine;

public class KillerHint : MonoBehaviour
{
    public GameObject hint;

    void OnEnable()
    {
        if(Game.isActorLiberated && Game.IsKnifeHooked) {
            hint.SetActive(true);
        }
        else{
            hint.SetActive(false);
        }
    }
}
