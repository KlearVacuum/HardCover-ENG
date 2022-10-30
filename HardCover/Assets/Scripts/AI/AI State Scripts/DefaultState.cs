using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI State/Default")]
public class DefaultState : AIState
{
    public override void Initialize(AIStateManager stateManager)
    {
        restartOnEvaluate = false;
    }

    public override void StartState(AIStateManager stateManager)
    {
        stateManager.ai.canTransit = true;
    }
}
