using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "AI State/State Name")]
public class AIState : ScriptableObject
{
    public new string name;
    [HideInInspector]
    public bool restartOnEvaluate = false;

    public virtual void Initialize(AIStateManager stateManager)
    {

    }

    public virtual bool EvaluateConditions(AIStateManager stateManager) { return true; }

    // First frame when this state is active
    public virtual void StartState(AIStateManager stateManager) { }

    // Updates every frame while this state is active
    public virtual void RunState(AIStateManager stateManager) { }

    // Last frame when this state is active, before first frame of next state
    public virtual void EndState(AIStateManager stateManager) { }
}