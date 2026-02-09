using UnityEngine;
using UnityEngine.Events;

public class Rail : MonoBehaviour
{
    public RailWaypointLevel[] waypointLevels;

    public bool isMovingRight = true;
    public bool isMovingUp = true;
    public float tweenTimeX = 0.5f;
    public float tweenTimeY = 0.3f;
    public Transform currentWaypoint;
    public RailWaypointLevel currentWaypointLevel;
    public ScenographyObject scenographyObject;
    public LeanTweenType horizontalTweenType = LeanTweenType.linear;
    public LeanTweenType verticalTweenType = LeanTweenType.linear;

    public Transform GetClosestWaypoint()
    {
        Transform closest = waypointLevels[0].waypoints[0];

        foreach (RailWaypointLevel level in waypointLevels)
        {
            foreach (Transform waypoint in level.waypoints)
            {
                if (Vector3.Distance(scenographyObject.transform.position, waypoint.position) < Vector3.Distance(scenographyObject.transform.position, closest.position))
                {
                    closest = waypoint;
                }
            }
        }
        if(IsLastWaypoint(closest)){
            isMovingRight = false;
        }
        else if(IsFirstWaypoint(closest)){
            isMovingRight = true;
        }
        return closest;
    }

    public Transform GetNextWaypoint(bool isGoingRight, Transform currentWaypoint)
    {
        Transform nextWaypoint = currentWaypoint;
        for(int i = 0; i < waypointLevels.Length; i++)
        {
            for(int j = 0; j < waypointLevels[i].waypoints.Length; j++)
            {
                if (waypointLevels[i].waypoints[j] != currentWaypoint) 
                    continue; 
                if( isGoingRight ){
                    if(j != waypointLevels[i].waypoints.Length - 1) {
                        nextWaypoint = waypointLevels[i].waypoints[j + 1];
                    }
                    return nextWaypoint;
                } 
                else {
                    if(j != 0){
                        nextWaypoint = waypointLevels[i].waypoints[j - 1];
                    }   
                    return nextWaypoint;
                }   
            }
        }
        return nextWaypoint;
    }

    public bool IsLastWaypoint(Transform currentWaypoint)
    {
        for(int i = 0; i < waypointLevels.Length; i++)
        {
            var levelWaypoints = waypointLevels[i].waypoints;
            if (levelWaypoints[levelWaypoints.Length - 1] == currentWaypoint)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsFirstWaypoint(Transform currentWaypoint)
    {
        for(int i = 0; i < waypointLevels.Length; i++)
        {
            var levelWaypoints = waypointLevels[i].waypoints;
            if (levelWaypoints[0] == currentWaypoint)
            {
                return true;
            }
        }
        return false;
    }


    public RailWaypointLevel GetClosesWaypointLevel()
    {
        currentWaypointLevel = waypointLevels[0];
        foreach(var level in waypointLevels)
        {
            if (Vector3.Distance(scenographyObject.transform.position, level.transform.position) < Vector3.Distance(scenographyObject.transform.position, currentWaypointLevel.transform.position))
            {
                currentWaypointLevel = level;
            }
        }

        return currentWaypointLevel;
    }

    public void SetScenographyObject(ScenographyObject scenographyObject)
    {
        this.scenographyObject = scenographyObject;
        currentWaypoint = GetClosestWaypoint();
        currentWaypointLevel = GetClosesWaypointLevel();
    }

    public void TweenHorizontalToNextWaypoint(UnityAction callback = null)
    {
        var nextWaypoint = GetNextWaypoint(isMovingRight, currentWaypoint);
        Debug.Log("nextWaypoint: " + nextWaypoint);
        LeanTween.moveX(scenographyObject.gameObject, nextWaypoint.position.x, tweenTimeX)
        .setEase(horizontalTweenType)
        .setOnComplete(() => {
            currentWaypoint = nextWaypoint;
            if(IsLastWaypoint(currentWaypoint)){
                isMovingRight = false;
            }
            else if(IsFirstWaypoint(currentWaypoint)){
                isMovingRight = true;
            }
            callback?.Invoke();
        });
    }

    public RailWaypointLevel GetNextLevel()
    {
        for(int i = 0; i < waypointLevels.Length; i++){
            if(waypointLevels[i] != currentWaypointLevel) continue;
            if(isMovingUp){
                if(i != waypointLevels.Length - 1){
                    currentWaypointLevel = waypointLevels[i + 1];
                }
                return currentWaypointLevel;
            }
            else{
                if(i != 0){
                    currentWaypointLevel = waypointLevels[i - 1];
                }
                return currentWaypointLevel;
            }
        }
        return currentWaypointLevel;
    }
    

    public int GetWaypointIndex()
    {
        Transform closest = waypointLevels[0].waypoints[0];
        int closestIndex = 0;

        for(int i = 0; i < waypointLevels.Length; i++){
            for(int j = 0; j < waypointLevels[i].waypoints.Length; j++){
                if(Vector3.Distance(scenographyObject.transform.position, waypointLevels[i].waypoints[j].position) < Vector3.Distance(scenographyObject.transform.position, closest.position)){
                    closest = waypointLevels[i].waypoints[j];
                    closestIndex = j;
                }
            }
        }

        return closestIndex;
    }

    public void TweenVerticalToNextLevel(UnityAction callback = null)
    {
        var nextWaypointLevel = GetNextLevel();

        LeanTween.moveY(scenographyObject.gameObject, nextWaypointLevel.waypoints[0].position.y, tweenTimeY)
        .setEase(verticalTweenType)
        .setOnComplete(() => {
            currentWaypointLevel = nextWaypointLevel;
            currentWaypoint = GetClosestWaypoint();
            if(currentWaypointLevel == waypointLevels[waypointLevels.Length - 1]){
                isMovingUp = false;
            }
            else if(currentWaypointLevel == waypointLevels[0]){
                isMovingUp = true;
            }
            callback?.Invoke();
        });
    }
}
