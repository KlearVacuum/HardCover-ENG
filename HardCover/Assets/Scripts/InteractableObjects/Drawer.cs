using UnityEngine;

public class Drawer : MonoBehaviour, IInteractable
{
    private Book mBook = null;

    public void StartInteraction(GameObject interactor)
    {
        IInteractable ii = interactor.GetComponent<InteractionController>().GetBook();
        if (ii != null)
        {
            // Holding Book
            mBook = ii.GetObject().GetComponent<Book>();

            // Puts book into drawer
            mBook.EndInteraction();
            mBook.SetPosition(transform.position);
        }
        else if (mBook != null)
        {
            mBook.StartInteraction(interactor);
            mBook = null;
        }
    }

    public InteractionPriority GetPriority()
    {
        return InteractionPriority.Medium;
    }

    public GameObject GetObject()
    {
        return gameObject;
    }
}