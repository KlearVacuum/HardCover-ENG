using UnityEngine;

public class Book : MonoBehaviour, IInteractableAndActionable
{
    public float yOffSet = 0.5f;

    public int KnowledgeToPlayer = 25;

    public int ProgressionPerHour = 10;
    public int EnergyConsumptionRatePerHour = 2;
    public int BookCost = 30;

    public float SecondsPerHour = 2.0f; // Amount of seconds IRL convert to in game hours

    // This is the name of owner
    public string BookOwner = "Amanda";

    private const int kStartingKnowledge = 100;
    private int mRemainingKnowledge = kStartingKnowledge;

    private float timePassedSinceAction;

    private Vector3 mDefaultPosition;
    private SpriteRenderer mSpriteRenderer;

    private void Awake()
    {
        mDefaultPosition = transform.position;
        mSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (theInteractor != null)
        {
            transform.position = theInteractor.transform.position + new Vector3(0, yOffSet, 0);
        }

        if (isActioning)
        {
            if (mRemainingKnowledge > 0)
            {
                while (timePassedSinceAction > SecondsPerHour)
                {
                    mRemainingKnowledge -= ProgressionPerHour;
                    timePassedSinceAction -= SecondsPerHour;
                    GlobalGameData.playerStats.energy -= EnergyConsumptionRatePerHour;
                    GlobalGameData.timeManager.AddTime();
                }
            }
            else
            {
                actionOver = true;
                GlobalGameData.playerStats.knowledge += KnowledgeToPlayer;
                EndAction();
            }

            GlobalGameData.playerStats.SetProgress(1.0f - (float)mRemainingKnowledge / (float)kStartingKnowledge);
            timePassedSinceAction += Time.deltaTime;
        }
    }

    public void ResetPosition()
    {
        transform.position = mDefaultPosition;
    }

    public void PlayerGotCaught()
    {
        EndInteraction();
        ResetPosition();
    }

    // ========================================================
    // ALL INTERFACE THINGS
    // ========================================================

    // ========================================================
    // IInteractableAndActionable
    // ========================================================
    private bool isActioning, actionOver;
    private GameObject theInteractor;

    public void StartInteraction(GameObject interactor)
    {
        // Pick Up Book
        theInteractor = interactor;
        mSpriteRenderer.sortingLayerName = "InteractableInfrontPlayer";

        Debug.Log("Pick up Book");
    }

    public void EndInteraction()
    {
        // Drop Book
        theInteractor = null;
        mSpriteRenderer.sortingLayerName = "InteractableInfrontBackground";

        Debug.Log("Drop Book");
    }

    public void StartAction()
    {
        if (actionOver)
        {
            return;
        }

        // Start Reading
        isActioning = true;
        timePassedSinceAction = 0;
        Debug.Log("Start Reading");

        GlobalGameData.playerStats.ShowActionBar();
    }

    public void EndAction()
    {
        // Stop Reading
        isActioning = false;
        Debug.Log("Stop Reading");

        GlobalGameData.playerStats.HideActionBar();
    }

    public bool IsInteracting()
    {
        return theInteractor != null;
    }

    public bool IsActioning()
    {
        return isActioning;
    }

    public InteractionPriority GetPriority()
    {
        return InteractionPriority.Low;
    }
}