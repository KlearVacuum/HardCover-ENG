using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public List<Transform> followTargets;
    public Vector3 offset;

    private void Update()
    {
        FollowTargets();
    }

    void FollowTargets()
    {
        if (followTargets == null || followTargets.Count == 0) return;
        Vector3 desiredPos = Vector2.zero;
        foreach(Transform target in followTargets)
        {
            desiredPos += target.position;
        }
        desiredPos /= followTargets.Count;
        float distance = Vector2.Distance(transform.position, desiredPos);
        if (distance < 0.001f) return;
        transform.position = (Vector3)Vector2.Lerp(transform.position, desiredPos, 0.025f) + offset;
    }
}