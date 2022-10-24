using UnityEngine;

public class PortalTrigger : MonoBehaviour, IInteractable
{
    public Transform destination;

    private GameObject inCollisionWith;

    private void Teleport(GameObject go)
    {
        if (GlobalGameData.portalCooldown <= 0 && go != null)
        {
            GlobalGameData.portalCooldown = 0.5f;
            go.transform.position = destination.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inCollisionWith = collision.gameObject;
            collision.GetComponent<PortalControl>().activePortals.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inCollisionWith = null;
            collision.GetComponent<PortalControl>().activePortals.Remove(this);
        }
    }

    public void StartInteraction()
    {
        Teleport(inCollisionWith);
    }
}