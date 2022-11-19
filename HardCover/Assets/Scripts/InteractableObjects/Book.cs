using UnityEngine;

public class Book : MonoBehaviour, IInteractableAndActionable
{
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
        mDefaultPosition = transform.parent.position;
        mSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (theInteractor != null)
        {
            SetPosition(theInteractor.transform.position);
        }

        if (isActioning)
        {
            if (mRemainingKnowledge > 0)
            {
                while (timePassedSinceAction > SecondsPerHour)
                {
                    mRemainingKnowledge -= ProgressionPerHour;
                    timePassedSinceAction -= SecondsPerHour;

                    // Don't pop for the last tick, just show knowledge gain
                    GlobalGameData.playerStats.AdjustEnergy(-EnergyConsumptionRatePerHour, mRemainingKnowledge > 0);
                    GlobalGameData.timeManager.AddTime();
                }
            }
            else
            {
                actionOver = true;
                GlobalGameData.playerStats.AdjustKnowledge(KnowledgeToPlayer);
                EndAction();
            }

            GlobalGameData.playerStats.SetProgress(1.0f - (float)mRemainingKnowledge / (float)kStartingKnowledge);
            timePassedSinceAction += Time.deltaTime;
        }
    }

    public void ResetPosition()
    {
        SetPosition(mDefaultPosition);
    }

    public void PlayerGotCaught()
    {
        EndInteraction();
        ResetPosition();
    }

    public void SetPosition(Vector3 pos)
    {
        transform.parent.position = pos;
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
        // Can only pick up if she wasn't already holding a book
        if (interactor.GetComponent<InteractionController>().GetBook() == null)
        {
            // Pick Up Book
            theInteractor = interactor;
            mSpriteRenderer.sortingLayerName = "InteractableInfrontPlayer";
        }
    }

    public void EndInteraction()
    {
        // Drop Book
        theInteractor = null;
        mSpriteRenderer.sortingLayerName = "InteractableInfrontBackground";
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

        GlobalGameData.playerController.DisableMovement();
        GlobalGameData.playerStats.ShowActionBar();
    }

    public void EndAction()
    {
        // Stop Reading
        isActioning = false;

        GlobalGameData.playerController.EnableMovement();
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
        return InteractionPriority.Book;
    }

    public GameObject GetObject()
    {
        return gameObject;
    }
}