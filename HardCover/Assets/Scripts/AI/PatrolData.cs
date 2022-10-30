using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatrolData
{
    public List<Transform> patrolPoints;
    public List<Vector2> minMaxAvailableTimes;
    public float waitTime;

    public bool PatrolAvailable(float time)
    {
        foreach (Vector2 timeSlot in minMaxAvailableTimes)
        {
            if (time > timeSlot.x && time < timeSlot.y)
            {
                return true;
            }
        }
        return false;
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
