using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Slot
{
    public Image bookImage;
    public TextMeshProUGUI bookOwner;
    public TextMeshProUGUI bookProgress;
    public Button bookWithdraw;
}

public class DrawerUiScript : MonoBehaviour
{
    public Drawer ConnectedDrawer;
    public Slot slot1, slot2, slot3, slot4;

    public void Show()
    {
        gameObject.SetActive(true);
        GlobalGameData.playerController.DisableMovement();

        FillInformation(ref slot1, ConnectedDrawer.GetBook(0));
        FillInformation(ref slot2, ConnectedDrawer.GetBook(1));
        FillInformation(ref slot3, ConnectedDrawer.GetBook(2));
        FillInformation(ref slot4, ConnectedDrawer.GetBook(3));
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        GlobalGameData.playerController.EnableMovement();
    }

    public void WithdrawBook(int i)
    {
        Book b = ConnectedDrawer.TakeBook(i);
        b.gameObject.SetActive(true);
        b.StartInteraction(GlobalGameData.playerStats.gameObject);

        Hide();
    }

    private void FillInformation(ref Slot slot, Book b)
    {
        bool setActive = b != null;

        slot.bookImage.gameObject.SetActive(setActive);
        slot.bookOwner.gameObject.SetActive(setActive);
        slot.bookProgress.gameObject.SetActive(setActive);
        slot.bookWithdraw.gameObject.SetActive(setActive);

        if (setActive)
        {
            slot.bookImage.sprite = b.GetComponent<SpriteRenderer>().sprite;
            slot.bookOwner.text = $"Owner: {b.BookOwner}";
            slot.bookProgress.text = $"Progress: {b.GetProgression()}%";
        }
    }
}