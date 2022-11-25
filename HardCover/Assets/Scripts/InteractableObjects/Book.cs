using UnityEngine;
using UnityEngine.Assertions;

public class Book : MonoBehaviour, IInteractableAndActionable
{
    private Drawer drawer;
    private BookShopCounter shop;

    public int KnowledgeToPlayer = 25;

    public int ProgressionPerHour = 10;
    public int EnergyConsumptionRatePerHour = 2;
    public int BookCost = 30;

    public float SecondsPerHour = 2.0f; // Amount of seconds IRL convert to in game hours

    // This is the name of owner
    public string BookOwner = "Amanda";
    public int BookVolume = 0;

    private const int kStartingKnowledge = 100;
    private int mRemainingKnowledge = kStartingKnowledge;

    private float timePassedSinceAction;

    private bool mDefaultActive;
    private Vector3 mDefaultPosition;
    private SpriteRenderer mSpriteRenderer;
    private bool mWasDrawer = false;

    private void Awake()
    {
        mDefaultActive = true;
        mDefaultPosition = transform.parent.position;
        mSpriteRenderer = GetComponent<SpriteRenderer>();

        drawer = GameObject.FindObjectOfType<Drawer>();
        shop = GameObject.FindObjectOfType<BookShopCounter>();

        Assert.AreNotEqual(BookVolume, 0, "Please set the book volume thanks");
    }

    private void Update()
    {
        if (theInteractor != null)
        {
            SetPosition(theInteractor.transform.position + Vector3.up);
        }

        if (isActioning)
        {
            if (mRemainingKnowledge > 0)
            {
                while (timePassedSinceAction > SecondsPerHour)
                {
                    timePassedSinceAction -= SecondsPerHour;

                    // Don't pop for the last tick, just show knowledge gain
                    if (GlobalGameData.playerStats.TryConsumeEnergy(EnergyConsumptionRatePerHour))
                    {
                        mRemainingKnowledge -= ProgressionPerHour;
                        GlobalGameData.timeManager.AddTime();
                        GlobalGameData.playerStats.AdjustKnowledge((int)((float)KnowledgeToPlayer * (float)ProgressionPerHour / 100));
                        GlobalGameData.playerStats.SetProgress(1.0f - (float)mRemainingKnowledge / (float)kStartingKnowledge);
                    }
                }
            }
            else
            {
                actionOver = true;
                EndAction();
            }

            timePassedSinceAction += Time.deltaTime;
        }
    }

    public void ResetPosition()
    {
        EndInteraction();

        SetPosition(mDefaultPosition);
        gameObject.SetActive(mDefaultActive);

        if (mWasDrawer)
        {
            drawer.PutBook(this);
        }
    }

    public void StoredInDrawer()
    {
        mDefaultPosition = drawer.transform.position;
        mDefaultActive = false;
        mWasDrawer = true;
    }

    public void BoughtFromShop()
    {
        mDefaultPosition = shop.transform.position;
        mDefaultActive = true;
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

    public int GetProgression()
    {
        return (kStartingKnowledge - mRemainingKnowledge) * 100 / kStartingKnowledge;
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
        SetPosition(theInteractor.transform.position);
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