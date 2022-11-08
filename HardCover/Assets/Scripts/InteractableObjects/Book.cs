using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour, IInteractableAndActionable
{
    public float yOffSet = 0.5f;
    public float knowledgeConsumptionRate = 10.0f;

    // This is how much knowledge left the player can gain from the book
    private const float kStartingKnowledge = 100.0f;
    private float mKnowledgeValue = kStartingKnowledge;

    public float knowledgeValue
    {
        get => mKnowledgeValue;
        set => mKnowledgeValue = value;
    }

    // This is the name of owner
    [SerializeField] private string mOwnerName;

    public string ownerName
    {
        get => mOwnerName;
        set => mOwnerName = value;
    }

    [SerializeField] private int mBookCost = 50;
    public int bookCost
    {
        get => mBookCost;
        set => mBookCost = value;
    }

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
            if (mKnowledgeValue > 0.0f)
            {
                mKnowledgeValue -= knowledgeConsumptionRate * Time.deltaTime;
                GlobalGameData.playerStats.SetProgress(1.0f - mKnowledgeValue / kStartingKnowledge);
            }
            else // Book reading done
            {
                actionOver = true;
                mKnowledgeValue = 0.0f;

                EndAction();
            }
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
}