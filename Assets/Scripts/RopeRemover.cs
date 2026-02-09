using UnityEngine;

public class RopeRemover : MonoBehaviour
{
    public GameObject[] spawnObjects;
    public GameObject[] hideObjects;
    
    void OnEnable()
    {
        Game.RemoveRopeDamagedListener(OnRopeDamaged);
        Game.AddRopeDamagedListener(OnRopeDamaged);
    }

    void OnRopeDamaged()
    {
        foreach (GameObject obj in spawnObjects)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in hideObjects)
        {
            obj.SetActive(false);
        }
    }
}
