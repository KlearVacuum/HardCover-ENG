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
        if (mBook.BookOwner != "Amanda")
        {
            if (Input.GetKeyDown(KeyCode.Space) && GlobalGameData.playerStats.TryPurchase(mBook.BookCost))
            {
                mBook.BookOwner = "Amanda";
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
                Debug.Log("Book touch Counter");
                mBook = collision.GetComponent<Book>();
                break;
            }
            case "Player":
            {
                Debug.Log("Player touch Counter");
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