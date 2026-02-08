using UnityEngine;

public class Rail : MonoBehaviour
{
    public RailWaypointLevel[] waypointLevels;

    public bool isMovingRight = true;
    public bool isMovingUp = true;
    public float tweenTimeX = 0.5f;

    public Transform currentWaypoint;
    public RailWaypointLevel currentWaypointLevel;
    public ScenographyObject scenographyObject;

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
                if (waypointLevels[i].waypoints[j] == currentWaypoint)
                {
                    if( isGoingRight){
                        if(j != waypointLevels[i].waypoints.Length - 1) {
                            nextWaypoint = waypointLevels[i].waypoints[j + 1];
                        }
                        return nextWaypoint;
                    } 
                    else{
                        if(j != 0){
                            nextWaypoint = waypointLevels[i].waypoints[j - 1];
                        }   
                        return nextWaypoint;
                   }
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

    public void GetNextWaypointLevel(bool isGoingUp, RailWaypointLevel currentWaypointLevel)
    {
        RailWaypointLevel railWaypointLevel = currentWaypointLevel;
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

    public void TweenHorizontalToNextWaypoint()
    {
        var nextWaypoint = GetNextWaypoint(isMovingRight, currentWaypoint);
        Debug.Log("nextWaypoint: " + nextWaypoint);
        LeanTween.moveX(scenographyObject.gameObject, nextWaypoint.position.x, tweenTimeX)
        .setOnComplete(() => {
            currentWaypoint = nextWaypoint;
            if(IsLastWaypoint(currentWaypoint)){
                isMovingRight = false;
            }
            else if(IsFirstWaypoint(currentWaypoint)){
                isMovingRight = true;
            }
        });
    }

}
