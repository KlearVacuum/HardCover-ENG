using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUiScript : MonoBehaviour
{
    public BookShopCounter ConnectedShop;
    public Slot slot1, slot2, slot3;

    public void Show()
    {
        gameObject.SetActive(true);
        GlobalGameData.playerController.DisableMovement();

        FillInformation(ref slot1, ConnectedShop.GetBook(0));
        FillInformation(ref slot2, ConnectedShop.GetBook(1));
        FillInformation(ref slot3, ConnectedShop.GetBook(2));
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        GlobalGameData.playerController.EnableMovement();
    }

    public void BuyBook(int i)
    {
        Book b = ConnectedShop.BuyBook(i);
        b.gameObject.SetActive(true);
        b.StartInteraction(GlobalGameData.playerStats.gameObject);
        b.BoughtFromShop();

        Hide();

        if (ConnectedShop.BookLeft() == 2)
        {
            GlobalGameData.dialogManager.StartChat("Ah Chai", "BuyBook0");
        }
        else if (ConnectedShop.BookLeft() == 1)
        {
            GlobalGameData.dialogManager.StartChat("Ah Chai", "BuyBook1");
        }
        else if (ConnectedShop.BookLeft() == 0)
        {
            GlobalGameData.dialogManager.StartChat("Ah Chai", "BuyBook1");
        }
    }

    private void FillInformation(ref Slot slot, GameObject go)
    {
        bool setActive = go != null;

        slot.bookImage.gameObject.SetActive(setActive);
        slot.bookOwner.gameObject.SetActive(setActive);
        slot.bookProgress.gameObject.SetActive(setActive);
        slot.bookWithdraw.gameObject.SetActive(setActive);
    }
}