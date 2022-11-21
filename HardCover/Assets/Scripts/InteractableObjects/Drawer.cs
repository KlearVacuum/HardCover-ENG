using UnityEngine;

public class Drawer : MonoBehaviour, IInteractable
{
    public DrawerUiScript drawerUi = null;

    private Book[] mBookList = new Book[4];

    public Book GetBook(int i)
    {
        return mBookList[i];
    }

    public Book TakeBook(int i)
    {
        Book b = mBookList[i];
        b.StoredInDrawer();

        mBookList[i] = null;
        return b;
    }

    public void PutBook(Book b)
    {
        mBookList[b.BookVolume - 1] = b;
    }

    public void StartInteraction(GameObject interactor)
    {
        IInteractable ii = interactor.GetComponent<InteractionController>().GetBook();
        if (ii != null)
        {
            Book b = ii.GetObject().GetComponent<Book>();

            // Holding Book
            PutBook(b);

            // Puts book into drawer
            b.EndInteraction();
            b.SetPosition(transform.position);
            b.gameObject.SetActive(false);
        }
        else
        {
            drawerUi.Show();
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