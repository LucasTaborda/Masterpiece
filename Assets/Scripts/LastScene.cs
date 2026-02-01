using UnityEngine;

public class LastScene : MonoBehaviour
{
    public GameObject liberableActor;
    public GameObject killer;
    public GameObject[] otherObjects;

    void Start()
    {
        if(Game.isActorLiberated) liberableActor.SetActive(false);
        if(Game.IsKnifeTaken){
            killer.SetActive(true);
            for(int i = 0; i < otherObjects.Length; i++){
                otherObjects[i].SetActive(false);
            }
        }
    }
}
