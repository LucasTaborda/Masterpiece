using UnityEngine;

public class Act: MonoBehaviour
{
    public StageScene[] scenes;
    public ScenographyObject[] scenographyObjects = new ScenographyObject[5]; //Deben ir ordenados de arriba a abajo
    public GameObject[] notPlayableScenography;
    public string lastActDialogKey;
    public string title;
}
