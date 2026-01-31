using UnityEngine;

public class LastScene : MonoBehaviour
{
    public GameObject liberableActor;

    void Start()
    {
        if(Game.isActorLiberated) liberableActor.SetActive(false);
    }
}
