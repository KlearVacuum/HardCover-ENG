using UnityEngine;
using UnityEngine.Assertions;

// On Object
public class ShowUiTrigger : MonoBehaviour
{
    private IShowUiPopUp showUiObject;

    private void Start()
    {
        showUiObject = GetComponent<IShowUiPopUp>();
        Assert.AreNotEqual(showUiObject, null, "No IShowUiPopUp Component on Object");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (showUiObject.TagsToInteractWith().Contains(collision.tag))
        {
            showUiObject.TriggerEnter(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (showUiObject.TagsToInteractWith().Contains(collision.tag))
        {
            showUiObject.TriggerExit(collision);
        }
    }
}