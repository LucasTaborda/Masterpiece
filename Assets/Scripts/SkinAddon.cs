using UnityEngine;

public class SkinAddon : MonoBehaviour
{
    public ScenographyObject scenographyObject;
    public string key;
    public GameObject addon;

    void OnEnable()
    {
        scenographyObject.AddChangeSkinListener(SetAddonVisibility);
        SetAddonVisibility();
    }

    private void SetAddonVisibility()
    {
        if(scenographyObject.GetKey() == key) addon.SetActive(true);
        else addon.SetActive(false);
    }

    public void OnDisable()
    {
        scenographyObject.RemoveChangeSkinListener(SetAddonVisibility);
    }
}
