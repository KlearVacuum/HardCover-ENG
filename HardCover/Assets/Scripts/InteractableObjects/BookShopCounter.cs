﻿using System.Collections.Generic;
using UnityEngine;

public class BookShopCounter : MonoBehaviour, IInteractable
{
    public ShopUiScript shopUi = null;
    public GameObject[] mBookList = new GameObject[3];

    public GameObject GetBook(int i)
    {
        return mBookList[i];
    }

    public Book BuyBook(int i)
    {
        GameObject prefab = mBookList[i];

        if (prefab != null)
        {
            Book b = GameObject.Instantiate(prefab, transform.position, Quaternion.identity)
                .GetComponentInChildren<Book>();

            if (b.BookOwner != "Amanda")
            {
                if (GlobalGameData.playerStats.TryPurchase(b.BookCost))
                {
                    b.BookOwner = "Amanda";
                    mBookList[i] = null;
                    return b;
                }
            }
        }

        return null;
    }

    public int BookLeft()
    {
        int count = 0;
        for (int i = 0; i < 3; ++i)
        {
            if (mBookList[i] != null) ++count;
        }

        return count;
    }

    // ========================================================
    // ALL INTERFACE THINGS
    // ========================================================

    // ========================================================
    // IInteractable
    // ========================================================
    public void StartInteraction(GameObject interactor)
    {
        shopUi.Show();
    }

    public InteractionPriority GetPriority()
    {
        return InteractionPriority.Default;
    }

    public GameObject GetObject()
    {
        return gameObject;
    }
}