using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatrolData
{
    public new string name;
    public List<Transform> patrolPoints;
    public Vector2 minMaxAvailableDay;
    public Vector2 minMaxAvailableHour;
    public float waitTime;

    public bool PatrolAvailable(int day, float hour)
    {
        // check day
        bool dayAvailable = day >= (int)minMaxAvailableDay.x && day <= (int)minMaxAvailableDay.y;

        // check hour
        bool hourAvailable = hour >= minMaxAvailableHour.x && hour <= minMaxAvailableHour.y;

        return dayAvailable && hourAvailable;
    }

    public void NextIndex(ref int index)
    {
        index++;
        if (index >= patrolPoints.Count) index = 0;
    }

    public int GetClosestPatrolIndex(Vector2 pos)
    {
        int index = int.MaxValue;
        float nearestDistance = float.MaxValue;

        for (int i = 0; i < patrolPoints.Count; i++)
        {
            float distance = Vector2.Distance(pos, patrolPoints[i].position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                index = i;
            }
        }
        return index;
    }
}
