using UnityEngine;

public class Book : MonoBehaviour, IInteractableAndActionable
{
    // This is how much knowledge left the player can gain from the book
    private float mKnowledgeValue = 100.0f;

    public float knowledgeValue
    {
        get => mKnowledgeValue;
        set => mKnowledgeValue = value;
    }

    // This is the name of owner
    private string mOwnerName;

    public string ownerName
    {
        get => mOwnerName;
        set => mOwnerName = value;
    }

    private Vector3 mDefaultPosition;

    private void Awake()
    {
        mDefaultPosition = transform.position;
    }

    private void Update()
    {
        if (theInteractor != null)
        {
            transform.position = theInteractor.transform.position;
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
    private bool isActioning;
    private GameObject theInteractor;

    public void StartInteraction(GameObject interactor)
    {
        // Pick Up Book
        theInteractor = interactor;
        Debug.Log("Pick up Book");
    }

    public void EndInteraction()
    {
        // Drop Book
        theInteractor = null;
        Debug.Log("Drop Book");
    }

    public void StartAction()
    {
        // Start Reading
        isActioning = true;
        Debug.Log("Start Reading");
    }

    public void EndAction()
    {
        // Stop Reading
        isActioning = false;
        Debug.Log("Stop Reading");
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