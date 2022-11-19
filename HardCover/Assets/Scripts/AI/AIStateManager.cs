using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// State manager of AI
[RequireComponent(typeof(AIEntityController))]
public class AIStateManager : MonoBehaviour
{
    private AIEntityController mAI;
    public AIEntityController ai { get { return mAI; } }
    public string currentStateName;

    public List<AIState> allAIStates = new List<AIState>();

    private AIState prevState;
    protected AIState currentState;
    protected AIState nextState;

    private string stateResults;

    public bool printStates;

    private void Awake()
    {
        mAI = GetComponent<AIEntityController>();
    }
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        InitializeStates();
        prevState = currentState = nextState = allAIStates[0];
        currentState.StartState(this);
    }

    protected virtual void Update()
    {
        if (allAIStates.Count == 0)
        {
            Debug.LogError(gameObject.name + " AI Controller has no AI states!");
        }
        if (nextState == currentState)
        {
            currentStateName = currentState.name;
            currentState.RunState(this);
            if (mAI.canTransit)
            {
                EvaluateStates();

                if (currentState.restartOnEvaluate && nextState == currentState)
                {
                    currentState.EndState(this);
                    nextState.StartState(this);
                }
            }
        }
        else
        {
            currentState.EndState(this);
            nextState.StartState(this);
            currentState = nextState;
        }
        prevState = currentState;
    }

    public virtual void InitializeStates()
    {
        foreach (AIState state in allAIStates)
        {
            state.Initialize(this);
        }
    }

    public void EvaluateStates()
    {
        stateResults = "Evaluation: \n";
        for (int i = 0; i < allAIStates.Count; ++i)
        {
            stateResults += allAIStates[allAIStates.Count - i - 1].name + ":" + allAIStates[allAIStates.Count - i - 1].EvaluateConditions(this).ToString() + "\n";
            if (allAIStates[allAIStates.Count - i - 1].EvaluateConditions(this))
            {
                nextState = allAIStates[allAIStates.Count - i - 1];
                break;
            }
        }
        // Uncomment this code to take a peek at what the kirbs be thinking about - but be warned it'll be quite a MESS if there's more than 1 active
        if (printStates)
        {
            PrintStateEvaluation();
        }
    }

    public void AddState(AIState aiState, int position)
    {
        if (position >= allAIStates.Count)
        {
            allAIStates.Add(aiState);
            return;
        }
        List<AIState> newStates = new List<AIState>();

        for (int i = 0; i < allAIStates.Count + 1; i++)
        {
            if (i < position)
            {
                newStates.Add(allAIStates[i]);
            }
            else if (i == position)
            {
                newStates.Add(aiState);
            }
            else
            {
                newStates.Add(allAIStates[i - 1]);
            }
        }
        allAIStates = newStates;
    }

    public void PrintStateEvaluation()
    {
        // Debug.Log(stateResults);
    }

    public void ForceTransition()
    {
        mAI.canTransit = true;
    }
}