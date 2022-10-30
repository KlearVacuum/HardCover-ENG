using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    public Transform destination;
<<<<<<< Updated upstream
    public void Teleport(GameObject GO)
=======

    private GameObject inCollisionWith;

    public void Teleport(GameObject go)
>>>>>>> Stashed changes
    {
        if (GlobalGameData.portalCooldown > 0) return;

        GO.transform.position = destination.position;
        GlobalGameData.portalCooldown = 0.5f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PortalControl>().activePortals.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PortalControl>().activePortals.Remove(this);
        }
    }
}
