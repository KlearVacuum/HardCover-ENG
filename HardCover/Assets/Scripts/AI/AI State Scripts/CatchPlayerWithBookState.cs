using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI State/Catch Player")]
public class CatchPlayerWithBookState : AIState
{
    public override bool EvaluateConditions(AIStateManager stateManager)
    {
        // check if player is in sight, and player is holding a book
        return stateManager.ai.catchPlayer;
    }

    public override void StartState(AIStateManager stateManager)
    {
        stateManager.ai.canTransit = false;
        stateManager.ai.StopMoving();
        stateManager.ai.currentCoroutine = stateManager.ai.currentCoroutine.StartCoroutine(stateManager.ai, stateManager.ai.CatchPlayerCoroutine());
    }

    public override void RunState(AIStateManager stateManager)
    {
        if (!stateManager.ai.currentCoroutine.running)
        {
            stateManager.ai.canTransit = true;
            stateManager.ai.StopMoving();
        }
    }
}
