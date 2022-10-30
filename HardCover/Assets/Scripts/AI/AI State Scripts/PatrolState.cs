using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI State/Patrol")]
public class PatrolState : AIState
{
    public override void Initialize(AIStateManager stateManager)
    {
        restartOnEvaluate = true;
    }
    public override bool EvaluateConditions(AIStateManager stateManager)
    {
        stateManager.ai.activePatrol = stateManager.ai.PatrolAvailable();
        return stateManager.ai.activePatrol != null;
    }
    public override void StartState(AIStateManager stateManager)
    {
        Debug.Log("start patrol: go to index " + stateManager.ai.patrolIndex);
        stateManager.ai.currentCoroutine = stateManager.ai.currentCoroutine.StartCoroutine(stateManager.ai, GoToPatrolPoint(stateManager));
        stateManager.ai.canTransit = false;
    }

    public override void RunState(AIStateManager stateManager)
    {
        if (!stateManager.ai.currentCoroutine.running)
        {
            stateManager.ai.canTransit = true;
            stateManager.ai.StopMoving();
            Debug.Log("arrived at patrol point " + stateManager.ai.patrolIndex);
            stateManager.ai.activePatrol.NextIndex(ref stateManager.ai.patrolIndex);
        }
    }

    IEnumerator GoToPatrolPoint(AIStateManager stateManager)
    {
        Debug.Log("start moving coroutine");
        Vector2 patrolPoint = stateManager.ai.activePatrol.patrolPoints[stateManager.ai.patrolIndex].position;
        PortalTrigger closestStairs = null;
        if (Mathf.Abs(stateManager.transform.position.y - patrolPoint.y) > 1f)
        {
            stateManager.ai.climbStairs = true;
            closestStairs = stateManager.ai.GetClosestStairs(stateManager.ai.transform.position, 20f);
        }

        float distance = float.MaxValue;
        // go to stairs if I need stairs and I found stairs
        if (stateManager.ai.climbStairs && closestStairs != null)
        {
            stateManager.ai.moveToTarget = closestStairs.transform;
            distance = Vector2.Distance(stateManager.ai.transform.position, closestStairs.transform.position);
            while (distance > 0.5f)
            {
                // Debug.DrawLine(stateManager.transform.position, closestStairs.transform.position, Color.white);
                stateManager.ai.MoveTowardTarget();
                distance = Vector2.Distance(stateManager.ai.transform.position, closestStairs.transform.position);
                yield return null;
            }
            Debug.Log("interact with stairs");
            closestStairs.Teleport(stateManager.gameObject);
            stateManager.ai.climbStairs = false;
        }

        // go to actual destination
        stateManager.ai.moveToTarget = stateManager.ai.activePatrol.patrolPoints[stateManager.ai.patrolIndex];
        distance = Vector2.Distance(stateManager.ai.transform.position, patrolPoint);
        while (distance > 0.5f)
        {
            // Debug.DrawLine(stateManager.transform.position, patrolPoint, Color.white);
            stateManager.ai.MoveTowardTarget();
            distance = Vector2.Distance(stateManager.ai.transform.position, patrolPoint);
            yield return null;
        }
        stateManager.ai.StopMoving();
        yield return new WaitForSeconds(stateManager.ai.activePatrol.waitTime);
    }
}
