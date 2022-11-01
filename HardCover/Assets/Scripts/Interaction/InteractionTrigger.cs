using UnityEngine;
using UnityEngine.Assertions;

// On Object
public class InteractionTrigger : MonoBehaviour
{
    private GameObject inCollisionWith;
    private InteractionController collidedInteractionController;

    private IInteractable interactableObject;

    private void Start()
    {
        interactableObject = GetComponent<IInteractable>();
        Assert.AreNotEqual(interactableObject, null, "No IInteractable Component on Object");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (inCollisionWith != collision.gameObject)
            {
                inCollisionWith = collision.gameObject;
                collidedInteractionController = collision.GetComponent<InteractionController>();
            }

            collidedInteractionController.interactableObj.Add(interactableObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collidedInteractionController.interactableObj.Remove(interactableObject);
        }
    }
}