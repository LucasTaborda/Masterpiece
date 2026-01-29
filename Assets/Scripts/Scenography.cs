using UnityEngine;

public class Scenography : MonoBehaviour
{
    public static Scenography Instance { get; private set; }
    public Transform topRailLeftLimitTransform;
    public Transform topRailRightLimitTransform;
    public Transform topRailUpLimitTransform;
    public Transform topRailDownLimitTransform;
    public Transform backgroundRailLeftLimitTransform;
    public Transform backgroundRailRightLimitTransform;
    public Transform backgroundRailUpLimitTransform;
    public Transform backgroundRailDownLimitTransform;
    public Transform backCharacterRightLimitTransform;
    public Transform backCharacterLeftLimitTransform;
    public Transform backCharacterUpLimitTransform;
    public Transform backCharacterDownLimitTransform;
    public Transform frontCharacterRightLimitTransform;
    public Transform frontCharacterLeftLimitTransform;
    public Transform frontCharacterUpLimitTransform;
    public Transform frontCharacterDownLimitTransform;
    public Transform frontRailRightLimitTransform;
    public Transform frontRailLeftLimitTransform;
    public Transform frontRailUpLimitTransform;
    public Transform frontRailDownLimitTransform;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            throw new System.Exception("Only one Scenography is allowed");
    }
}
