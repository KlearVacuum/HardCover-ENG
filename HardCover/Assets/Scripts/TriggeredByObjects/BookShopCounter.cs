using System.Collections.Generic;
using UnityEngine;

public class BookShopCounter : MonoBehaviour, IShowUiPopUp
{
    public Transform bookLocationOnCounter;

    private Book mBook;
    private GameObject mPlayer;

    private void Update()
    {
        if (mBook == null || mPlayer == null)
        {
            return;
        }

        if (mBook.IsInteracting())
        {
            return;
        }

        // Puts book at the counter
        mBook.transform.position = bookLocationOnCounter.position;

        // Purchase Book
        // TODO: FIX HARDCODE
        if (mBook.ownerName != "Amanda")
        {
            // TODO: ACCESS PLAYER STUFF

            // TODO: Try to purchase
            if (true)
            {
                mBook.ownerName = "Amanda";
            }
        }
    }

    // ========================================================
    // ALL INTERFACE THINGS
    // ========================================================

    // ========================================================
    // IShowUiPopUp
    // ========================================================
    private readonly List<string> canTriggerUiPopUpList = new() { "Book", "Player" };

    public List<string> TagsToInteractWith()
    {
        return canTriggerUiPopUpList;
    }

    public void TriggerEnter(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Book":
            {
                mBook = collision.GetComponent<Book>();
                break;
            }
            case "Player":
            {
                mPlayer = collision.gameObject;
                break;
            }
        }
    }

    public void TriggerExit(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Book":
            {
                mBook = null;
                break;
            }
            case "Player":
            {
                mPlayer = null;
                break;
            }
        }
    }
}