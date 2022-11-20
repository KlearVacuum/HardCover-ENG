using System.Collections.Generic;
using UnityEngine;

public class BookShopCounter : MonoBehaviour, IInteractableAndActionable
{
    public Transform bookLocationOnCounter;
    private Book mBook;

    // ========================================================
    // ALL INTERFACE THINGS
    // ========================================================

    // ========================================================
    // IInteractable
    // ========================================================
    public void StartInteraction(GameObject interactor)
    {
        IInteractable ii = interactor.GetComponent<InteractionController>().GetBook();
        if (ii != null)
        {
            // Holding Book
            mBook = ii.GetObject().GetComponent<Book>();

            // Puts book at the counter
            mBook.EndInteraction();
            mBook.SetPosition(bookLocationOnCounter.position);
        }
        else if (mBook != null)
        {
            mBook.StartInteraction(interactor);
            mBook = null;
        }
    }

    public void StartAction()
    {
        // Purchase Book
        if (mBook != null && mBook.BookOwner != "Amanda")
        {
            if (GlobalGameData.playerStats.TryPurchase(mBook.BookCost))
            {
                mBook.BookOwner = "Amanda";
            }
        }
    }

    public bool IsInteracting()
    {
        return mBook != null;
    }

    public InteractionPriority GetPriority()
    {
        return InteractionPriority.Medium;
    }

    public GameObject GetObject()
    {
        return gameObject;
    }

    public void EndInteraction()
    {
        // Blank
    }

    public void EndAction()
    {
        // Blank
    }

    public bool IsActioning()
    {
        return false;
    }
}